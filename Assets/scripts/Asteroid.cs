using UnityEngine;
using System.Collections;

public class Asteroid : Enemy {

    public GameObject bonusPref;

    GameObject bonus;
    Vector3 randTorque;

	// Use this for initialization
	void Start () {

        if (bonusPref != null)
        {
            bonus = Instantiate(bonusPref);
            bonus.transform.position = transform.position;
            bonus.transform.SetParent(gameObject.transform);
        }

        StartSimpleMove();
        randTorque = Random.insideUnitSphere * Random.Range(10f, 100f);
        rb.AddTorque(randTorque);
	}


	
	// Update is called once per frame
	void Update () {
	
	}

    public override void DestroyEnemy()
    {
        if (bonus != null)
        {
            bonus.transform.parent = null;
            Rigidbody bonusRB = bonus.AddComponent<Rigidbody>();

            if (bonusRB != null)
            {
                bonusRB.useGravity = false;
                bonusRB.velocity = Vector3.back * moveSpeed;
                bonusRB.AddTorque(randTorque);
            }
            bonus.GetComponent<Bonus>().DetachFromAsteroid();
        }

        Destroy(Instantiate(explosion, transform.position, transform.rotation), 3f);
        //AudioSource.PlayClipAtPoint(explosionSound, transform.position);//звук взрыва уже есть в префабе взрыва
        Destroy(gameObject);
    }

    public void DestroyImm()
    {
        Destroy(Instantiate(explosion, transform.position, transform.rotation), 3f);
        //AudioSource.PlayClipAtPoint(explosionSound, transform.position);//звук взрыва уже есть в префабе взрыва
        Destroy(gameObject);
    }

   
}
