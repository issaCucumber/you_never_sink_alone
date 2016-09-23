﻿using UnityEngine;
using System.Collections;

public class WheelActions : MonoBehaviour
{

    //public bool isContactWheel = false;
    public int wheellevel = 1;
    float speed = 1000;
    float maxspeed = 2;

    public GameObject[] charArray;
    public GameObject Ship;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        // Change sprites according to wheellevel if necessary;
        switch (wheellevel)
        {
            case 1:
                GetComponent<SpriteRenderer>().sprite = Resources.Load("Wheel_lvl1", typeof(Sprite)) as Sprite;
                maxspeed = 2;
                break;
            case 2:
                GetComponent<SpriteRenderer>().sprite = Resources.Load("Wheel_lvl2", typeof(Sprite)) as Sprite;
                maxspeed = 4;
                break;
            case 3:
                GetComponent<SpriteRenderer>().sprite = Resources.Load("Wheel_lvl3", typeof(Sprite)) as Sprite;
                maxspeed = 6;
                break;
            case 4:
                GetComponent<SpriteRenderer>().sprite = Resources.Load("Wheel_lvl4", typeof(Sprite)) as Sprite;
                maxspeed = 8;
                break;
            case 5:
                GetComponent<SpriteRenderer>().sprite = Resources.Load("Wheel_lvl5", typeof(Sprite)) as Sprite;
                maxspeed = 10;
                break;
        }

        Rigidbody2D rb = Ship.GetComponent<Rigidbody2D>();

        for (int k = 0; k < 2; k++)
        {

            //move character along with ship
            charArray[k].GetComponent<Rigidbody2D>().velocity = rb.velocity;


            if (charArray[k].GetComponent<MoveChar>().isContactWheel)
            {
                //get character no
                int i = charArray[k].GetComponent<MoveChar>().playerNo;

                //move up
                if (Input.GetAxis("Interact" + i) > 0.0f)
                {
                    rb.AddForce(transform.up * Time.deltaTime * speed);

                    if (rb.velocity.magnitude > maxspeed)
                    {
                        rb.velocity = rb.velocity.normalized * maxspeed;
                    }
                }
                else
                if ((Input.GetAxis("Interact" + i) < 0.0f) && (rb.velocity.magnitude < 0.1f))
                {
                    rb.AddForce(transform.up * Time.deltaTime * speed * -0.5f);

                    if (rb.velocity.magnitude > 0.1f)
                    {
                        rb.velocity = rb.velocity.normalized * 0.1f;
                    }
                }
                else
                if (rb.velocity.magnitude > 0.0f)
                {
                    rb.velocity = rb.velocity * (1.0f - (1.5f * Time.deltaTime));
                }

                //rotation
               /* if (Input.GetButton("Horizontal" + i))
                {
                    //rb.AddTorque(Input.GetAxis("Horizontal" + i) *-5 * rb.velocity.magnitude);
                    rb.angularVelocity = Input.GetAxis("Horizontal" + i) * -20.0f * rb.velocity.magnitude;
                    //charArray[0].GetComponent<Rigidbody2D>().angularVelocity = rb.angularVelocity;
                    //Debug.Log("char1 AV = " + charArray[0].GetComponent<Rigidbody2D>().angularVelocity);
                }
                else
                {
                    if (rb.angularVelocity > 0.0f)
                    {
                        rb.angularVelocity -= 1.0f;
                    }
                    else
                    if (rb.angularVelocity < 0.0f)
                    {
                        rb.angularVelocity += 1.0f;
                    }
                }
                */

                if (Input.GetAxis("Horizontal" + i) * rb.velocity.magnitude < -0.5f)
                    Ship.transform.Rotate(Vector3.forward * 15 * Time.deltaTime);

                if (Input.GetAxis("Horizontal" + i) * rb.velocity.magnitude > 0.5f)
                    Ship.transform.Rotate(Vector3.forward * -15 * Time.deltaTime);
                

            }
        }
    }

}
