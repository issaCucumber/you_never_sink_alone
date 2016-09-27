﻿using UnityEngine;
using System.Collections;

public class EnermySelfDestruct : MonoBehaviour {//some problems to be discussed.
	bool inVisible;
	public float selfDestructTime = 10f;

	void OnBecameVisible(){
		inVisible = false;
		Debug.Log ("visible");
	}

	void OnBecameInvisible(){
		inVisible = true;
		Debug.Log("invisible");
	}

	void FixedUpdate () {
		if (inVisible) {
			selfDestructTime -= Time.deltaTime;
			Debug.Log("Time: "+ selfDestructTime);
		}
		//Debug.Log ("Time left:" + selfDestructTime);
		if (selfDestructTime<=0){
			Die ();
			Debug.Log ("finished");
		}
	}

	void Die(){
		Destroy (gameObject);
	}
}
