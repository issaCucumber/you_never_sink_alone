using UnityEngine;
using System.Collections;

public class PortCannonActions : MonoBehaviour {

    public GameObject[] charArray;
    public int portcannonpowerlevel = 1;
    public int portcannonfireratelevel = 1;
    //public Transform portcannonball;
    public Transform CannonSpawn;
    public GameObject myCannonBall;
    float rotspeed = 100;
    float lastfiretime = 0;

    GameObject cannonInstance;
    // Use this for initialization
    void Start () {
        // Read previous powerlevel;
        // Display sprites according to upgradelevel;
    }

    void Update () {
        // Change sprites according to powerlevel if necessary;
        switch (portcannonpowerlevel)
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

        // Determine fire rate according to cannon fire rate level
        float firerate = 0;
        switch (portcannonfireratelevel)
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

        for (int k = 0; k < charArray.Length; k++)
        {
            if (charArray[k].GetComponent<MoveChar>().isContactCannonLeft)
            {

                int i = charArray[k].GetComponent<MoveChar>().playerNo;

                transform.Rotate(0, 0, Input.GetAxis("Horizontal" + i) * Time.deltaTime * rotspeed * -1);
                if (transform.localEulerAngles.z > 40 && transform.localEulerAngles.z < 90)
                {
                    transform.localRotation = Quaternion.Euler(0, 0, 40);
                }
                if (transform.localEulerAngles.z > 270 && transform.localEulerAngles.z < 300)
                {
                    transform.localRotation = Quaternion.Euler(0, 0, 300);
                }

                if (Input.GetButtonDown("Interact" + i))
                {
                    float currenttime = Time.time;
                    if ((currenttime - lastfiretime) >= firerate)
                    {
                        //Transform mycannonball = (Transform)Instantiate(portcannonball, GameObject.Find("cannonSpawn").transform.position, transform.rotation);
                        cannonInstance = Instantiate(myCannonBall, CannonSpawn.position, CannonSpawn.rotation) as GameObject;
                        cannonInstance.GetComponent<Shoot>().level = portcannonpowerlevel;
                        lastfiretime = currenttime;
                    }
                }
            }
        }

    }
}
