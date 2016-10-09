using UnityEngine;
using System.Collections;

public class ShipActions : MonoBehaviour {

	[SerializeField]
	private Stat health;

	[SerializeField]
	private Prestige prestige;

	private float startTime;
	//public bool shocknow;//TODO set shock to false after 5s
	public bool hypnotizenow;//TODO set shock to false after 5s


 	public int hullmax = 100;
    public int hullcurrent = 100;
    //public int prestige = 0;
    public int hulllevel = 1;
    public bool shocknow = false;
    public bool isShocked = false;
    private float shockstarttime;
	public bool isHynotized = false;
	private float hynotizestarttime;
    public GameObject shipshock;

	private void Awake ()
	{
		health.MaxVal = hullmax;
		health.CurrentVal = hullcurrent;
		prestige.Initialize();
	}

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (hypnotizenow) {
//			transform.gameObject.GetComponent<Renderer> ().material.color = Color.red;
			hypnotizenow = false;
			hynotizestarttime = Time.time;
			if (isHynotized == false)
			{
				isHynotized = true;
				// Show ship_shock animation
//				shipshock.SetActive(true);
			}

		}

		if (Time.time - hynotizestarttime >= 5) // 5 secs since hypnotized
		{
			isHynotized = false;
			// Hide ship_shock animation
//			shipshock.SetActive(false);
		}

		if (shocknow == true) // shock started
        {
            shocknow = false;
            shockstarttime = Time.time;
            if (isShocked == false)
            {
                isShocked = true;
                // Show ship_shock animation
                shipshock.SetActive(true);
            }
        }

        if (Time.time - shockstarttime >= 5) // 5 secs since shocked
        {
            isShocked = false;
            // Hide ship_shock animation
            shipshock.SetActive(false);
        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        //        if (coll.gameObject.tag == "Enemy")
        //            coll.gameObject.SendMessage("ApplyDamage", 10);
        hullcurrent -= Mathf.CeilToInt(rb.velocity.magnitude);
    }
}
