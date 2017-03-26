using UnityEngine;
using System.Collections;

public class gameProp : MonoBehaviour {

	public float minwaveWait = 0.5f;
	public float minspawnWait = 0.5f;
	public float speedOfChange = 0.25f;
	public int[] hazardCountRange= {2,10};
	//static public float hazardCountRange [0] = 2;
	//static public float hazardCountRange [1] = 10;
	public float hazardSpeed = 3f;
	public float maxhazardSpeed = 8f;
	public float deltahazardSpeed = 0.5f;
	public float maxPlayerHealth=150f;
	public float maxScore = 5000;
	public string diff;

	public bool controllerType = true; //accelerometer or screen keys. false - accelerometer
}
