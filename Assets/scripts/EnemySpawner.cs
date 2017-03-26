using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {

    public GameObject gamePropObj;
    public GameObject[] enemies;
    //public GameObject[] bonuses;


    public GameObject[] asteroids; //холдеры бонусов
    public GameObject[] lightningSparks; //свечение бонусов
    public GameObject[] weaponModels; //изображение внутри бонусов


    public float startWait, spawnWait, waveWait;
    public int minHazardCount;
    public bool spawn;
    public float hazardSpeed;

    gameProp gp;
    float maxhazardSpeed, deltahazardSpeed;
    float speedOfChange;
    float minwaveWait, minspawnWait;
    int[] hazardCountRange;

    // Use this for initialization
    void Start () {

        gp = gamePropObj.GetComponent<gameProp>();
        setLevel();
        spawn = true;
        StartCoroutine(SpawnEnemyWaves());

    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void setLevel()
    {
        //set level level
        minwaveWait = gp.minwaveWait;
        minspawnWait = gp.minspawnWait;
        speedOfChange = gp.speedOfChange;
        hazardCountRange = new int[2];
        hazardCountRange[0] = gp.hazardCountRange[0];
        hazardCountRange[1] = gp.hazardCountRange[1];
        hazardSpeed = gp.hazardSpeed;
        maxhazardSpeed = gp.maxhazardSpeed;
        deltahazardSpeed = gp.deltahazardSpeed;
    }


    IEnumerator SpawnEnemyWaves()
    {
        yield return new WaitForSeconds(startWait);

        while (spawn)
        {
            for (int i = 0; i < minHazardCount; i++)
            {
                GameObject instance= Instantiate(enemies[Random.Range(0, enemies.Length)]);
                instance.transform.position = new Vector3(gameObject.transform.position.x + Random.Range(-2f, 2f), instance.transform.position.y,
                    gameObject.transform.position.z);

                //Destroy(instance, 3);

                //Debug.Log(hazards[Random.Range(0,hazards.Length)].transform.rotation+"  "+hazards.transform.position);
                if (spawn == false) break;
                yield return new WaitForSeconds(spawnWait);
            }

            //SpawnAsteroid(true,lightningSparks[2],null);

            minHazardCount = minHazardCount + Random.Range(hazardCountRange[0], hazardCountRange[1]);
            if (waveWait > minwaveWait)
            {
                waveWait -= speedOfChange;
            }
            if (spawnWait > minspawnWait)
            {
                spawnWait -= speedOfChange;
            }
            if (hazardSpeed < maxhazardSpeed)
            {
                hazardSpeed += deltahazardSpeed;

            }
            yield return new WaitForSeconds(waveWait);

        }

    }

    void SpawnAsteroid(bool isBonus, GameObject lightningSpark, GameObject weaponModel)
    {
        GameObject asteroid = Instantiate(asteroids[0]);
        asteroid.transform.position= new Vector3(gameObject.transform.position.x + Random.Range(-2f, 2f), asteroid.transform.position.y,
                    gameObject.transform.position.z);

        if (isBonus)
        {
            asteroid.GetComponent<Asteroid>().bonusPref = lightningSpark;
        }
    }
}
