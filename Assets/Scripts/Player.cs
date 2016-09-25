using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    [SerializeField]
    private Stat health;

    [SerializeField]
    private Stat energy;

    private float startTime;

    [SerializeField]
    private float rechargeTiming;

	private void Awake ()
    {
        health.Initialize();
        energy.Initialize();
	}
	
	// Update is called once per frame
	void Update () {

        if(Input.GetKeyDown(KeyCode.Q))
        {
            health.CurrentVal -= 10;
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            health.CurrentVal += 10;
        }

        if (energy.CurrentVal == energy.MaxVal)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                Debug.Log("Enter here");
                startTime = Time.time;
                energy.CurrentVal -= 10;
            }
        }
        else
        {
            Debug.Log("Recharging");
            //for eg lv 1 take 10s 
            float t = Time.time - startTime;    //amt of seconds has past since trigger
            energy.CurrentVal = (t / rechargeTiming) * energy.MaxVal;
            Debug.Log("Current val: " + energy.CurrentVal );
        }

    }
}
