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
		} else if (isSameName(other.name, "CannonballPrefab")) {
			Debug.Log("hit CannonballPrefab");
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

		}else if (isSameName(other.name,"SuperAttack")) {
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


	bool isSameName(string name1, string name2) {

		int bracketIndex1 = name1.IndexOf ("(");
		int bracketIndex2 = name2.IndexOf ("(");

		string n1 = name1.Substring (0, bracketIndex1 > 0 ? bracketIndex1 : name1.Length);
		string n2 = name2.Substring (0, bracketIndex2 > 0 ? bracketIndex2 : name2.Length);

		return n1.Equals (n2);
	}
}
