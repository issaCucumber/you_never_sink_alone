using UnityEngine;
using System.Collections;
using TeamUtility.IO;

public class DynamiteActions : MonoBehaviour
{

    public int dynamitelevel = 1;
    public float percent = 100.0f;
    public Transform superattack;
    private int cooldown = 300; // 300 secs
    private float lastfiretime = -300;

	public Stat energy;

    public GameObject[] charArray;
    public GameObject Dynamite;
    public bool[] activation;

    // Use this for initialization
    void Start()
    {
		energy.Initialize ();
    }

    // Update is called once per frame
    void Update()
    {
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

        float currenttime = Time.time;
        if ((currenttime - lastfiretime) < cooldown)
        {
            return;
        }

        for (int k = 0; k < charArray.Length; k++)
        {
            if (charArray[k].GetComponent<MoveChar>().isContactDynamite)
            {
                //get character no
                int i = charArray[k].GetComponent<MoveChar>().playerNo;

				if (InputManager.GetAxis ("Interact" + i) > 0.0f) {
					activation [i] = true;
				} 
            }
        }
        
        // Activate dynamite
        if (countActivated(activation) > 1)
        {
            //Transform mysuperattack = (Transform)Instantiate(superattack, Dynamite.transform.position, transform.rotation);
            GameObject dynamiteInstance = Instantiate(superattack, Dynamite.transform.position, transform.rotation) as GameObject;
            deactivate(activation);
            lastfiretime = Time.time;
			energy.CurrentVal = 0;
        }

		if (energy.CurrentVal != energy.MaxVal) {

			float t = Time.time - lastfiretime;    //amt of seconds has past since trigger
			energy.CurrentVal = (t / cooldown) * energy.MaxVal;
		}
    }


    public static int countActivated(bool[] activation)
    {
        int count = 0;
        for (int i = 0; i < activation.Length; i++)
        {
            if (activation[i] == true)
                count++;
        }
        return count;
    }

    public void deactivate(bool[] activation)
    {
        for (int i = 0; i < activation.Length; i++)
        {
            activation[i] = false;
        }
    }
}

