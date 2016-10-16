using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using TeamUtility.IO;

public class TriggerAreaOne : MonoBehaviour {

	public TutorialDialogueEntity[] tutorials;
	public Text nameText;
	public Text dialogueText;
	private float textSpeed = 0.2f;
	public GameObject ship;
	public GameObject avatar;
	public GameObject avatarPanel;
	public GameObject canvas;
	public GameObject instruction;

	private GameObject wheel;

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

			wheel = ship.transform.FindChild ("Wheel").gameObject;

		}

	}

	
	// Update is called once per frame
	void Update () {
		timeCount++;
		if (triggered) {

			if (wheel.GetComponent<WheelActions> ().wheelUsed) {
				instruction.GetComponentInChildren<Text> ().text = "Move the Ship Forward";
			}

			if (ship.GetComponent<Rigidbody2D>().velocity.magnitude > 0){
				instruction.SetActive (false);
				tutorialDone = true;
				this.gameObject.SetActive (false);
			}

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

				if (currentFrame == 0) {
					ship.transform.FindChild ("Wheel").gameObject.SetActive (true);
					canvas.SetActive (false);
					instruction.GetComponentInChildren<Text> ().text = "Interact with the Wheel station";
					instruction.SetActive (true);
					continueGame ();
				}

				currentFrame++;
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
		ship.GetComponent<Rigidbody2D> ().velocity = new Vector2(0,0);
		ship.GetComponent<ShipActions> ().enabled = false;

		Transform wheel = ship.transform.FindChild ("Wheel");
		wheel.GetComponent<WheelActions> ().enabled = false;
	}

	private void continueGame(){
		Time.timeScale = 1;
		ship.GetComponent<ShipActions> ().enabled = true;

		Transform wheel = ship.transform.FindChild ("Wheel");
		wheel.GetComponent<WheelActions> ().enabled = true;

		canvas.gameObject.SetActive (false);
	}
}

[Serializable]
public class TutorialDialogueEntity
{
	[SerializeField]
	private string characterName;
	[SerializeField]
	private string dialogue;

	public string CharacterName
	{
		get
		{
			return characterName;
		}

		set
		{
			characterName = value;
		}
	}

	public string Dialogue
	{
		get
		{
			return dialogue;
		}

		set
		{
			dialogue = value;
		}
	}

}
