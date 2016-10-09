using UnityEngine;
using System.Collections;

//Dragon shoot fire or wave
public class EnemyDragonShoot : MonoBehaviour {

	public GameObject fire;

	public GameObject wave;

	public float attackDistance = 3f;

	Transform ship;

	bool isFireAttack = true;

	public float shootDelay = 15f;
	float attackEffectDelay = 0.3f;

	float cooldownTimer;

	// Use this for initialization
	void Start () {
		cooldownTimer = shootDelay;
	}

	// Update is called once per frame
	void Update () {

		cooldownTimer -= Time.deltaTime;
		if (cooldownTimer < shootDelay - attackEffectDelay) {
			transform.gameObject.GetComponent<Renderer> ().material.color = Color.white;
		}
		if (cooldownTimer <= 0 && inAttackDistance(transform.position)) {

			cooldownTimer = shootDelay;

			if (isFireAttack) {
				Instantiate (fire, transform.position + getBulletOffset (), transform.rotation);
				transform.gameObject.GetComponent<Renderer> ().material.color = Color.red;
			} else {
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
		Vector3 offset = dir.normalized * new Vector3 (5f,5f,0).magnitude;

		return offset;

	}
}
