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
	public int crewtosave ;
	public int crewsaved ;
	public int prestigevalue = 0; // for enemies
	public bool shocknow = false;
	public bool isShocked = false;
	private float shockstarttime;
	public bool hypnotizenow = false;
	public bool isHypnotized = false;
	private float hypnotizestarttime;
	public GameObject shipshock;

	public int hulllevel;
	public int portcannonpowerlevel;
	public int portcannonfireratelevel;
	public int starboardcannonpowerlevel;
	public int starboardcannonfireratelevel;
	public int wheellevel;
	public int toolboxlevel;
	public int dynamitelevel;

	public bool isGodMode = false;
	public bool touchWhirlpool = false;

	public GameObject burning;
	public GameObject hole;
	private GameObject[] damagearray;
	private int damagearraysize = 0;

	private AudioSource shipcollidesound;

	private void Awake ()
	{
		health.MaxVal = hullmax;
		health.CurrentVal = hullcurrent;
		prestige.Initialize();

		damagearray = new GameObject[10];
	}

	// Use this for initialization
	void Start () {
		shipcollidesound = GetComponent<AudioSource> ();
		
		hulllevel = PlayerPrefs.GetInt(Constants.HULL, 1);
		toolboxlevel = PlayerPrefs.GetInt(Constants.TOOLBOX, 1);
		starboardcannonpowerlevel = PlayerPrefs.GetInt(Constants.STARBOARDPOWER, 1);
		starboardcannonfireratelevel = PlayerPrefs.GetInt(Constants.STARBOARDFIRERATE, 1);
		wheellevel = PlayerPrefs.GetInt(Constants.WHEEL, 1);
		dynamitelevel = PlayerPrefs.GetInt(Constants.DYNAMITE, 1);
		portcannonpowerlevel = PlayerPrefs.GetInt(Constants.PORTPOWER, 1);
		portcannonfireratelevel = PlayerPrefs.GetInt(Constants.PORTFIRERATE, 1);
        SetCurrentPrestige(PlayerPrefs.GetInt(Constants.PRESTIGEEARN, 0));

        if (PlayerPrefs.GetInt(Constants.DEFEATDRAGON,0)==1)
        {
			transform.position = new Vector3 (44, -33, 0);
            
            hullcurrent = PlayerPrefs.GetInt(Constants.HULLVALUE_TEMP, hullmax);
            SetCurrentPrestige(PlayerPrefs.GetInt(Constants.PRESTIGEEARN_TEMP, 0));
            crewsaved = PlayerPrefs.GetInt(Constants.CREWSAVED_TEMP, 0);

            hulllevel = PlayerPrefs.GetInt(Constants.HULL_TEMP, 1);
            toolboxlevel = PlayerPrefs.GetInt(Constants.TOOLBOX_TEMP, 1);
            starboardcannonpowerlevel = PlayerPrefs.GetInt(Constants.STARBOARDPOWER_TEMP, 1);
            starboardcannonfireratelevel = PlayerPrefs.GetInt(Constants.STARBOARDFIRERATE_TEMP, 1);
            wheellevel = PlayerPrefs.GetInt(Constants.WHEEL_TEMP, 1);
            dynamitelevel = PlayerPrefs.GetInt(Constants.DYNAMITE_TEMP, 1);
            portcannonpowerlevel = PlayerPrefs.GetInt(Constants.PORTPOWER_TEMP, 1);
            portcannonfireratelevel = PlayerPrefs.GetInt(Constants.PORTFIRERATE_TEMP, 1);
        }

        //if (SceneManager.GetActiveScene().name.Equals("Training"))
        //{
        //    hulllevel = 5;
        //    toolboxlevel = 5;
        //    starboardcannonpowerlevel = 5;
        //    starboardcannonfireratelevel = 5;
        //    wheellevel = 5;
        //    dynamitelevel = 5;
        //    portcannonpowerlevel = 5;
        //    portcannonfireratelevel = 5;

        //    hullmax = 100 + (25 * (hulllevel -1));
        //    hullcurrent = hullmax;
        //}

    }

	// Update is called once per frame
	void Update () {
		if (isGodMode == true)
		{
			hullcurrent = hullmax;
		}

		// Change sprites according to hulllevel if necessary;
		switch (hulllevel)
		{
		case 1:
			GetComponent<SpriteRenderer>().sprite = Resources.Load("Ship_lvl1", typeof(Sprite)) as Sprite;
			hullmax = 100;
			break;
		case 2:
			GetComponent<SpriteRenderer>().sprite = Resources.Load("Ship_lvl2", typeof(Sprite)) as Sprite;
			hullmax = 125;
			break;
		case 3:
			GetComponent<SpriteRenderer>().sprite = Resources.Load("Ship_lvl3", typeof(Sprite)) as Sprite;
			hullmax = 150;
			break;
		case 4:
			GetComponent<SpriteRenderer>().sprite = Resources.Load("Ship_lvl4", typeof(Sprite)) as Sprite;
			hullmax = 175;
			break;
		case 5:
			GetComponent<SpriteRenderer>().sprite = Resources.Load("Ship_lvl5", typeof(Sprite)) as Sprite;
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
				// Play ElectricShock.wav
				// Show ship_shock animation
				shipshock.SetActive(true);
			}
		}

		if (Time.time - shockstarttime >= 5) // 5 secs since shocked
		{
			isShocked = false;
			// Stop ElectricShock.wav
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

		//Show damages
		int currentdamage = Mathf.FloorToInt(((float)hullmax - (float)hullcurrent) / (float)hullmax * 10.0f);
		if (damagearraysize > currentdamage)
		{
			damagearraysize--;
			if (damagearraysize < 0) {
				damagearraysize = 0;
			}
			else
			{
				Destroy (damagearray [damagearraysize]);
			}
		}
		else
		if (damagearraysize < currentdamage)
		{
			Vector3 v = transform.position;
			v.x += Random.Range (-0.6f, 0.6f);
			v.y += Random.Range (-1.5f, 1.5f);
			if (damagearraysize > 9) {
				damagearraysize = 9;
			}
			else
			{
				damagearray [damagearraysize] = Instantiate (burning, v, transform.rotation) as GameObject;
				damagearray [damagearraysize].transform.parent = gameObject.transform;
				float randomsize = currentdamage * Random.value / 10.0f;
				damagearray [damagearraysize].transform.localScale = new Vector3 (1.0f + (currentdamage / 10.0f), 1.0f + (currentdamage / 10.0f), 1.0f);
				damagearraysize++;
			}
/*				if (Random.value >= 0.5f)
				{
					Vector3 v = transform.position;
					v.x += Random.Range (-0.9f, 0.9f);
					v.y += Random.Range (-1.5f, 1.5f);
					damagearray [damagearraysize] = Instantiate (burning, v, transform.rotation) as GameObject;
					damagearray [damagearraysize].transform.parent = gameObject.transform;
					float randomsize = currentdamage * Random.value / 10.0f;
					damagearray [damagearraysize].transform.localScale = new Vector3 (1.0f + (currentdamage / 10.0f), 1.0f + (currentdamage / 10.0f), 1.0f);
					damagearraysize++;
				}
				else
				{
					Vector3 v = transform.position;
					v.x += Random.Range (-0.6f, 0.6f);
					v.y += Random.Range (-1.5f, 1.5f);
					damagearray [damagearraysize] = Instantiate (hole, v, transform.rotation) as GameObject;
					damagearray [damagearraysize].transform.parent = gameObject.transform;
					float randomsize = currentdamage * Random.value / 10.0f;
					damagearray [damagearraysize].transform.localScale = new Vector3 (1.0f + (currentdamage / 10.0f), 1.0f + (currentdamage / 10.0f), 1.0f);
					var angle = transform.eulerAngles;
					angle.z = Random.Range (0.0f, 360.0f);
					damagearray [damagearraysize].transform.eulerAngles = angle;
					damagearraysize++;
				}*/
			}

		if ((Input.GetKey(KeyCode.RightControl) || Input.GetKey(KeyCode.LeftControl)) && Input.GetKeyDown(KeyCode.G))
		{
			// CTRL + G = God Mode
			isGodMode = !isGodMode;
		}

		if ((Input.GetKey (KeyCode.RightControl) || Input.GetKey (KeyCode.LeftControl)) && Input.GetKeyDown (KeyCode.T)) {
			// CTRL + T = Teleport
			if (SceneManager.GetActiveScene ().name == "Training") {
				Vector3 newposition = new Vector3 (58.46f, 31.45f, 0f);
				transform.position = newposition;
			} else if (SceneManager.GetActiveScene ().name == "Level 1") {
				Vector3 newposition = new Vector3 (44.0f, -33.0f, 0f);
				transform.position = newposition;
			}
		}

        if ((Input.GetKey(KeyCode.RightControl) || Input.GetKey(KeyCode.LeftControl)) && Input.GetKeyDown(KeyCode.Y))
        {
            // CTRL + Y = Teleport
            if (SceneManager.GetActiveScene().name == "Level 1")
            {
                Vector3 newposition = new Vector3(135.0f, -62.0f, 0f);
                transform.position = newposition;
            }
        }

        if ((Input.GetKey(KeyCode.RightControl) || Input.GetKey(KeyCode.LeftControl)) && Input.GetKeyDown(KeyCode.P))
		{
			// CTRL + P = 5000 prestige
			SetCurrentPrestige(5000);
		}

        if ((Input.GetKey(KeyCode.RightControl) || Input.GetKey(KeyCode.LeftControl)) && Input.GetKeyDown(KeyCode.V))
        {
            // CTRL + V = all crew saved
            crewsaved = 7;
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
			// Play ShipCollide.wav
			//shipcollidesound.Play();
            AudioManager.instance.PlaySound(shipcollidesound.clip, this.transform.position);
			hullcurrent -= Mathf.CeilToInt(rb.velocity.magnitude);
		}
	}
}
