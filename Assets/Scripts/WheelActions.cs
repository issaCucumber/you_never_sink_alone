using UnityEngine;
using System.Collections;
using TeamUtility.IO;

public class WheelActions : MonoBehaviour
{

    //public bool isContactWheel = false;
    public int wheellevel = 1;
    public float speed = 1000;
    float maxspeed = 2;

    public GameObject[] charArray;
    public GameObject Ship;
    public GameObject waves;
    private Vector3 originalscale;
	public bool wheelUsed = false;

    // Use this for initialization
    void Start()
    {
        originalscale = waves.transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        wheellevel = Ship.GetComponent<ShipActions>().wheellevel;

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

        if (rb.velocity.magnitude > 0.3)
        {
            // show waves animation
            Vector3 scale = originalscale;
            scale.y += (rb.velocity.magnitude / 5.0f);
            waves.transform.localScale = scale;
            waves.SetActive(true);
        }
        else
        {
            // hide waves animation
            waves.transform.localScale = originalscale;
            waves.SetActive(false);
        }

        // Do nothing when ship is shocked
        if (Ship.GetComponent<ShipActions>().isShocked == true)
        {
            return;
        }

        for (int k = 0; k < charArray.Length; k++)
        {

            //move character along with ship
            charArray[k].GetComponent<Rigidbody2D>().velocity = rb.velocity;


            if (charArray[k].GetComponent<MoveChar>().isContactWheel)
            {
				wheelUsed = true;
                //get character no
                int i = charArray[k].GetComponent<MoveChar>().playerNo;

                //move up
                if (InputManager.GetAxis("Vertical" + i) > 0.0f)
                {
                    rb.AddForce(transform.up * Time.deltaTime * speed);

                    if (rb.velocity.magnitude > maxspeed)
                    {
                         rb.velocity = rb.velocity.normalized * maxspeed;
                    }
                }
                else
                if ((InputManager.GetAxis("Vertical" + i) < 0.0f) && (rb.velocity.magnitude < 0.1f))
                {
                    rb.AddForce(transform.up * Time.deltaTime * speed * -0.5f);

                    if (rb.velocity.magnitude > 0.1f)
                    {
                        rb.velocity = rb.velocity.normalized * 0.1f;
                    }
                }

                if (InputManager.GetAxis("Horizontal" + i) * rb.velocity.magnitude < -0.5f)
                    Ship.transform.Rotate(Vector3.forward * 10 * Time.deltaTime);

                if (InputManager.GetAxis("Horizontal" + i) * rb.velocity.magnitude > 0.5f)
                    Ship.transform.Rotate(Vector3.forward * -10 * Time.deltaTime);
                

            }
        }
    }

}
