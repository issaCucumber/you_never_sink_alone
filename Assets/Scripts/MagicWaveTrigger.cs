using UnityEngine;
using System.Collections;

public class MagicWaveTrigger : MonoBehaviour {

	private bool triggered = false;
	private int timecount = 0;

	void OnTriggerEnter2D(Collider2D other){
		triggered = true;
		timecount = 0;
	}

	void OnTriggerStay2D(Collider2D other) {
		attack (other);
	}

	void OnTriggerExit2D(Collider2D other)
	{
		triggered = false;
		transform.GetComponent<SpriteRenderer> ().enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (triggered) {
			timecount++;
		}
	}

	private void attack(Collider2D other){

		if (timecount % 1000 < 500) {
			transform.GetComponent<SpriteRenderer> ().enabled = true;

			//distance
			float distance = Vector3.Distance (other.transform.position, transform.position);

			float force = 0f;
			if (distance < 50f) {
				force = -150F;
			} else if (distance < 70f) {
				force = -100F;
			} else if (distance < 95f) {
				force = -50F;
			}

			other.attachedRigidbody.AddForce (force * other.attachedRigidbody.velocity);
		} else {
			transform.GetComponent<SpriteRenderer> ().enabled = false;
		}

	}
}
