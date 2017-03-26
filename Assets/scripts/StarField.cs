using UnityEngine;
using System.Collections;

public class StarField : MonoBehaviour {

    public float starFieldSpeed=1f;
    public float starField_distantSpeed=0.1f;

    private ParticleSystem[] ps;

	void Awake () {

        ps = GetComponentsInChildren<ParticleSystem>();
        foreach (ParticleSystem c in ps)
        {
            switch (c.name)
            {
                case "part_starField":
                    c.startSpeed = starFieldSpeed;
                    c.Play();
                    break;

                case "part_starField_distant":
                    c.startSpeed = starField_distantSpeed;
                    c.Play();
                    break;

                default: break;
            }
        }

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
