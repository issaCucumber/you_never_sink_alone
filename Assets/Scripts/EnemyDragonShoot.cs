using UnityEngine;
using System.Collections;

//Dragon shoot fire or wave
public class EnemyDragonShoot : MonoBehaviour {

	public GameObject fire;

	public GameObject wave;

	public float attackDistance = 10f;

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
		cooldownTimer = 0f;
	}

	// Update is called once per frame
	void Update () {
		Debug.Log ("dragonshoot update cooldownTimer="+cooldownTimer);
		if (inAttackDistance(transform.position)) {
			cooldownTimer -= Time.deltaTime;
		}
		Debug.Log ("cooldownTimer="+cooldownTimer);
		Debug.Log ("shootDelay - attackEffectDelay="+(shootDelay-attackEffectDelay));
		if (cooldownTimer < shootDelay - attackEffectDelay || !inAttackDistance(transform.position)) {
			transform.gameObject.GetComponent<Renderer> ().material.color = Color.white;
		}
		if (cooldownTimer <= 0 && (firstshoot || inAttackDistance(transform.position))) {
			firstshoot = false;
			cooldownTimer = shootDelay;

			if (isFireAttack) {
				Debug.Log ("fireattack");
				//DragonFire.wav
				audio.PlayOneShot(audioClip[0]);
				Instantiate (fire, transform.position + getBulletOffset (), transform.rotation);
				transform.gameObject.GetComponent<Renderer> ().material.color = Color.red;
			} else {
				//DragonWave.wav
				Debug.Log ("waveattack");
				audio.PlayOneShot(audioClip[1]);
				Instantiate (wave, transform.position + getBulletOffset (), transform.rotation);
				transform.gameObject.GetComponent<Renderer> ().material.color = Color.magenta;
			}
			cooldownTimer = shootDelay;
			isFireAttack = !isFireAttack;
		}

	}

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
		Vector3 offset = dir.normalized * new Vector3 (2.5f,2.5f,0).magnitude;

		return offset;

	}
}
