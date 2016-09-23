using UnityEngine;
using System.Collections;

public class StarboardCannonActions : MonoBehaviour {

    public bool starboardcannonused = false;
    public int starboardcannonpowerlevel = 1;
    public int starboardcannonfireratelevel = 1;
    public Transform starboardcannonball;
    float rotspeed = 100;
    float lastfiretime = 0;

    // Use this for initialization
    void Start () {
        // Read previous powerlevel;
        // Display sprites according to upgradelevel;
    }

    // Update is called once per frame
    void Update () {
        // Change sprites according to powerlevel if necessary;
        switch (starboardcannonpowerlevel)
        {
            case 1:
                GetComponent<SpriteRenderer>().sprite = Resources.Load("Cannon_lvl1", typeof(Sprite)) as Sprite;
                break;
            case 2:
                GetComponent<SpriteRenderer>().sprite = Resources.Load("Cannon_lvl2", typeof(Sprite)) as Sprite;
                break;
            case 3:
                GetComponent<SpriteRenderer>().sprite = Resources.Load("Cannon_lvl3", typeof(Sprite)) as Sprite;
                break;
            case 4:
                GetComponent<SpriteRenderer>().sprite = Resources.Load("Cannon_lvl4", typeof(Sprite)) as Sprite;
                break;
            case 5:
                GetComponent<SpriteRenderer>().sprite = Resources.Load("Cannon_lvl5", typeof(Sprite)) as Sprite;
                break;
        }

        if (starboardcannonused == false)
        {
            return;
        }

        // Traverse cannon
        transform.Rotate(0, 0, Input.GetAxis("Horizontal2") * Time.deltaTime * rotspeed * -1);
        if (transform.localEulerAngles.z < 120)
        {
            transform.localRotation = Quaternion.Euler(0, 0, 120);
        }
        if (transform.localEulerAngles.z > 240)
        {
            transform.localRotation = Quaternion.Euler(0, 0, 240);
        }

        // Determine fire rate according to cannon fire rate level
        float firerate = 0;
        switch (starboardcannonfireratelevel)
        {
            case 1:
                firerate = 1;
                break;
            case 2:
                firerate = 1 / 1.5f;
                break;
            case 3:
                firerate = 1 / 2f;
                break;
            case 4:
                firerate = 1 / 2.5f;
                break;
            case 5:
                firerate = 1 / 3f;
                break;
        }

        // Fire cannon
        if (Input.GetButtonDown("Fire2"))
        {
            float currenttime = Time.time;
            if ((currenttime - lastfiretime) >= firerate)
            {
                Transform mycannonball = (Transform)Instantiate(starboardcannonball, GameObject.Find("StarboardCannonTip").transform.position, transform.rotation);
                mycannonball.GetComponent<Shoot>().level = starboardcannonpowerlevel;
                lastfiretime = currenttime;
            }
        }
    }
}
