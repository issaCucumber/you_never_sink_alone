using UnityEngine;
using System.Collections;

public class EnemyDragonGateHandler : MonoBehaviour {
	
	public GameObject explode;

	int health = 20;

	bool exploded = false;

	ShipActions sa;	

	void Start () {
		sa = GameObject.Find ("Ship").transform.GetComponent<ShipActions>();
	}

	void OnTriggerEnter2D(Collider2D other) {
		Debug.Log (other.name + " enters "+gameObject.name+" trigger");
		if (other.name.StartsWith("Cannonball")) {
			Debug.Log(gameObject.name + " hit Cannonball bf hp="+health);
			Shoot bullet = other.GetComponent<Shoot>();
			if (bullet.level == 1) {
				health -= 2;
			} else if (bullet.level == 2) {
				health -= 4;
			} else if (bullet.level == 3) {
				health -= 6;
			} else if (bullet.level == 4) {
				health -= 8;
			} else if (bullet.level == 5) {
				health -= 10;
			} 
			Debug.Log("hit Cannonball af hp="+health);
			//invulnTimer = 1f;
			//gameObject.layer = 10;

		}else if (other.name.StartsWith("SuperAttack")) {
			Debug.Log("hit SuperAttack");
			health -= 20;
		}

		if (health <= 0 ) {
				GameObject go = (GameObject)Instantiate (explode, transform.position, transform.rotation);
				Debug.Log("Instantiate explode="+go);
				exploded = true;
			Destroy (gameObject);

		}
	}

}
