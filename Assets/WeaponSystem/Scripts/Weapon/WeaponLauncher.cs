using System;
using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;

[RequireComponent(typeof(AudioSource))]

public class WeaponLauncher : MonoBehaviour
{
	public Transform[] MissileOuter;
	public GameObject Missile;
	public float FireRate = 0.1f;
	public float Spread = 1;
	public float ForceShoot = 8000;
	public int NumBullet = 1;
	//public int Ammo = 10;
	///public int AmmoMax = 10;
	//public bool InfinityAmmo = false;
	//public float ReloadTime = 1;
	//public bool ShowHUD = true;
	///public Texture2D TargetLockOnTexture;
	//public Texture2D TargetLockedTexture;
	//public float DistanceLock = 200;
	//public float TimeToLock = 2;
	//public float AimDirection = 0.8f;
	//public bool Seeker;
	//public GameObject Shell;
	//public float ShellLifeTime = 4;
	//public Transform[] ShellOuter;
	//public int ShellOutForce = 300;
	//public GameObject Muzzle;
	//public float MuzzleLifeTime = 2;
	//public AudioClip[] SoundGun;
	//private float timetolockcount = 0;
	private float nextFireTime = 0;
	//private GameObject target;
	//private Vector3 torqueTemp;
	//private float reloadTimeTemp;
	//private AudioSource audio;


    [HideInInspector]
    //public GameObject Owner;
    //[HideInInspector]
    //public GameObject Target;
    //public string[] TargetTag = new string[1] { "Enemy" };
    //public bool RigidbodyProjectile;
    //public Vector3 TorqueSpeedAxis;
    //public GameObject TorqueObject;

    AudioSource asc;
    public AudioClip shotSound;

    void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        asc = GetComponent<AudioSource>();
    }

    private void Update ()
	{
        //if (Input.GetKeyDown(KeyCode.Space)) Shoot();    	
	}
	
	private int currentOuter = 0;

	public void Shoot ()
	{

        
			if (Time.time > nextFireTime + FireRate) {
            asc.PlayOneShot(shotSound);
            nextFireTime = Time.time;
				//torqueTemp = TorqueSpeedAxis;
				Vector3 missileposition = this.transform.position;
				Quaternion missilerotate = this.transform.rotation;

			
				for (int i = 0; i < NumBullet; i++) {
					if (Missile) {
                    //Vector3 spread = new Vector3 (Random.Range (-Spread, Spread), Random.Range (-Spread, Spread), Random.Range (-Spread, Spread)) / 100;
                    Vector3 spread = new Vector3(Random.Range(-Spread, Spread), 0f, Random.Range(-Spread, Spread)) / 100;
                    Vector3 direction = this.transform.forward + spread;				
					
						GameObject bullet = (GameObject)Instantiate (Missile, missileposition, missilerotate);
					
						/*if (bullet.GetComponent<DamageBase> ()) {
							bullet.GetComponent<DamageBase> ().Owner = Owner;
						}
						if (bullet.GetComponent<WeaponBase> ()) {
							bullet.GetComponent<WeaponBase> ().Owner = Owner;
							bullet.GetComponent<WeaponBase> ().Target = target;
						}*/
						bullet.transform.forward = direction;
						//if (RigidbodyProjectile) {
							if (bullet.GetComponent<Rigidbody>()) {
								/*if (Owner != null && Owner.GetComponent<Rigidbody>()) {
									bullet.GetComponent<Rigidbody>().velocity = Owner.GetComponent<Rigidbody>().velocity;
								}*/
								bullet.GetComponent<Rigidbody>().AddForce (direction * ForceShoot);	
							}
						//}
					
					}
				}
			
				nextFireTime += FireRate;
			}
		} 
		
	}
