﻿using UnityEngine;
using System.Collections;

public class MoveCannon : MonoBehaviour
{

    public GameObject[] charArray;
    public GameObject cannon;
    public Transform cannonSpawn;

    public float fireRate;

    private float nextFire;
    private GameObject cannonInstance;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        for (int k = 0; k < 2; k++)
        {
            if (charArray[k].GetComponent<MoveChar>().isContactCannonLeft)
            {

                int i = charArray[k].GetComponent<MoveChar>().playerNo;

                if (Input.GetAxisRaw("Horizontal" + i) < -0.5f)
                    RotateLeft();

                if (Input.GetAxisRaw("Horizontal" + i) > 0.5f)
                    RotateRight();

                if ((Input.GetAxisRaw("Interact" + i) > 0.5f) && (Time.time > nextFire))
                {
                    nextFire = Time.time + fireRate;
                    cannonInstance = Instantiate(cannon, cannonSpawn.position, cannonSpawn.rotation) as GameObject;
                    Destroy(cannonInstance, 2);
                }
            }
        }
        
    }

    void RotateLeft()
    {
    
        //Debug.Log("transform angles: " + transform.localEulerAngles.z);
        if ((transform.localEulerAngles.z <= 150) && (transform.localEulerAngles.z >= 25))
            transform.Rotate(Vector3.forward * 2);
    }

    void RotateRight()
    {
        //Debug.Log("transform angles: " + transform.localEulerAngles.z);
        if ((transform.localEulerAngles.z <= 155) && (transform.localEulerAngles.z >= 30))
        transform.Rotate(Vector3.forward * -2);
    }

}