using UnityEngine;
using System.Collections;

public class EnemyDamageHandler : MonoBehaviour {
	
	public GameObject explode;

	public bool clashShipDie = true;

	public int health = 1;

	float invulnTimer = 0;

	float deathDelay = 0;

	bool exploded = false;

	int correctLayer;

	// Use this for initialization
	void Start () {
		correctLayer = gameObject.layer;
	}

	void OnTriggerEnter2D(Collider2D other) {

		if (other.name == "Ship") {
			if (clashShipDie) {
				health = 0;
			}
		} else if (other.name.StartsWith ("Cannon")) {
			
			if (other.name == "Cannon1") {
				health -= 2;
			} else if (other.name == "Cannon2") {
				health -= 4;
			} else if (other.name == "Cannon3") {
				health -= 6;
			} else if (other.name == "Cannon4") {
				health -= 8;
			} else if (other.name == "Cannon5") {
				health -= 10;
			} 

		}else if (other.name.StartsWith("Dynamite")) {
			health = 0;
		}

		invulnTimer = 2f;
		deathDelay = 1f;
		gameObject.layer = 10;
	}

	// Update is called once per frame
	void Update () {
		
		invulnTimer -= Time.deltaTime;
		deathDelay -= Time.deltaTime;

		if(invulnTimer <= 0) {
			gameObject.layer = correctLayer;
		}

		if (health <= 0 ) {
			
			if (!exploded && explode != null) {
				Instantiate (explode, transform.position, transform.rotation);
				exploded = true;
			}

			if(deathDelay <= 0) {
				Debug.Log ("health="+health);
				Debug.Log ("deathDelay="+deathDelay);
				Die();
			}
		}
	}

	void Die() {
		Destroy (gameObject);
	}
}
