using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using TeamUtility.IO;

public class TriggerAreaThree : MonoBehaviour {

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
	private GameObject portCannon;
	private GameObject starCannon;
	private GameObject fishOne;
	private GameObject fishTwo;
	private GameObject toolBox;
	private GameObject dynamite;
	private GameObject fishGroup;

	private char[] charArray;
	private int charArrayLength;
	private int currChar = 0;
	private float startTime;
	private string dialogue;
	private bool triggered = false;
	private bool tutorialDone = false;
	private bool pauseInstructions = false;

	private bool triggeredFishOne 	= false;
	private bool triggeredFishTwo 	= false;
	private bool triggeredFishGroup = false;
	private bool triggeredToolBox	= false;
	private bool triggeredDynamite	= false;
	private int currentFrame = 0;

	private int timeCount = 0; //time scale is disabled, use a running number as time scale

	void OnTriggerEnter2D(Collider2D other){

		if (other.name == "Ship" && !tutorialDone && !triggered) {

			//trigger two tutorial
			canvas.gameObject.SetActive (true);
			triggered = true;

			startTime = 0;
			dialogue = "";
			currChar = 0;

			wheel 		= ship.transform.FindChild ("Wheel").gameObject;
			portCannon 	= ship.transform.FindChild ("PortCannon").gameObject;
			starCannon  = ship.transform.FindChild ("StarboardCannon").gameObject;
			toolBox		= ship.transform.FindChild ("Toolbox").gameObject;
			fishOne		= ship.transform.FindChild ("FlyingFish (1)").gameObject;
			fishTwo		= ship.transform.FindChild ("FlyingFish (2)").gameObject;
			fishGroup	= ship.transform.FindChild ("FlyingFishGroup").gameObject;
			dynamite	= ship.transform.FindChild ("Dynamite").gameObject;

			disableGame ();

		}

	}

	void OnTriggerExit2D(Collider2D other){
		if (other.name == "Ship") {
			tutorialDone = true;
		}
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		timeCount++;

		if (triggered) {

			if(currentFrame < 2 && (portCannon.GetComponent<PortCannonActions> ().cannonUsed || 
				starCannon.GetComponent<StarboardCannonActions> ().cannonUsed)) {
				instruction.SetActive (false);
			}

			if (fishOne == null && !triggeredFishOne) {
				dialogueText.text = "";
				dialogue = "";
				currChar = 0;
				triggeredFishOne = true;

				fishTwo.SetActive (true);
			}

			if (triggeredFishOne && fishTwo == null && !triggeredFishTwo) {
				currentFrame++;
				dialogueText.text = "";
				dialogue = "";
				currChar = 0;
				triggeredFishTwo = true;

				canvas.SetActive (true);
				pauseInstructions = false;
			}

			if (triggeredFishOne && triggeredFishTwo && 
				toolBox.GetComponent<ToolboxActions> ().toolboxUsed && !triggeredToolBox) {

				fishGroup.SetActive (true);
				currentFrame++;
				dialogueText.text = "";
				dialogue = "";
				currChar = 0;
				triggeredToolBox = true;

				canvas.SetActive (true);
				instruction.SetActive (false);
				pauseInstructions = false;

			}

			if (triggeredFishOne && triggeredFishTwo && triggeredToolBox &&
				fishGroup.GetComponentsInChildren<Transform> ().Length < 8 && 
				!triggeredDynamite) {


				currentFrame++;
				dialogueText.text = "";
				dialogue = "";
				currChar = 0;

				triggeredDynamite = true;

				canvas.SetActive (true);
				instruction.SetActive (false);
				pauseInstructions = false;

				wheel.GetComponent<WheelActions> ().enabled = true;

			}

			playInstruction ();

			if (InputManager.GetButtonDown ("Submit") && !tutorialDone && canvas.active) {
				playTutorial ();
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
		wheel.GetComponent<WheelActions> ().enabled = false;
	}

	private void continueGame(){
		pauseInstructions = true;

		Time.timeScale = 1;
		ship.GetComponent<Timer> ().enabled = true;

		canvas.gameObject.SetActive (false);
	}

	private void playInstruction(){

		if (currentFrame >= tutorials.Length || pauseInstructions) {
			return;
		}

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
				dialogueText.text = newLineReplace (dialogue);
				currChar++;
				startTime = timeCount;
			}

		}
	}

	private void playTutorial(){
		switch (currentFrame) {
		case 0:
			currentFrame++;
			fishOne.SetActive (true);
			portCannon.SetActive (true);
			starCannon.SetActive (true);

			dialogueText.text = "";
			dialogue = "";
			currChar = 0;
			break;
		case 1: //let the player kill the flying fish
			continueGame ();

			instruction.GetComponentInChildren<Text> ().text = "Interact with the cannon";
			instruction.SetActive (true);
			break;
		case 2: //continue dialogue
			currentFrame++;
			dialogueText.text = "";
			dialogue = "";
			currChar = 0;
			break;
		case 3:
			continueGame ();
			toolBox.SetActive (true);

			instruction.GetComponentInChildren<Text> ().text = "Disengage the current station";
			instruction.SetActive (true);
			break;

		case 4:
			currentFrame++;
			dialogueText.text = "";
			dialogue = "";
			currChar = 0;
			break;

		case 5:
			currentFrame++;
			dialogueText.text = "";
			dialogue = "";
			currChar = 0;
			break;

		case 6: //Dynamite
			continueGame ();
			dynamite.SetActive (true);
			instruction.GetComponentInChildren<Text> ().text = "Disengage the current station";
			instruction.SetActive (true);
			break;

		case 7: //Dynamite
			continueGame ();
			break;

		case 9:
			triggered = false;
			tutorialDone = true;
			break;
		}
			
	}
}
