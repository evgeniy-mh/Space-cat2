using UnityEngine;
using System.Collections;

public class Fireball : Weapon {

    public AudioClip extraSound;

    // Use this for initialization
    protected override void Start () {
        asc = GetComponent<AudioSource>();
        asc.PlayOneShot(ShotSound);
        asc.PlayOneShot(extraSound);
        Destroy(gameObject, destructionTime);
    }
	
}
