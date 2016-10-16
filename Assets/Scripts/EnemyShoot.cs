using UnityEngine;
using System.Collections;

//shoot bullet when touch ship
public class EnemyShoot : MonoBehaviour {

	public GameObject bullet;
	public float bulletOffset = 5f;
	Transform ship;
	bool readyToShoot = true;
	public bool dieAfterShoot = false;
	public bool stopAfterShoot = false;
	ShipActions sa;	

	void Start () {
		sa = GameObject.Find ("Ship").transform.GetComponent<ShipActions>();
	}

	// Update is called once per frame
	void Update () {
		

		if (readyToShoot && withinAttackDistance ()) {

			sa.hullcurrent -= getEnemyDamageValue();
			Instantiate (bullet, transform.position + getBulletOffset (), transform.rotation);
			//TODO Sound: OctopusInk.wav
			readyToShoot = false;

			if (stopAfterShoot) {
				EnemyMoveForward emf = transform.GetComponent<EnemyMoveForward>();
				emf.stop = true;	
			}

			if (dieAfterShoot) {
				EnemyDamageHandler edh = transform.GetComponent<EnemyDamageHandler>(); 
				edh.health = 0;
			}
		}
	}


	void OnTriggerEnter2D(Collider2D other) {
		
		if (isSameName(other.name,bullet.name)) {
			readyToShoot = false;
		}
	}


	void OnTriggerExit2D(Collider2D other) {
		
		if (isSameName(other.name,bullet.name)) {
			readyToShoot = true;
		}
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

			return true;

		}

		return false;

	}


	int getEnemyDamageValue() {
		if (gameObject.name.StartsWith ("Octopus")) {
				return 5;
		}
		return  0;
	}


}
