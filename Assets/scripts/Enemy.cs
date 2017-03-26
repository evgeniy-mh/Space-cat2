using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    public float moveSpeed;
    public GameObject explosion;
    //public AudioClip explosionSound;
    public AudioClip[] takeDamageSounds;
    public int health;

    protected Rigidbody rb;
    protected Animator myAnim;

    protected virtual void Awake()
    {
        Physics.IgnoreLayerCollision(8, 10,true);
        rb = GetComponent<Rigidbody>();
        myAnim = GetComponentInChildren<Animator>();
        
        //Destroy(gameObject,10f);
    }

    protected virtual void StartSimpleMove()
    {
        //rb.velocity = transform.forward * moveSpeed;
        rb.velocity = Vector3.back * moveSpeed;
    }

    public virtual void DestroyEnemy()
    {
            Destroy(Instantiate(explosion, transform.position, transform.rotation), 3f);
            //AudioSource.PlayClipAtPoint(explosionSound, transform.position);//звук взрыва уже есть в префабе взрыва
            Destroy(gameObject);
    }

    public virtual void TakeDamage(int damage)
    {
        health -= damage;
        if(takeDamageSounds.Length!=0)
            AudioSource.PlayClipAtPoint(takeDamageSounds[Random.Range(0, takeDamageSounds.Length)], transform.position,1f);

        if (health <= 0) DestroyEnemy();
    }

    void OnTriggerEnter(Collider myColllider)
    {
        //Debug.Log(myColllider.name);
        if (myColllider.tag== "Weapon")
        {
            Weapon wp = myColllider.GetComponent<Weapon>();
            TakeDamage(wp.damage);
            if(wp.mustBeDestroyed) Destroy(myColllider.gameObject);            
        }

    }

    void OnParticleCollision(GameObject particle)
    {
        //Debug.Log(particle.name);
        if (particle.name.Contains("Fireball"))
        {
            Weapon wp = particle.GetComponent<Weapon>();
            TakeDamage(wp.damage);
            if (wp.mustBeDestroyed) Destroy(particle.gameObject,5);
        }
    }

}
