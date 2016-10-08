﻿using UnityEngine;
using System.Collections;

public class EnemyHypnotize : MonoBehaviour {

	public GameObject bullet;
	public float bulletOffset = 5f;
	const float  delay = 0.5f;
	float dieDelay = 0;
	Transform ship;
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

		if (!attacked && withinAttackDistance () && sa != null) {

			sa.hypnotize = true;
			attacked = true;
			dieDelay = delay;
			transform.gameObject.GetComponent<Renderer> ().material.color = Color.blue;
			if (bullet != null) {
				GameObject go = (GameObject)Instantiate (bullet, transform.position + getBulletOffset (), transform.rotation);
				go.transform.parent = ship;
			}

			if (stopAfterShock) {
				EnermyRandomMove erm = transform.GetComponent<EnermyRandomMove>();
				//erm.stop = true;	//TODO
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

		if (dir.magnitude - shipHeight - enemyHeight < shipHeight * 0.5) {

			return true;

		}

		return false;

	}
}
