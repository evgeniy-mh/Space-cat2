using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AntFormation : MonoBehaviour {

    public GameObject antPrefab;
    public int antCount=3;
    GameObject[] antsObj;
    List<Ant> ants;

	// Use this for initialization
	void Start () {
        antsObj = new GameObject[antCount];
        ants = new List<Ant>();
        CreateFormation();
        Destroy(gameObject, 10f);
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void CreateFormation()
    {
        float temp_pos = 0;
        float pos_offset = 1f;
        for(int i = 0; i < antCount; i++)
        {
            Vector3 pos = new Vector3(transform.position.x+temp_pos, transform.position.y, transform.position.z);
            GameObject temp = Instantiate(antPrefab);
            temp.transform.position = pos;
            temp_pos += pos_offset;

            temp.transform.parent = transform;
            antsObj[i] = temp;
            //ants[i] = temp.GetComponent<Ant>();
            ants.Add(temp.GetComponent<Ant>());
            temp.GetComponent<Ant>().id = i;
        }
    }

    public void RegisterDead(/*int deadId*/ Ant ant)
    {
        //ants.Remove(ant);
        //ant.DestroyEnemy();
        StartCoroutine( DestroyFormation());
    }

    IEnumerator DestroyFormation()
    {
        yield return new WaitForSeconds(0.1f);
        foreach (Ant a in ants)
        {
            if(a!=null)    a.OMG();
        }

        foreach (Ant a in ants)
        {
            if (a != null) a.DestroyEnemy();
            yield return new WaitForSeconds(1f-Random.Range(0f,1f));
        }
        
        Destroy(gameObject);
    }
}
