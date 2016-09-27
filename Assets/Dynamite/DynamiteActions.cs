using UnityEngine;
using System.Collections;

public class DynamiteActions : MonoBehaviour {

    public bool dynamiteused = false;
    public bool p1activate = false;
    public bool p2activate = false;
    public int dynamitelevel = 1;
    public float percent = 100.0f;
    public Transform superattack;
    private int cooldown = 300; // 300 secs
    private float lastfiretime = -300;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        // Change sprites according to dynamitelevel if necessary;
        switch (dynamitelevel)
        {
            case 1:
                GetComponent<SpriteRenderer>().sprite = Resources.Load("Dynamite_lvl1", typeof(Sprite)) as Sprite;
                cooldown = 300;
                break;
            case 2:
                GetComponent<SpriteRenderer>().sprite = Resources.Load("Dynamite_lvl2", typeof(Sprite)) as Sprite;
                cooldown = 240;
                break;
            case 3:
                GetComponent<SpriteRenderer>().sprite = Resources.Load("Dynamite_lvl3", typeof(Sprite)) as Sprite;
                cooldown = 180;
                break;
            case 4:
                GetComponent<SpriteRenderer>().sprite = Resources.Load("Dynamite_lvl4", typeof(Sprite)) as Sprite;
                cooldown = 120;
                break;
            case 5:
                GetComponent<SpriteRenderer>().sprite = Resources.Load("Dynamite_lvl5", typeof(Sprite)) as Sprite;
                cooldown = 60;
                 break;
        }

        percent = (Time.time - lastfiretime) / cooldown * 100.0f;
        if (percent > 100.0f)
        {
            percent = 100.0f;
        }

        if (dynamiteused == false)
        {
            p1activate = false;
            p2activate = false;
            return;
        }

        float currenttime = Time.time;
        if ((currenttime - lastfiretime) < cooldown)
        {
            return;
        }

        // Check P1 activate
        if (Input.GetButtonDown("Fire1"))
        {
            p1activate = true;
        }

        // Check P2 activate
        if (Input.GetButtonDown("Fire2"))
        {
            p2activate = true;
        }

        // Activate dynamite
        if ((p1activate == true) && (p2activate == true))
        {
            Transform mysuperattack = (Transform)Instantiate(superattack, GameObject.Find("Dynamite").transform.position, transform.rotation);
            p1activate = false;
            p2activate = false;
            lastfiretime = Time.time;
        }
    }
}
