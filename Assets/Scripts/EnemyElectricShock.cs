using UnityEngine;
using System.Collections;

public class EnemyElectricShock : MonoBehaviour {

	public GameObject bullet;
	public float bulletOffset = 5f;
	const float  delay = 0.5f;
	float dieDelay = 0;
	Transform ship;
	bool stopAfterShock = true;
	bool attacked = false;

	ShipActions sa;	
	EnemyDamageHandler edh;

	AudioSource audio;

	void Start () {
		
		audio = GetComponent<AudioSource> ();
		edh = transform.GetComponent<EnemyDamageHandler> ();
		
	}

	// Update is called once per frame
	void Update () {
		
		if (ship != null && sa == null) {
			sa = ship.GetComponent<ShipActions>();
		}

		if (dieDelay > 0) {
			dieDelay -= Time.deltaTime;
		}

		if (!attacked && withinAttackDistance () && sa != null) {

			//EnemyElectricSHock.wav
			audio.Play();

			sa.shocknow = true;
			sa.hullcurrent -= getEnemyDamageValue();
			attacked = true;
			dieDelay = delay;
			transform.gameObject.GetComponent<Renderer> ().material.color = Color.cyan;
			if (bullet != null) {
				Instantiate (bullet, transform.position + getBulletOffset (), transform.rotation);
			}

			if (stopAfterShock) {
				EnemyMoveForward emf = transform.GetComponent<EnemyMoveForward>();
				emf.stop = true;	
			}
		}

		if (attacked && dieDelay <=0) {
			edh.health = 0;
		}
	}

	Vector3 getBulletOffset () {

		if (ship == null) {

			GameObject target = GameObject.Find ("Ship");

			if (target != null) {

				ship = target.transform;
			}
		}

		if (ship == null) {
			return new Vector3 (0,0,0);
		}

		Vector3 dir = ship.position - transform.position;
		Vector3 offset = dir.normalized * bulletOffset;

		return offset;

	}

	bool withinAttackDistance () {
		GameObject target = GameObject.Find ("Ship");
		if (ship == null) {

			if (target != null) {

				ship = target.transform;
			}
		}

		if (ship == null) {
			return false;
		}

		float shipHeight = ship.gameObject.GetComponent<Renderer> ().bounds.size.y / 2;
		float enemyHeight = transform.gameObject.GetComponent<Renderer> ().bounds.size.y / 2;

		Vector3 dir = ship.position - transform.position;

		if (dir.magnitude  < shipHeight * 0.5 + enemyHeight + 0.5) {

			return true;

		}

		return false;

	}



	int getEnemyDamageValue() {
		if (gameObject.name.StartsWith ("ElectricEel")) {
			return 5;
		}
		return  0;
	}
}
