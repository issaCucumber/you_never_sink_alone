using UnityEngine;
using System.Collections;

public class EnemySuicideAttack : MonoBehaviour {

	const float  delay = 5f;

	public float speedMultiple = 3;

	float attackDelay = 0;
	float attackSpeed;
	float originalSpeed;


	Transform ship;
	EnemyMoveForward emf;

	// Use this for initialization
	void Start () {
		emf = transform.GetComponent<EnemyMoveForward>();
		originalSpeed = emf.maxSpeed;
		attackSpeed = emf.maxSpeed * speedMultiple;
	}

	// Update is called once per frame
	void Update () {
		if (attackDelay > 0) {
			attackDelay -= Time.deltaTime;
		}

		if (emf.maxSpeed.Equals (attackSpeed) && withinAttackDistance ()) {
			//do nothing

		} else if (emf.maxSpeed.Equals (originalSpeed) && withinAttackDistance () && attackDelay <= 0) {

			emf.maxSpeed = attackSpeed;
			transform.gameObject.GetComponent<Renderer> ().material.color = Color.red;
			attackDelay = delay;
			Debug.Log ("Start Attack");
		} else if (emf.maxSpeed.Equals (originalSpeed)) {
			//do nothing
		} else if (emf.maxSpeed.Equals (attackSpeed)) {
			emf.maxSpeed = originalSpeed;
			transform.gameObject.GetComponent<Renderer> ().material.color = Color.white;
			Debug.Log ("Restore");
		}

	}


	bool withinAttackDistance() {
		
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

		float distance = (ship.position - transform.position).magnitude - shipHeight - enemyHeight;


		if (distance < shipHeight * 0.5) {

			return true;
		
		}

		return false;

	}
}
