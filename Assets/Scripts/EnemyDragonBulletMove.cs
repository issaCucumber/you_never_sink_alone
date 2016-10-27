using UnityEngine;
using System.Collections;

public class EnemyDragonBulletMove : MonoBehaviour {

	public float speed = 5f;

	float timer;
	const float stoptime = 0.2f;
	const float destroytime = 0.4f;


	bool stop = false;

	// Use this for initialization
	void Start () {
		
	}

	void OnTriggerEnter2D(Collider2D other) {
		
	}

	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		if (timer > stoptime) {
			stop = true;
		}
		if (!stop) {
			Vector3 pos = transform.position;

			Vector3 velocity = new Vector3 (0, speed * Time.deltaTime, 0);

			pos += transform.rotation * velocity;

			transform.position = pos;
		}
		if (timer > destroytime) {
			Destroy (gameObject);
		}
	}
}
