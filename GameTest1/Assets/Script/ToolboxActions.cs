﻿using UnityEngine;
using System.Collections;

public class ToolboxActions : MonoBehaviour {

    public GameObject[] charArray;
    public int toolboxlevel = 1;
    private int repairrate = 2;
    private float lastfiretime = 0;
    public GameObject Ship;

    // Use this for initialization
    void Start () {
	
	}

    // Update is called once per frame
    void Update()
    {
        // Change sprites according to toolboxlevel if necessary;
        switch (toolboxlevel)
        {
            case 1:
                GetComponent<SpriteRenderer>().sprite = Resources.Load("Toolbox_lvl1", typeof(Sprite)) as Sprite;
                repairrate = 2;
                break;
            case 2:
                GetComponent<SpriteRenderer>().sprite = Resources.Load("Toolbox_lvl2", typeof(Sprite)) as Sprite;
                repairrate = 4;
                break;
            case 3:
                GetComponent<SpriteRenderer>().sprite = Resources.Load("Toolbox_lvl3", typeof(Sprite)) as Sprite;
                repairrate = 6;
                break;
            case 4:
                GetComponent<SpriteRenderer>().sprite = Resources.Load("Toolbox_lvl4", typeof(Sprite)) as Sprite;
                repairrate = 8;
                break;
            case 5:
                GetComponent<SpriteRenderer>().sprite = Resources.Load("Toolbox_lvl5", typeof(Sprite)) as Sprite;
                repairrate = 10;
                break;
        }

        // Repair hull
        for (int k = 0; k < charArray.Length; k++)
        {

            if (charArray[k].GetComponent<MoveChar>().isContactWheel)
            {

                int i = charArray[k].GetComponent<MoveChar>().playerNo;
                if (Input.GetButtonDown("Interact" + i))
                {
                    float currenttime = Time.time;
                    if ((currenttime - lastfiretime) >= 1.0f)
                    {
                        Ship.GetComponent<ShipActions>().hullcurrent += repairrate;
                        if (Ship.GetComponent<ShipActions>().hullcurrent > Ship.GetComponent<ShipActions>().hullmax)
                        {
                            Ship.GetComponent<ShipActions>().hullcurrent = Ship.GetComponent<ShipActions>().hullmax;
                        }
                        lastfiretime = currenttime;
                    }
                }
            }
        }
    }
}
