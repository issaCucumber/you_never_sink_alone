using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using TeamUtility.IO;

public class TriggerAreaTwo : MonoBehaviour {

	public TutorialDialogueEntity[] tutorials;
	public Text nameText;
	public Text dialogueText;
	private float textSpeed = 0.2f;
	public GameObject ship;
	public GameObject avatar;
	public GameObject avatarPanel;
	public GameObject canvas;
	public GameObject instruction;

	private char[] charArray;
	private int charArrayLength;
	private int currChar = 0;
	private float startTime;
	private string dialogue;
	private bool triggered = false;
	private bool tutorialDone = false;
	private int currentFrame = 0;

	private int timeCount = 0; //time scale is disabled, use a running number as time scale

	void OnTriggerEnter2D(Collider2D other){

		if (other.name == "Ship" && !tutorialDone && !triggered) {

			//trigger two tutorial
			canvas.gameObject.SetActive (true);
			triggered = true;

			disableGame ();

			startTime = 0;
			dialogue = "";
			currChar = 0;

		}

	}

	void OnTriggerExit2D(Collider2D other){
		if (other.name == "Ship") {
			tutorialDone = true;
		}
	}

	// Update is called once per frame
	void Update () {
		timeCount++;
		if (triggered) {

			if (currentFrame < tutorials.Length) {
				nameText.text = tutorials [currentFrame].CharacterName;

				if (tutorials [currentFrame].CharacterName == "Sharpie") {
					avatarPanel.SetActive(true);
					avatar.GetComponent<Image> ().sprite = Resources.Load ("sharpie", typeof(Sprite)) as Sprite;
				} else if (tutorials [currentFrame].CharacterName == "Painty") {
					avatarPanel.SetActive(true);
					avatar.GetComponent<Image> ().sprite = Resources.Load ("painty", typeof(Sprite)) as Sprite;
				} else {
					avatarPanel.SetActive(false);
				}

				if (currChar == 0) {
					charArray = tutorials [currentFrame].Dialogue.ToCharArray ();
					charArrayLength = charArray.Length;
				}

				if (currChar < charArrayLength) {
					if (timeCount - startTime > textSpeed) {
						dialogue += charArray [currChar];
						dialogueText.text = newLineReplace(dialogue);
						currChar++;
						startTime = timeCount;
					}

				}
			}

			if (InputManager.GetButtonDown ("Submit") && !tutorialDone) {
				continueGame ();
			}
		}
	}

	private string newLineReplace(string InText)
	{
		bool newLinesRemaining = true;

		while(newLinesRemaining)
		{
			int CIndex = InText.IndexOf("\\n"); // Gets the Index of "\n" 


			if(CIndex == -1)
			{
				newLinesRemaining = false;
			}
			else
			{
				InText = InText.Remove(CIndex, 2); // Removes "\n from original String"
				InText = InText.Insert(CIndex, "\n"); // Adds the actual New Line symbol
			}
		}

		return InText;
	}

	private void disableGame(){

		Time.timeScale = 0;
		ship.GetComponent<Timer> ().enabled = false;

		ship.GetComponent<ShipActions> ().enabled = false;

		Transform wheel = ship.transform.FindChild ("Wheel");
		wheel.GetComponent<WheelActions> ().enabled = false;
	}

	private void continueGame(){
		Time.timeScale = 1;

		ship.GetComponent<Timer> ().enabled = true;
		ship.GetComponent<ShipActions> ().enabled = true;

		Transform wheel = ship.transform.FindChild ("Wheel");
		wheel.GetComponent<WheelActions> ().enabled = true;

		canvas.gameObject.SetActive (false);
	}

}
