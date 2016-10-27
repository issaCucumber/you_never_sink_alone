﻿using UnityEngine;
using System.Collections;

//Dragon shoot fire or wave
public class EnemyDragonShoot : MonoBehaviour {

	public GameObject fire;

	public GameObject wave;

	Transform ship;

	bool isFireAttack = true;
	bool firstshoot = true;

	public float shootDelay = 5f;
	float attackEffectDelay = 0.3f;

	float cooldownTimer;
	public AudioClip[] audioClip;
	AudioSource audio;

	// Use this for initialization
	void Start () {
		audio = GetComponent<AudioSource> ();
		cooldownTimer = 1f;
	}

	// Update is called once per frame
	void Update () {
		//if (inAttackDistance(transform.position)) {
			cooldownTimer -= Time.deltaTime;
		//}
		//if (cooldownTimer < shootDelay - attackEffectDelay || !inAttackDistance(transform.position)) {
		if (cooldownTimer < shootDelay - attackEffectDelay) {
			transform.gameObject.GetComponent<Renderer> ().material.color = Color.white;
		}
		//if (cooldownTimer <= 0 && (firstshoot || inAttackDistance(transform.position))) {
		if (cooldownTimer <= 0) {
			firstshoot = false;
			cooldownTimer = shootDelay;

			if (isFireAttack) {
				Debug.Log ("fireattack");
				//DragonFire.wav
				//audio.PlayOneShot(audioClip[0]);
                AudioManager.instance.PlaySound(audioClip[0], this.transform.position);
				Instantiate (fire, transform.position + getBulletOffset (), transform.rotation);
				transform.gameObject.GetComponent<Renderer> ().material.color = Color.red;
			} else {
				//DragonWave.wav
				Debug.Log ("waveattack");
				//audio.PlayOneShot(audioClip[1]);
                AudioManager.instance.PlaySound(audioClip[1], this.transform.position);
                Instantiate (wave, transform.position + getBulletOffset (), transform.rotation);
				transform.gameObject.GetComponent<Renderer> ().material.color = Color.magenta;
			}
			cooldownTimer = shootDelay;
			isFireAttack = !isFireAttack;
		}

	}
	/*
	bool inAttackDistance(Vector3 position) {

		if (ship == null) {

			GameObject target = GameObject.Find ("Ship");


			if (target != null) {
				
				ship = target.transform;
			}
		}
		if (ship != null && Vector3.Distance(position, ship.position) < attackDistance) {
			return true;
		}

		return false;
	}
	*/

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
		Vector3 offset = dir.normalized * new Vector3 (4f,4f,0).magnitude;

		return offset;

	}
}
