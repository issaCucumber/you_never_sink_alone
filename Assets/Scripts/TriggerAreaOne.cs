﻿using UnityEngine;
using System.Collections;

public class TriggerAreaOne : MonoBehaviour {

	public GameObject canvas;
	private bool triggered = false;

	void OnTriggerEnter2D(Collider2D other){

		if (other.name == "Ship" && !triggered) {
			//trigger two tutorial
			canvas.GetComponent<InstructionsManager>().current_trigger = this.gameObject;
			canvas.gameObject.SetActive (true);
			triggered = true;

		}

	}
}
