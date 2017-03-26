using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    public float xMin, xMax, zMin, zMax;
    public float speed;
    public float fireRate = 0.0f;
    public AudioClip[] PlayerFireSounds;
    public GameObject[] Weapons;
    public GameObject[] ExtWeapons;
    public int currentWeaponId; 
    public Transform shotSpawn;
    public int health=100;
    public GameObject collectBonusSFX;
    public Transform extWeaponHolder;//это cat_body

    public AudioClip PurrSound; //когда взял бонус
    //public GameObject[] extWeaponsPrefabs;

    GameObject currentExtWeapon;
    WeaponLauncher extWeapon;
    float nextFire = 0.5f;
    string controllerType;
    Animator myAnim;
    Rigidbody rb;
    bool isEquipedExtWeapon = false;//для внешних пушек

    // Use this for initialization
    void Start () {
        controllerType = "keyboard";

        myAnim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody>();

        //EquipExtWeapon(extWeaponsPrefabs[0]);
        fireRate = Weapons[currentWeaponId].GetComponent<Weapon>().fireRate;

    }
	
	// Update is called once per frame
	void Update () {
	
	}


    void FixedUpdate()
    {
        //анимация
        if (Input.GetAxis("Vertical") > 0)
        {
            myAnim.SetBool("movingForward", true);
        }
        else myAnim.SetBool("movingForward", false);
        if (Input.GetAxis("Vertical") < 0)
        {
            myAnim.SetBool("movingBackward", true);
        }
        else myAnim.SetBool("movingBackward", false);


        //движение
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        if (controllerType == "keyboard")
        {
            Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
            rb.velocity = movement * speed;

            rb.position = new Vector3
            (
                Mathf.Clamp(rb.position.x, xMin, xMax),
                0.0f,
                Mathf.Clamp(rb.position.z, zMin, zMax)
            );


            //if (Input.GetKey(KeyCode.Q) && (Time.time > nextFire)) Fire();
            if (Input.GetButton("Fire1") && (Time.time > nextFire)) Fire();
        }
    }


    void Fire()
    {
        if (isEquipedExtWeapon)
        {
            extWeapon.Shoot();
        }
        else
        if (currentWeaponId == 1) //если это молния
        {
            nextFire = Time.time + fireRate;
            GameObject clone = Instantiate(Weapons[currentWeaponId], shotSpawn.position, shotSpawn.rotation) as GameObject;
            //clone.transform.parent = gameObject.transform;
        }
        else {

            nextFire = Time.time + fireRate;
            GameObject clone = Instantiate(Weapons[currentWeaponId], shotSpawn.position, shotSpawn.rotation) as GameObject;
            AudioSource.PlayClipAtPoint(PlayerFireSounds[Random.Range(0, PlayerFireSounds.Length)], shotSpawn.position);
        }
    }

    void EquipWeapon(byte id)
    {
        if (isEquipedExtWeapon) DeleteExtWeapon();
        currentWeaponId = id;
        fireRate = Weapons[currentWeaponId].GetComponent<Weapon>().fireRate;
    }

    public void EquipExtWeapon(GameObject newExtWeapon)
    {
        if (isEquipedExtWeapon)
        {
            /*if (newExtWeapon != currentExtWeapon)         //не тестилось еще
            {
                DeleteExtWeapon();
                EquipExtWeapon(newExtWeapon);
            }*/
        }
        else
        {
            isEquipedExtWeapon = true;
            currentExtWeapon = (GameObject)Instantiate(newExtWeapon, extWeaponHolder.position,Quaternion.Euler(0f,0f,0f));
            Vector3 weaponPosOffset = new Vector3(0.3f, 0.5f, 0.6f);
            currentExtWeapon.transform.localPosition = currentExtWeapon.transform.localPosition + weaponPosOffset;
            currentExtWeapon.transform.parent = extWeaponHolder;

            extWeapon = currentExtWeapon.GetComponent<WeaponLauncher>();
            nextFire = 0;
        }
    }

    public void DeleteExtWeapon()
    {
        DestroyObject(currentExtWeapon);
        isEquipedExtWeapon = false;
    }


    void OnTriggerEnter(Collider myColllider)
    {
        if (myColllider.name.Contains("dolphin"))
        {
            ChangeHealth(+20);

            GameObject temp = (GameObject)Instantiate(collectBonusSFX, transform.position, Quaternion.Euler(0f, -180f, 0f));
            temp.transform.parent = gameObject.transform;
            Destroy(temp, 3f);
            myColllider.gameObject.GetComponentInParent<Dolphin>().CollectMe();
            AudioSource.PlayClipAtPoint(PurrSound, transform.position);
        }

        if (myColllider.tag == "Bonus")
        {
            AudioSource.PlayClipAtPoint(PurrSound, transform.position);
            GameObject temp = (GameObject)Instantiate(collectBonusSFX, transform.position, Quaternion.Euler(0f, -180f, 0f));
            temp.transform.parent = gameObject.transform;
            Destroy(temp, 3f);

            Bonus b = myColllider.GetComponent<Bonus>();
            if (b.bonusModelPref != null)
            {
                if (b.bonusModelPref.name.Contains("Fireball")) EquipWeapon(2);
                if (b.bonusModelPref.name.Contains("shotgun")) EquipExtWeapon(ExtWeapons[0]);
                if (b.bonusModelPref.name.Contains("rocket2")) EquipExtWeapon(ExtWeapons[1]);



            }

        }

        if(myColllider.tag=="Enemy")
        {
            if (myColllider.name.Contains("bird")) myColllider.GetComponentInParent<Bird>().DestroyImm();

            ChangeHealth(-20);
            if(myColllider.name.Contains("asteroid")) myColllider.GetComponentInParent<Asteroid>().DestroyImm();
            else myColllider.GetComponentInParent<Enemy>().DestroyEnemy();

        }
    }


    void ChangeHealth(int value)
    {
        health += value;
        Debug.Log("health: " + health);
    }

    }
