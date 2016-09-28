﻿using UnityEngine;
using System.Collections;

public class EnemyDamageHandler : MonoBehaviour {
	
	public GameObject explode;

	public bool clashShipDie = true;

	public int health = 1;

	float invulnTimer = 0;

	bool exploded = false;

	int correctLayer;

	void Start () {
		correctLayer = gameObject.layer;
	}

	void OnTriggerEnter2D(Collider2D other) {
		Debug.Log ("other name is "+other.name);
		if (other.name == "Ship") {
			if (clashShipDie) {
				health = 0;
			}
		} else if (other.name.StartsWith("Rock")) {
			health = 0;
			Debug.Log("hit Rock health is "+health);
		} else if (other.name.StartsWith("Cannonball")) {
			Debug.Log("hit Cannonball");
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
			invulnTimer = 1f;
			gameObject.layer = 10;

		}else if (other.name.StartsWith("SuperAttack")) {
			health = 0;
		}

	}

	// Update is called once per frame
	void Update () {
		
		invulnTimer -= Time.deltaTime;

		if(invulnTimer <= 0) {
			gameObject.layer = correctLayer;
		}

		if (health <= 0 ) {
			if (!exploded && explode != null) {
				Instantiate (explode, transform.position, transform.rotation);
				exploded = true;
			}

			Debug.Log ("health="+health);
			Die();
		}
	}

	void Die() {
		Debug.Log ("I Die");
		Destroy (gameObject);
	}

}
