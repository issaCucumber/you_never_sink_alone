using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {

	public GameObject enemy;

	public float spawnRate = 5;
	float next = 1;

	public float triggerDistance = 10;

	Transform ship;
	// Update is called once per frame
	void Update () {
		next -= Time.deltaTime;

		if (next <= 0) {
			next = spawnRate;

			if (inTriggerArea(transform.position)) {
				Instantiate (enemy, transform.position, Quaternion.identity);
			}
		}
	}
		
	bool inTriggerArea(Vector3 position) {

		if (ship == null) {

			GameObject target = GameObject.Find ("Ship");

			if (target != null) {

				ship = target.transform;
			}
		}

		float distance = Vector3.Distance (position, ship.position);

		//enemy will not spawn if the ship is quite near to the spawn point
		if (ship != null && distance < triggerDistance && distance > triggerDistance * 0.5) {
			Debug.Log ("Trigger Spawn");
			return true;
		}

		return false;
	}
}
