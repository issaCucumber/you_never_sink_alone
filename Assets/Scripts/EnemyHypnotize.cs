using UnityEngine;
using System.Collections;

public class EnemyHypnotize : MonoBehaviour {

	public GameObject bullet;
	public float bulletOffset = 0f;
	const float  delay = 0.5f;
	float dieDelay = 0;
	Transform ship;
	bool stopAfterShock = true;
	bool attacked = false;
	bool hit = false;

	ShipActions sa;	
	EnemyDamageHandler edh;

	void Start () {

		edh = transform.GetComponent<EnemyDamageHandler> ();

	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.name == "Ship") {
			hit = true;
			if (ship == null) {
				ship = GameObject.Find ("Ship").transform;
			}
			if (sa == null) {
				sa = ship.GetComponent<ShipActions>();
			}
		}
	}

	// Update is called once per frame
	void Update () {

		if (dieDelay > 0) {
			dieDelay -= Time.deltaTime;
		}

		if (!attacked && hit && sa != null) {

			sa.hypnotize = true;
			attacked = true;
			dieDelay = delay;
			transform.gameObject.GetComponent<Renderer> ().material.color = Color.blue;
			if (bullet != null) {
				GameObject go = (GameObject)Instantiate (bullet, ship.position, transform.rotation);
				go.transform.parent = ship;
			}

			if (stopAfterShock) {
				EnermyRandomMove erm = transform.GetComponent<EnermyRandomMove>();
				//erm.stop = true;	//TODO
			}
		}

		if (attacked && dieDelay <=0) {
			edh.health = 0;
		}
	}

}
