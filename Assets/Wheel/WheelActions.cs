using UnityEngine;
using System.Collections;

public class WheelActions : MonoBehaviour {

    public bool wheelused = false;
    public int wheellevel = 1;
    float speed = 100;
    float maxspeed = 2;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
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

        if (wheelused == false)
        {
            return;
        }

        Rigidbody2D rb = GameObject.Find("Ship").GetComponent<Rigidbody2D>();

        if (Input.GetButton("Horizontal1"))
        {
            rb.angularVelocity = Input.GetAxis("Horizontal1") * -20.0f * rb.velocity.magnitude;
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

        if (Input.GetAxis("Vertical1") > 0.0f)
        {
            rb.AddForce(transform.up * Time.deltaTime * speed);

            if (rb.velocity.magnitude > maxspeed)
            {
                rb.velocity = rb.velocity.normalized * maxspeed;
            }
        }
        else
        if ((Input.GetAxis("Vertical1") < 0.0f) && (rb.velocity.magnitude < 0.1f))
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
    }
}
