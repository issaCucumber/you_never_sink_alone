using UnityEngine;
using System.Collections;

public class EnemyElectricShock : MonoBehaviour {

	public GameObject bullet;
	public float bulletOffset = 5f;
	const float  delay = 0.5f;
	float dieDelay = 0;
	//public float bulletOffset = 5f;
	Transform ship;
	//bool readyToShock = true;
	bool stopAfterShock = true;
	bool attacked = false;

	ShipActions sa;	
	EnemyDamageHandler edh;

	void Start () {

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

		if (/*readyToShock &&*/ !attacked && withinAttackDistance () && sa != null) {
			//update ship flag
			Debug.Log ("EnemyElectricShock:attack!!!!!!!!!!!!!!!!!!!!!!!!!");

			sa.shock = true;
			attacked = true;
			dieDelay = delay;
			transform.gameObject.GetComponent<Renderer> ().material.color = Color.red;

			Instantiate (bullet, transform.position + getBulletOffset (), transform.rotation);
			//readyToShock = false;

			if (stopAfterShock) {
				Debug.Log ("EnemyElectricShock:stop!!");
				EnemyMoveForward emf = transform.GetComponent<EnemyMoveForward>();
				emf.stop = true;	
			}
		}

		if (attacked && dieDelay <=0) {
			Debug.Log ("health is set to 0");
			edh.health = 0;
		}
	}


	void OnTriggerEnter2D(Collider2D other) {

		//if (isSameName(other.name,bullet.name)) {
		//	readyToShock = false;
		//}
	}


	void OnTriggerExit2D(Collider2D other) {

		//if (isSameName(other.name,bullet.name)) {
		//	readyToShock = true;
		//}
	}

	bool isSameName(string name1, string name2) {

		int bracketIndex1 = name1.IndexOf ("(");
		int bracketIndex2 = name2.IndexOf ("(");

		string n1 = name1.Substring (0, bracketIndex1 > 0 ? bracketIndex1 : name1.Length);
		string n2 = name2.Substring (0, bracketIndex2 > 0 ? bracketIndex2 : name2.Length);

		return n1.Equals (n2);
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

		if (dir.magnitude - shipHeight - enemyHeight < shipHeight * 0.5) {

			Debug.Log ("EnemyElectricShock:withinAttackDistance true");
			return true;

		}

		return false;

	}
}
