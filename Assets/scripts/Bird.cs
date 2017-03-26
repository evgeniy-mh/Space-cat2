using UnityEngine;
using System.Collections;

public class Bird : Enemy {

    public Transform playerTransform;
    public AudioClip ExplosionSound;

    bool CanFolLowPlayer;
    bool stopFollow = false;
    Vector3 VectOfTarget;
    float step;
    // Use this for initialization
    void Start () {
        playerTransform = GameObject.Find("cat").transform;

    }
	
	// Update is called once per frame
	void FixedUpdate () {

        float angle = Vector3.Angle(Vector3.back, playerTransform.position - transform.position);

        if (angle <= 30 && !stopFollow) CanFolLowPlayer = true;
        else CanFolLowPlayer = false;

        if (CanFolLowPlayer)
        {
            VectOfTarget = new Vector3(playerTransform.position.x,transform.position.y, playerTransform.position.z);
            transform.LookAt(VectOfTarget);

            step = moveSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, VectOfTarget, step);
            //Debug.DrawRay(transform.position,VectOfTarget);
        }
        else
        {
            rb.velocity = transform.forward * moveSpeed;
            stopFollow = true;
        }

    }

    public void DestroyImm()
    {
        Destroy(Instantiate(explosion, transform.position, transform.rotation), 5f);
        AudioSource.PlayClipAtPoint(ExplosionSound, transform.position);
        Destroy(gameObject);
    }

    public override void DestroyEnemy()
    {
        stopFollow = true;
        GetComponentInChildren<Collider>().isTrigger = true;
        StartCoroutine(Destruction());
    }

    IEnumerator Destruction()
    {
        rb.AddTorque(new Vector3(Random.Range(10,100),0, Random.Range(10, 100)));
        yield return new WaitForSeconds(2f);

        Destroy(Instantiate(explosion, transform.position, transform.rotation), 5f);
        AudioSource.PlayClipAtPoint(ExplosionSound, transform.position);
        Destroy(gameObject);
    }

}
