using UnityEngine;
using System.Collections;

public class EnemyMoveForward : MonoBehaviour {

	public float maxSpeed = 2f;

	public bool stop = false;

	// Use this for initialization
	void Start () {
		stop = false;
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.name == "Ship") {
			stop = true;
		}
	}

	// Update is called once per frame
	void Update () {
		if (!stop) {
			Vector3 pos = transform.position;

			Vector3 velocity = new Vector3 (0, maxSpeed * Time.deltaTime, 0);
			 
			pos += transform.rotation * velocity;

			transform.position = pos;
		}
	}
}
