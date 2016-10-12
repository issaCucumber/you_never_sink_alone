﻿using UnityEngine;
using System.Collections;

public class TriggerAreaThree : MonoBehaviour {

	public Transform canvas;
	private bool triggered = false;

	void OnTriggerEnter2D(Collider2D other){

		if (other.name == "Ship" && !triggered) {
			//trigger three tutorial
			canvas.GetComponent<InstructionsManager>().current_trigger = this.gameObject;
			canvas.gameObject.SetActive (true);
			triggered = true;

		}

	}

}