﻿using UnityEngine;
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
			
			if (isSameName(other.name,"Cannon1")) {
				health -= 2;
			} else if (isSameName(other.name,"Cannon2")) {
				health -= 4;
			} else if (isSameName(other.name,"Cannon3")) {
				health -= 6;
			} else if (isSameName(other.name,"Cannon4")) {
				health -= 8;
			} else if (isSameName(other.name,"Cannon5")) {
				health -= 10;
			} 

		}else if (isSameName(other.name,"Dynamite")) {
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


	bool isSameName(string name1, string name2) {

		int bracketIndex1 = name1.IndexOf ("(");
		int bracketIndex2 = name2.IndexOf ("(");

		string n1 = name1.Substring (0, bracketIndex1 > 0 ? bracketIndex1 : name1.Length);
		string n2 = name2.Substring (0, bracketIndex2 > 0 ? bracketIndex2 : name2.Length);

		return n1.Equals (n2);
	}
}
