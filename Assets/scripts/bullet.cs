using UnityEngine;
using System.Collections;

public class bullet : Weapon {

    protected override void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        asc = GetComponent<AudioSource>();

        //Destroy(gameObject, destructionTime);
    }


}
