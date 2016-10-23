using UnityEngine;
using System.Collections;

public class WhirlpoolTrigger : MonoBehaviour {

	private bool triggered = false;

	void OnTriggerEnter2D(Collider2D other){
		triggered = true;
	}

	void OnTriggerStay2D(Collider2D other) {
		absorb (other);
	}

	void OnTriggerExit2D(Collider2D other)
	{
		triggered = false;
	}

	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.name == "Ship") {
			coll.gameObject.GetComponent<ShipActions> ().touchWhirlpool = true;
			coll.gameObject.GetComponent<ShipActions> ().hullcurrent = 0;
		}
	}

	private void absorb(Collider2D other){
		Vector3 toDeath = Vector3.Normalize (transform.position - other.transform.position);
		other.attachedRigidbody.AddForce (50f * toDeath);
	}
}
