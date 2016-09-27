using UnityEngine;
using System.Collections;

public class EnemyFaceShip : MonoBehaviour {

	float rotSpeed = 180f;
	Transform ship;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

		if (ship == null) {

			GameObject target = GameObject.Find ("Ship");

			if (target != null) {
				
				ship = target.transform;
			}
		}

		if (ship == null) {
			return;
		}

		Vector3 dir = ship.position - transform.position;

		float zAngle = Mathf.Atan2 (dir.y, dir.x) * Mathf.Rad2Deg - 90;

		Quaternion desiredRot = Quaternion.Euler (0, 0, zAngle);

		transform.rotation = Quaternion.RotateTowards (transform.rotation, desiredRot , rotSpeed * Random.Range(0.9f,1.1f) * Time.deltaTime);

	}
}
