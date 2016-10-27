using UnityEngine;
using System.Collections;

public class MagicWaveTrigger : MonoBehaviour {

	private bool triggered = false;
	private float startTime = 0.0f;

	private float force;

	void Start(){
		startTime = Time.time;
	}
		
	void Update(){
		if (Time.time - startTime < 5.0f) {
			transform.GetComponent<SpriteRenderer> ().enabled = true;
            triggered = true;
		}else {
			transform.GetComponent<SpriteRenderer> ().enabled = false;
            triggered = false;
			if (Time.time - startTime > 20.0f) {
				startTime = Time.time;
			}
		}
	}

	void OnTriggerStay2D(Collider2D other) {
		attack (other);
	}

	private void attack(Collider2D other){

		if (triggered && !other.transform.name.StartsWith ("CannonballPrefab")) {

			//distance
            float distance = Vector3.Distance (other.transform.position, transform.position);

            float force = 0f;
            if (distance < 50f) {
                force = -100F;
            } else if (distance < 70f) {
                force = -50F;
            } else if (distance < 95f) {
                force = -10F;
            }

            //other.attachedRigidbody.AddForce (force * other.attachedRigidbody.velocity);
            other.attachedRigidbody.AddForce(force * Vector3.Normalize(transform.position - other.transform.position));
        }


//		Debug.Log ("================== ATTACK =================");
//		if ( Time.time - startTime < 5.0f ) {
//			Debug.Log ("================== ATTACK !!!!! =================" + (Time.time - startTime));
//			transform.GetComponent<SpriteRenderer> ().enabled = true;
//

//
//			float force = 0f;
//			if (distance < 50f) {
//				force = -15F;
//			} else if (distance < 70f) {
//				force = -10F;
//			} else if (distance < 95f) {
//				force = -5F;
//			}
//
//			other.attachedRigidbody.AddForce (force * Vector3.Normalize(transform.position - other.transform.position));
//		} else {
//			Debug.Log ("================== ATTACK else =================");
//			transform.GetComponent<SpriteRenderer> ().enabled = false;
//			if (Time.time - startTime < 15.0f) {
//				Debug.Log ("================== ATTACK stop!! =================");
//				startTime = Time.time;
//			}
//		}

	}


}
