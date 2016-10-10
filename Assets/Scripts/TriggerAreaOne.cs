using UnityEngine;
using System.Collections;

public class TriggerAreaOne : MonoBehaviour {

	private bool triggeredOne = false;

	void OnTriggerEnter2D(Collider2D other){

		if (!triggeredOne && other.name == "Ship") {
			//trigger first tutorial

			triggeredOne = true;
		}

	}
}
