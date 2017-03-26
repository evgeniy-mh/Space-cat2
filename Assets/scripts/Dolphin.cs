using UnityEngine;
using System.Collections;

public class Dolphin : Enemy {

    public AudioClip[] dolphinSounds;
    


    void Start()
    {
        StartSimpleMove();

    }

    // Update is called once per frame
    void Update () {
	
	}

    public void CollectMe()
    {
        AudioSource.PlayClipAtPoint(dolphinSounds[Random.Range(0, dolphinSounds.Length)], transform.position);
        myAnim.SetBool("collected", true);
        GetComponentInChildren<Collider>().enabled = false;
        Destroy(gameObject,1f);
    }

}
