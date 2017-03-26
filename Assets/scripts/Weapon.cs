using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {

    public float speed;
    public float fireRate;
    public int damage;

    public bool mustBeDestroyed;//нужно ли уничтожить после столкновения с врагом

    public float destructionTime;

    public AudioClip ShotSound;

    protected AudioSource asc;

    protected virtual void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        asc = GetComponent<AudioSource>();
        transform.rotation = Quaternion.Euler(0, 0, 0);
        rb.velocity = transform.forward * speed;
        asc.PlayOneShot(ShotSound);

        Destroy(gameObject,destructionTime);
    }
}
