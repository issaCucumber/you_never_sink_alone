using UnityEngine;
using System.Collections;

public class WheelActions : MonoBehaviour
{

    //public bool isContactWheel = false;
    public int wheellevel = 1;
    float speed = 1000;
    float maxspeed = 2;

    public GameObject[] charArray;
	public GameObject Ship;
	bool hypnotized = false;
	Transform ship;

    // Use this for initialization
    void Start()
    {

		ship = GameObject.Find ("Ship").transform;
    }

    // Update is called once per frame
    void Update()
	{
		hypnotized = ship.GetComponent<ShipActions> ().hypnotize;

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

        for (int k = 0; k < charArray.Length; k++)
        {

            //move character along with ship
            charArray[k].GetComponent<Rigidbody2D>().velocity = rb.velocity;


            if (charArray[k].GetComponent<MoveChar>().isContactWheel)
            {
                //get character no
                int i = charArray[k].GetComponent<MoveChar>().playerNo;

                //move up
				if ( (hypnotized == true ? -1 : 1) * Input.GetAxis("Vertical" + i) > 0.0f)
                {
                    rb.AddForce(transform.up * Time.deltaTime * speed);

                    if (rb.velocity.magnitude > maxspeed)
                    {
                        rb.velocity = rb.velocity.normalized * maxspeed;
                    }
                }
                else
				if ( ((hypnotized == true ? -1 : 1) *Input.GetAxis("Vertical" + i) < 0.0f) && (rb.velocity.magnitude < 0.1f))
                {
                    rb.AddForce(transform.up * Time.deltaTime * speed * -0.5f);

                    if (rb.velocity.magnitude > 0.1f)
                    {
                        rb.velocity = rb.velocity.normalized * 0.1f;
                    }
                }

				if ( (hypnotized == true ? -1 : 1) * Input.GetAxis("Horizontal" + i) * rb.velocity.magnitude < -0.5f)
                    Ship.transform.Rotate(Vector3.forward * 10 * Time.deltaTime);

				if ( (hypnotized == true ? -1 : 1) * Input.GetAxis("Horizontal" + i) * rb.velocity.magnitude > 0.5f)
                    Ship.transform.Rotate(Vector3.forward * -10 * Time.deltaTime);
                

            }
        }
    }

}
