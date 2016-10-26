﻿using UnityEngine;
using System.Collections;

public class EnemyBulletHitShip : MonoBehaviour {

	// Use this for initialization
	bool hitship = false;
	Transform ship;
	public GameObject hitEffect;
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (hitship) {
			GameObject go = (GameObject)Instantiate (hitEffect, ship.position, ship.rotation);
			ship.gameObject.GetComponent<ShipActions> ().hullcurrent -= getEnemyDamageValue();
			go.transform.parent = ship;
			hitship = false;
			Destroy (gameObject);
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.name == "Ship") {
			hitship = true;
			ship = other.transform;
		}
	}



	int getEnemyDamageValue() {
		if (gameObject.name.StartsWith ("DragonFire")) {
			return 15;
		} else if (gameObject.name.StartsWith ("DragonWave")) {
			return 25;
		}
		return  0;
	}
}
