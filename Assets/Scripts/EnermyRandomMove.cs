using UnityEngine;
using System.Collections;

public class EnermyRandomMove : MonoBehaviour {
	float rotSpeed = 180f;
	public float maxSpeed = 2f;
	float randomY;
	float randomX;
	float randomRot;
	Transform ship;
	float zAngle;
	float timer = 0;
	// Update is called once per frame
	void Update () {
			if (ship == null) {

				GameObject target = GameObject.Find ("Ship");

				if (target != null) {
					//Debug.Log ("find ship");
					ship = target.transform;
				}
			}

			if (ship == null) {
				return;
			}


			randomY = Random.Range (0f, 70f);
			randomX = Random.Range (0f, 70f);
			randomRot = Random.Range (-2f, 2f);

			//random is to change the face direction for now

			//face direction
			Vector3 dir = ship.position - transform.position;
			zAngle = Mathf.Atan2 (dir.y + randomX, dir.x + randomY) * Mathf.Rad2Deg - 90;
			Quaternion desiredRot = Quaternion.Euler (0, 0, zAngle);
			transform.rotation = Quaternion.RotateTowards (transform.rotation, desiredRot, rotSpeed * Time.deltaTime);


			//movement
			Vector3 pos = transform.position;
			Vector3 velocity = new Vector3 (0, Random.Range (0, 1f) * maxSpeed * Time.deltaTime, 0);
			pos += transform.rotation * velocity;
			transform.position = pos;
	}
}
