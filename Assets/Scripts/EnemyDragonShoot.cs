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

	float cooldownTimer = 5f;
	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

		cooldownTimer -= Time.deltaTime;

		if (cooldownTimer <= 0 && inAttackDistance(transform.position)) {

			cooldownTimer = shootDelay;

			if (isFireAttack) {
				Instantiate (fire, transform.position + getBulletOffset (), transform.rotation);
			} else {
				Instantiate (wave, transform.position + getBulletOffset (), transform.rotation);
			}
			cooldownTimer = shootDelay;
			isFireAttack = !isFireAttack;
		}
	}

	bool inAttackDistance(Vector3 position) {

		Debug.Log("inAttackDistance");

		if (ship == null) {

			GameObject target = GameObject.Find ("Ship");

			if (target != null) {
				
				ship = target.transform;
			}
		}

		if (ship != null && Vector3.Distance(position, ship.position) < attackDistance) {
			Debug.Log("inAttackDistance return true");
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
