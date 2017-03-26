using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Chimp : Enemy {

    bool isSimpleMove = false;
    bool isAdvMove = false;


    float chimpY;
    Vector3 nextPoint;
    Vector3 velocity = Vector3.zero;
    public byte sectors = 4;
    byte currentSector=0;

    float dz;

    void Start()
    {
        dz = -15f / sectors;
        chimpY = transform.position.y;
        nextPoint = transform.position;        

        StartAdvancedMove();        
    }

    override protected void StartSimpleMove()
    {
        isAdvMove = false;
        isSimpleMove = true;
    }

    void StartAdvancedMove()
    {
        isSimpleMove = false;
        isAdvMove = true;
    }

    float temp = 0;

    void Update()
    {
        if(isSimpleMove)
        rb.velocity = -Vector3.forward * moveSpeed * Mathf.Sin(Mathf.Clamp(transform.position.z,0.2f,1f));
        
        if (isAdvMove)
        {

            if (!ComparePosXZ(transform.position,nextPoint))
            { 
                transform.position = Vector3.SmoothDamp(transform.position, nextPoint, ref velocity, 1f);
            }
            else
            {
                nextPoint = new Vector3(Random.Range(-4f, 4f), chimpY, Random.Range(temp, temp + dz));
                temp += dz;

                if (currentSector == sectors) StartSimpleMove();
                else   currentSector++;
            }            
        }
    }

    bool ComparePosXZ(Vector3 a, Vector3 b)
    {
        if (Mathf.Floor(a.x) == Mathf.Floor(b.x) && Mathf.Floor(a.z) == Mathf.Floor(b.z)) return true;
        else return false;

    }

}
