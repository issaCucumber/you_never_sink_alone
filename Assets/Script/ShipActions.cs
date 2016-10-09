using UnityEngine;
using System.Collections;

public class ShipActions : MonoBehaviour {

	[SerializeField]
	private Stat health;

	[SerializeField]
	private Prestige prestige;

	private float startTime;


 	public int hullmax = 100;
    public int hullcurrent = 100;
    //public int prestige = 0;
    public int hulllevel = 1;

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

    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        //        if (coll.gameObject.tag == "Enemy")
        //            coll.gameObject.SendMessage("ApplyDamage", 10);
        hullcurrent -= Mathf.CeilToInt(rb.velocity.magnitude);
    }
}
