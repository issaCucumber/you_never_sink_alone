using UnityEngine;
using System.Collections;
using TeamUtility.IO;

public class DynamiteActions : MonoBehaviour
{

    public int dynamitelevel = 1;
    //public float percent = 100.0f;
    public Transform superattack;
    private int cooldown = 300; // 300 secs
    private float lastfiretime = -300;

	public Stat energy;

    public GameObject[] charArray;
    public GameObject Dynamite;
    public bool[] activation;
    public GameObject Ship;
    public GameObject glowBar;

    private float timePast;

    // Use this for initialization
    void Start()
    {
		energy.Initialize ();
        //need to initialise cooldown here when loading game
        timePast = cooldown; //remove later
        if (PlayerPrefs.GetInt(Constants.DEFEATDRAGON, 0) == 1)
        {
            timePast = PlayerPrefs.GetFloat(Constants.DYNAMITECOOLDOWN_TEMP, cooldown);
        }
    }

    public float GetTimePast()
    {
        return timePast;
    }

    // Update is called once per frame
    void Update()
    {
        dynamitelevel = Ship.GetComponent<ShipActions>().dynamitelevel;

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
        timePast += Time.deltaTime;
        //percent = (Time.time - lastfiretime) / cooldown * 100.0f;
        //if (percent > 100.0f)
        //{
        //    percent = 100.0f;
        //}

        //if (energy.CurrentVal != energy.MaxVal)
        //{

        //    //float t = Time.time - lastfiretime;    //amt of seconds has past since trigger

        //    energy.CurrentVal = (timePast / cooldown) * energy.MaxVal;
        //}

        if(timePast >= cooldown)
        {
            glowBar.SetActive(true);
            energy.CurrentVal = energy.MaxVal;
        }
        else
        {
            glowBar.SetActive(false);
            energy.CurrentVal = (timePast / cooldown) * energy.MaxVal;
        }

        //float currenttime = Time.time;
        //if ((currenttime - lastfiretime) < cooldown)
        //{
        //    return;
        //}

        if(timePast < cooldown)
        {
            return;
        }

        // Do nothing when ship is shocked
        if (Ship.GetComponent<ShipActions>().isShocked == true)
        {
            return;
        }

        for (int k = 0; k < charArray.Length; k++)
        {
            if (charArray[k].GetComponent<MoveChar>().isContactDynamite)
            {
                //get character no
                int i = charArray[k].GetComponent<MoveChar>().playerNo;

				if (InputManager.GetButtonDown ("Interact" + i)) {
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
            timePast = 0;
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

