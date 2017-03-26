using UnityEngine;
using System.Collections;

public class Ant : Enemy {

    public int id;

    AntFormation af;

    void Start()
    {
        af = GetComponentInParent<AntFormation>();
        StartSimpleMove();
    }

    public override void TakeDamage(int damage)
    {
        health -= damage;
        if (takeDamageSounds.Length != 0)
            AudioSource.PlayClipAtPoint(takeDamageSounds[Random.Range(0, takeDamageSounds.Length)], transform.position);

        if (health <= 0)
        {
            af.RegisterDead(/*id*/this);
            //DestroyEnemy();
        }
    }

    public override void DestroyEnemy()
    {
        af.RegisterDead(this);
        Destroy(Instantiate(explosion, transform.position, transform.rotation), 3f);
        Destroy(gameObject);
    }

    public void OMG()
    {
        GetComponentInChildren<Collider>().isTrigger = true;
        rb.velocity=new Vector3(Random.Range(0,moveSpeed),0f, Random.Range(0, moveSpeed));
        rb.AddTorque(Random.insideUnitSphere * 500f);
    }

}
