using UnityEngine;
using System.Collections;

public class Bonus : MonoBehaviour {

    public GameObject bonusModelPref; //что будет отображаться внутри свечения

    bool isFadind = false;
    float currentVelocity = 0f;

    // Use this for initialization
    void Start () {

	}

    public void DetachFromAsteroid()
    {
        if (bonusModelPref != null)
        {
            GameObject bonusModel = Instantiate(bonusModelPref);

            Collider bonusModelCol = bonusModel.GetComponent<Collider>();
            if (bonusModelCol != null) bonusModelCol.enabled = false;            

            bonusModel.transform.position = transform.position;
            bonusModel.transform.SetParent(gameObject.transform);

        }
    }

    
    void OnTriggerEnter(Collider col)
   {
       if (col.name == "cat")
       {
            Destroy(gameObject);
       }
   }

    void Update () {
        
	}
}
