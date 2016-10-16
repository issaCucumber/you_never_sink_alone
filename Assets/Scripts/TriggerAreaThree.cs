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
	public GameObject flyingfishes;

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
	
	// Update is called once per frame
	void Update () {
		timeCount++;

		if (triggered) {

			ship.GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, 0);
//			if (canvas.activeInHierarchy) {
//				ship.GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, 0);
//			}

			if(currentFrame < 2 && (portCannon.GetComponent<PortCannonActions> ().cannonUsed || 
				starCannon.GetComponent<StarboardCannonActions> ().cannonUsed)) {
				instruction.SetActive (false);
			}

			if (fishOne == null && !triggeredFishOne) {
				
				dialogueText.text = "";
				dialogue = "";
				currChar = 0;

				triggeredFishOne = true;

				currentFrame++;
				instruction.SetActive (false);
				canvas.SetActive (true);

				pauseInstructions = false;

				Debug.Log ("Kill Fish One");

				//fishTwo.SetActive (true);
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

				if (ship.GetComponent<ShipActions> ().hullcurrent != 100) {
					instruction.GetComponentInChildren<Text> ().text = "Engage the Toolbox to repair to 100";
					instruction.SetActive (true);
				} else {
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

			if (InputManager.GetButtonDown ("Submit") && !tutorialDone) {
				Debug.Log ("===================== ENTER KEY PRESSED ===================" + currentFrame);
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
//		ship.GetComponent<Rigidbody2D> ().velocity = new Vector2(0,0);
//		ship.GetComponent<Timer> ().enabled = false;
//		wheel.GetComponent<WheelActions> ().enabled = false;
	}

	private void continueGame(){
		pauseInstructions = true;

		Time.timeScale = 1;
//		ship.GetComponent<Timer> ().enabled = true;
//		wheel.GetComponent<WheelActions> ().enabled = false;

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
		case 0: //let the player kill the flying fish
			fishOne.SetActive (true);
			portCannon.SetActive (true);
			starCannon.SetActive (true);

			instruction.GetComponentInChildren<Text> ().text = "Interact with the cannon and Kill the Flying Fish!";
			instruction.SetActive (true);
			canvas.SetActive (false);
			pauseInstructions = true;

			continueGame ();
			break;
		case 1: //2nd fish attack!
			fishTwo.SetActive (true);

			canvas.SetActive (false);
			pauseInstructions = true;

			continueGame ();
			break;
		case 2: //After darn it, we got hit!
			instruction.GetComponentInChildren<Text> ().text = "Disengage the current station and Interact with the Toolbox";
			instruction.SetActive (true);

			canvas.SetActive (false);
			pauseInstructions = true;

			continueGame ();
			toolBox.SetActive (true);
			break;
		case 3:
			currentFrame++;
			dialogueText.text = "";
			dialogue = "";
			currChar = 0;
			break;
		case 4:
			continueGame ();
			dynamite.SetActive (true);
			instruction.GetComponentInChildren<Text> ().text = "Disengage the current station and Interact with the Dynamite";
			instruction.SetActive (true);
			break;
		case 5:
			continueGame ();
			tutorialDone = true;
			this.gameObject.SetActive (false);
			flyingfishes.SetActive (true);
			instruction.GetComponentInChildren<Text> ().text = "Reach the Destination Island. Fight any enemies along the way.";
			instruction.SetActive (true);
			break;
		}
			
	}
}
