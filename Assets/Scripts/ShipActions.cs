using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ShipActions : MonoBehaviour {

	[SerializeField]
	private Stat health;

	[SerializeField]
	private Prestige prestige;

	private float startTime;

 	public int hullmax = 100;
    public int hullcurrent = 100;
	public int crewtosave = 20;
	public int crewsaved = 0;
	public int prestigevalue = 0; // for enemies
    public bool shocknow = false;
    public bool isShocked = false;
    private float shockstarttime;
	public bool hypnotizenow = false;
	public bool isHypnotized = false;
	private float hypnotizestarttime;
    public GameObject shipshock;

    public int hulllevel = 1;
    public int portcannonpowerlevel = 1;
    public int portcannonfireratelevel = 1;
    public int starboardcannonpowerlevel = 1;
    public int starboardcannonfireratelevel = 1;
    public int wheellevel = 1;
    public int toolboxlevel = 1;
    public int dynamitelevel = 1;

	public bool isGodMode = false;

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
		if (isGodMode == true)
		{
			hullcurrent = hullmax;
		}

		switch (hulllevel)
        {
            case 1:
                hullmax = 100;
                break;
            case 2:
                hullmax = 125;
                break;
            case 3:
                hullmax = 150;
                break;
            case 4:
                hullmax = 175;
                break;
            case 5:
                hullmax = 200;
                break;
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

		if (hypnotizenow == true) // hynotize started
		{
			hypnotizenow = false;
			hypnotizestarttime = Time.time;
			if (isHypnotized == false)
			{
				isHypnotized = true;
			}
		}

		if (Time.time - hypnotizestarttime >= 5) // 5 secs since hypnotized
		{
			isHypnotized = false;
		}

        health.MaxVal = hullmax;
        health.CurrentVal = hullcurrent;

        // Increase prestige from enemies.
        prestige.PrestigeVal += prestigevalue;
        prestigevalue = 0;
        
		if ((Input.GetKey(KeyCode.RightControl) || Input.GetKey(KeyCode.LeftControl)) && Input.GetKeyDown(KeyCode.G))
		{
			// CTRL + G = God Mode
			isGodMode = !isGodMode;
		}

		if ((Input.GetKey(KeyCode.RightControl) || Input.GetKey(KeyCode.LeftControl)) && Input.GetKeyDown(KeyCode.T))
		{
			// CTRL + T = Teleport
			if (SceneManager.GetActiveScene ().name == "Training")
			{
				Vector3 newposition = new Vector3 (58.46f, 31.45f, 0f);
				transform.position = newposition;
			}
			else
			if (SceneManager.GetActiveScene ().name == "Level_1")
			{
				Vector3 newposition = new Vector3 (10.00f, 10.00f, 0f);
				transform.position = newposition;
			}
		}
	}

    public int GetCurrentPrestige()
    {
        return prestige.PrestigeVal;
    }

    public void SetCurrentPrestige(int value)
    {
        prestige.PrestigeVal = value;
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        //        if (coll.gameObject.tag == "Enemy")
        //            coll.gameObject.SendMessage("ApplyDamage", 10);

        if (coll.gameObject.tag == "rock")
        {
            hullcurrent -= Mathf.CeilToInt(rb.velocity.magnitude);
        }
    }
}
