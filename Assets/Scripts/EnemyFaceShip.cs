using UnityEngine;
using System.Collections;

public class EnemyFaceShip : MonoBehaviour {

	float rotSpeed = 180f;
	Transform ship;
	bool movingOpp = false;//true if enemy is changing direction on hitting island. false if enemy face ship as normal
	float movingOppTimer = 3f;
	bool hitIsland = false;
	bool changeDir = false;
	// Use this for initialization
	void Start () {
	}


	void OnTriggerEnter2D(Collider2D other) {
//		Debug.Log ("OnTriggerEnter2D");
		if (other.name.StartsWith ("Academy")
		    || other.name.Equals ("Hamburger Island", System.StringComparison.OrdinalIgnoreCase)
			|| other.name.Equals ("Island of the Mist", System.StringComparison.OrdinalIgnoreCase)
			|| other.name.Equals ("Pepperoni Pizza Island", System.StringComparison.OrdinalIgnoreCase)
			|| other.name.Equals ("Hawaiian Pizza Island", System.StringComparison.OrdinalIgnoreCase)
			|| other.name.Equals ("rock island", System.StringComparison.OrdinalIgnoreCase)) {//TODO to add other island names
			//Debug.Log ("hit island!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
			hitIsland = true;
		}


	}
	// Update is called once per frame
	void Update () {

		//moving on opp dir for 3 seconds on hitting island
		if (hitIsland) {
			//Debug.Log ("hitIsland");
			changeDir = true;
			movingOppTimer = 3f;
			movingOpp = true;
		}else if (!movingOpp && !hitIsland) {
			//Debug.Log ("!movingOpp && !hitIsland");
			movingOpp = false;
		} else if (movingOpp && movingOppTimer > 0 && !hitIsland) {
			//Debug.Log ("movingOpp && movingOppTimer > 0 && !hitIsland");
			changeDir = true;
			movingOppTimer -= Time.deltaTime;
			movingOpp = true;
		} else if (movingOpp && movingOppTimer <= 0 && !hitIsland) {
			//Debug.Log ("movingOpp && movingOppTimer <= 0 && !hitIsland");
			changeDir = false;
			movingOpp = false;
		}

		if (ship == null) {

			GameObject target = GameObject.Find ("Ship");

			if (target != null) {
				
				ship = target.transform;
			}
		}

		if (ship == null) {
			return;
		}

		Vector3 dir = ship.position - transform.position;
		if (changeDir) {
			dir = -dir;
		}

		float zAngle = Mathf.Atan2 (dir.y, dir.x) * Mathf.Rad2Deg - 90;

		Quaternion desiredRot = Quaternion.Euler (0, 0, zAngle);

		transform.rotation = Quaternion.RotateTowards (transform.rotation, desiredRot , rotSpeed * Random.Range(0.9f,1.1f) * Time.deltaTime);

		hitIsland = false;
	}
}
