using UnityEngine;
using System.Collections;
using TeamUtility.IO;

public class StarboardCannonActions : MonoBehaviour {

    public GameObject[] charArray;
    public int starboardcannonpowerlevel = 1;
    public int starboardcannonfireratelevel = 1;
    //public Transform starboardcannonball;
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

        for (int k = 0; k < charArray.Length; k++)
        {
            if (charArray[k].GetComponent<MoveChar>().isContactCannonRight)
            {

                int i = charArray[k].GetComponent<MoveChar>().playerNo;

                transform.Rotate(0, 0, InputManager.GetAxis("Horizontal" + i) * Time.deltaTime * rotspeed * -1);
                if (transform.localEulerAngles.z < 140)
                {
                    transform.localRotation = Quaternion.Euler(0, 0, 140);
                }
                if (transform.localEulerAngles.z > 230)
                {
                    transform.localRotation = Quaternion.Euler(0, 0, 230);
                }

                if (InputManager.GetButtonDown("Interact" + i))
                {
                    float currenttime = Time.time;
                    if ((currenttime - lastfiretime) >= firerate)
                    {
                        //Transform mycannonball = (Transform)Instantiate(portcannonball, GameObject.Find("cannonSpawn").transform.position, transform.rotation);
                        cannonInstance = Instantiate(myCannonBall, CannonSpawn.position, CannonSpawn.rotation) as GameObject;
                        cannonInstance.GetComponent<Shoot>().level = starboardcannonpowerlevel;
                        lastfiretime = currenttime;
                    }
                }
            }
        }

    }
}
