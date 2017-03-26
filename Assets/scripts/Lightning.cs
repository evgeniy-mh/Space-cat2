using UnityEngine;
using System.Collections;

public class Lightning : Weapon {

    public GameObject lightningSFX;
    public byte lightningSFXCount;
    public byte lightningColiderOffTime;
    public AudioClip thunderClap;

    protected AudioSource asc;


    override protected void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        //AudioSource.PlayClipAtPoint(ShotSound, transform.position);
        asc = GetComponent<AudioSource>();
        asc.PlayOneShot(ShotSound);
        asc.PlayOneShot(thunderClap);

        StartCoroutine(spawnLightningSFX());
        StartCoroutine(ColliderOff());
        Destroy(gameObject, destructionTime);
    }

    IEnumerator ColliderOff()
    {
        yield return new WaitForSeconds(lightningColiderOffTime);
        GetComponent<SphereCollider>().enabled = false;
    }

    IEnumerator spawnLightningSFX()
    {
        GameObject temp;
        for (int i = 0; i <= lightningSFXCount; i++)
        {
            temp=Instantiate(lightningSFX);
            temp.transform.position = transform.position;
            Destroy(temp,1f);
            yield return new WaitForSeconds(0.2f);
        }
        
    }


}
