using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;
using TeamUtility.IO;

public class InstructionsManager : MonoBehaviour {

	public GameObject current_trigger = null;
	public GameObject ship;
	public Text dialogueText;

	private List<string> accomplished_triggers = new List<string>();

	private Dictionary<string, string> instructions = new Dictionary<string, string>(){
		{"TriggerArea0", "Sharpie and Painty, let's see if you still remember how to navigate the Ship\n" +
			"One of you has to go to the wheel station.\n" +
			"Press ENTER to engage Sharpie / LEFTSHFIT to engage Painty to the wheel station\n" +
			"Forward - up for Sharpie, w for Painty\n" +
			"Left - up + left for Sharpie, w + a for Painty\n" +
			"Right - up + right for Sharpie, w + d for Painty\n" +
			"Press ENTER to continue"},
		{"TriggerArea1", "Now, let's see how well you can navigate the ship\n" +
			"Try to avoid the rock group!\n" +
			"You can refer to the mini map at the left bottom corner for direction.\n" +
			"Press ENTER to continue"},
		{"TriggerArea2", "Beware of the flying fishes! They move fast.\n" +
			"Engage at the canon section.\n" +
			"To fire, press ENGAGE again\n" +
			"Press ENTER to continue"},
		{"TriggerArea3", "Well done! Now you may slowly navigate your ship to the destination island.\n" +
			"We are having a graduation ceremony there!\n" +
			"Press ENTER to continue"}
	};

	private char[] charArray;
	private int charArrayLength;
	private int currChar = 0;
	private int startTime = 0;
	private int timeCount = 0; //time scale is disabled, use a running number as time scale
	private int defaultTextSpeed = 5;
	private string dialogue = "";
	private bool triggered = false;

	// Use this for initialization
	void OnEnable () {
		if (current_trigger != null && accomplished_triggers.Contains(current_trigger.name) == false) {

			Time.timeScale = 0;
			ship.GetComponent<Timer> ().enabled = false;
			accomplished_triggers.Add (current_trigger.name);
			startTime = 0;
			triggered = true;
			dialogue = "";
			currChar = 0;
		}
	}
	
	// Update is called once per frame
	void Update () {

		timeCount++;
		if (current_trigger != null && InputManager.GetButtonDown ("Submit")) {
			Time.timeScale = 1;
			ship.GetComponent<Timer> ().enabled = true;
			this.gameObject.SetActive (false);

			current_trigger = null;
			dialogue = "";
			currChar = 0;
			triggered = false;

		} else if(current_trigger != null && triggered){
			
			if (currChar == 0)
			{
				charArray = instructions[current_trigger.name].ToCharArray();
				charArrayLength = charArray.Length;
			}

			if (currChar < charArrayLength)
			{
				
				if (timeCount - startTime > defaultTextSpeed)
				{
					dialogue += charArray[currChar];
					dialogueText.text = dialogue;
					currChar++;
					startTime = timeCount;
				}

			}

		}
	}
}
