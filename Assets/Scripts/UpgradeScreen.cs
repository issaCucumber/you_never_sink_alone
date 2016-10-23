using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TeamUtility.IO;

public class UpgradeScreen : MonoBehaviour
{
    public EventSystem ES;
    private GameObject storeSelected;

    public Transform upgradeScreen;
    public Transform playerObj;

    //common
    public int islandNo;
    public enum MenuStates { Main, Search, Shop};
    public MenuStates currentState;

    public GameObject mainIsland;
    public GameObject searchIsland;
    public GameObject upgradeMenu;

    //upgrades
    public Text leftUpgradeText;
    public Image leftUpgradeImage;  //to change use leftUpgradeImage.sprite = myFirstImage;
    public Text leftPrestigeText;
    public Text middleUpgradeText;
    public Image middleUpgradeImage;
    public Text middlePrestigeText;
    public Text rightUpgradeText;
    public Image rightUpgradeImage;
    public Text rightPrestigeText;

    public Text prestigeLeftText;

    //main
    public GameObject shopButton;
    public GameObject searchButton;

    //search crew
    public int totalNoOfCrewOnIsland;
    public int currCrewSaveOnIsland;
    public Text resultText;
    public Text crewText;
    public GameObject returnToIsland;

    public GameObject leftPurchasedButton;
    public GameObject middlePurchasedButton;
    public GameObject rightPurchasedButton;
    public GameObject leaveButton;

    public Sprite[] hullSprite;
    public Sprite[] wheelSprite;
    public Sprite[] portCannonSprite;
    public Sprite[] starboardCannonSprite;
    public Sprite[] toolboxSprite;
    public Sprite[] dynamiteSprite;

    Dictionary<int, int> stationsAndLevel;
    int maxLevel = 5 ;
    ShipActions ship;

    private float player1Time;
    private float player2Time;
    private bool player1Pressed;
    private bool player2Pressed;
    private bool triggeredBackToIsland;
    public float startTime;

    // Use this for initialization
    void Start()
    {
        currCrewSaveOnIsland = 0;
        SetMainSelected();
        ship = playerObj.GetComponent<ShipActions>();
    }

    // Update is called once per frame
    void Update()
    {
        if (upgradeScreen.gameObject.activeInHierarchy == true)
        {
			if (PlayerPrefs.GetInt (Constants.ISLAND, 0) == islandNo) {
				if (ES.currentSelectedGameObject != storeSelected) {
					if (ES.currentSelectedGameObject == null) {
						ES.SetSelectedGameObject (storeSelected);
					} else {
						storeSelected = ES.currentSelectedGameObject;
					}
				}

				int currentPrestige = ship.GetCurrentPrestige ();
				prestigeLeftText.text = System.Convert.ToString (currentPrestige);
				crewText.text = ship.crewsaved + "/" + ship.crewtosave;
            
				searchButton.GetComponent<Button> ().interactable = true;
				if (currCrewSaveOnIsland >= totalNoOfCrewOnIsland) {   //reach max no of crew on island
					searchButton.GetComponent<Button> ().interactable = false;
				}
				Debug.Log ("Current state " + currentState);
				if (currentState == MenuStates.Main) {
					mainIsland.SetActive (true);
					searchIsland.SetActive (false);
					upgradeMenu.SetActive (false);
					triggeredBackToIsland = false;
				} else if (currentState == MenuStates.Shop) {
					mainIsland.SetActive (false);
					searchIsland.SetActive (false);
					upgradeMenu.SetActive (true);
					triggeredBackToIsland = false;

					DisablePurchaseButton ();
				} else if (currentState == MenuStates.Search) {
					mainIsland.SetActive (false);
					searchIsland.SetActive (true);
					upgradeMenu.SetActive (false);

					int player1ControlNo = PlayerPrefs.GetInt ("Player1", 1);
					int player2ControlNo = PlayerPrefs.GetInt ("Player2", 2);

					if (player2ControlNo == 4) {
						if (player1ControlNo != 3) {
							player2ControlNo = 3;
						}
					}

					if (!triggeredBackToIsland) {
						returnToIsland.SetActive (false);
						if (Time.realtimeSinceStartup - startTime > 1) {
							if (InputManager.GetButtonDown ("Interact" + player1ControlNo) && !player1Pressed) {
								player1Pressed = true;
								player1Time = Time.realtimeSinceStartup;
								Debug.Log ("Player 1 Time is " + player1Time);
							}

							if (InputManager.GetButtonDown ("Interact" + player2ControlNo) && !player2Pressed) {
								player2Pressed = true;
								player2Time = Time.realtimeSinceStartup;
								Debug.Log ("Player 2 Time" + player2Time);
							}
						}

						if (player1Pressed && player2Pressed) {
							if (Mathf.Abs (player1Time - player2Time) < 0.1) {
								//success
								int number = totalNoOfCrewOnIsland - currCrewSaveOnIsland;
								int i = Random.Range (1, number);
								currCrewSaveOnIsland += i;
								resultText.text = "You have found " + i + " crews";

								ship.crewsaved += i;
							} else {
								if ((player1Time - player2Time) > 0) {
									resultText.text = "Painty pressed too fast";
								} else {
									resultText.text = "Sharpie pressed too fast";
								}
							}

							crewText.text = ship.crewsaved + "/" + ship.crewtosave;

							player1Pressed = false;
							player2Pressed = false;

							triggeredBackToIsland = true;
							startTime = Time.realtimeSinceStartup;
						}
					} else {
						//trigger back to island
						returnToIsland.SetActive (true);
						if (Time.realtimeSinceStartup - startTime > 2) {
							currentState = MenuStates.Main;
							resultText.text = "";
							ES.SetSelectedGameObject (shopButton);
						}
					}
				}
			}
        }
    }


    //possible upgrades
    
    //hull
    //toolbox
    //starboard cannon power
    //starboard fire rate
    //wheel
    //dynamite
    //port cannon power
    //port cannon fire rate
    private void Pause()
    {
        if (upgradeScreen.gameObject.activeInHierarchy == false)
        {         
            InitialiseStationAndLevel();
            Debug.Log("Initialise successfully");
            int[] uniqueNumber = GetThreeUniqueNumber();
            Debug.Log("No of unique Number: " + uniqueNumber.Count());

            KeyValuePair<int, int>[] stationsAndLevelArray = stationsAndLevel.ToArray();
            KeyValuePair<int, int> leftPair = stationsAndLevelArray[uniqueNumber[0]];
            KeyValuePair<int, int> middlePair = stationsAndLevelArray[uniqueNumber[1]];
            KeyValuePair<int, int> rightPair = stationsAndLevelArray[uniqueNumber[2]];

            Debug.Log("Left pair :" + leftPair.Key + " is level " + leftPair.Value);
            Debug.Log("Middle pair :" + middlePair.Key + " is level " + middlePair.Value);
            Debug.Log("Right pair :" + rightPair.Key + " is level " + rightPair.Value);

            leftPurchasedButton.GetComponent<Button>().interactable = true;
            middlePurchasedButton.GetComponent<Button>().interactable = true;
            rightPurchasedButton.GetComponent<Button>().interactable = true;

            leftUpgradeText.text = GetUpgradeText(leftPair.Key);
            int leftLevel = leftPair.Value + 1;
            ShowUpgradeImage(leftPair.Key, leftPair.Value, leftUpgradeImage);
            leftPrestigeText.text = System.Convert.ToString(200 * leftLevel);

            middleUpgradeText.text = GetUpgradeText(middlePair.Key);
            int middleLevel = middlePair.Value + 1;
            ShowUpgradeImage(middlePair.Key, middlePair.Value, middleUpgradeImage);
            middlePrestigeText.text = System.Convert.ToString(200 * middleLevel);

            rightUpgradeText.text = GetUpgradeText(rightPair.Key);
            int rightLevel = rightPair.Value + 1;
            ShowUpgradeImage(rightPair.Key, rightPair.Value, rightUpgradeImage);
            rightPrestigeText.text = System.Convert.ToString(200 * rightLevel);

            DisablePurchaseButton();
            upgradeScreen.gameObject.SetActive(true);
            ES.SetSelectedGameObject(shopButton);
            //SetEventSelected();
            Time.timeScale = 0;
            playerObj.GetComponent<Timer>().enabled = false;
        }
    }

    private void DisablePurchaseButton()
    {
        int currentPrestige = ship.GetCurrentPrestige();

        int leftPrestigeValue = System.Convert.ToInt32(leftPrestigeText.text);
        if(currentPrestige < leftPrestigeValue)
        {
            leftPurchasedButton.GetComponent<Button>().interactable = false;
        }

        int middlePrestigeValue = System.Convert.ToInt32(middlePrestigeText.text);
        if(currentPrestige < middlePrestigeValue)
        {
            middlePurchasedButton.GetComponent<Button>().interactable = false;
        }

        int rightPrestigeValue = System.Convert.ToInt32(rightPrestigeText.text);
        if(currentPrestige < rightPrestigeValue)
        {
            rightPurchasedButton.GetComponent<Button>().interactable = false;
        }
    }

    public void SetEventSelected()
    {
        if(leftPurchasedButton.GetComponent<Button>().IsInteractable())
        {
            storeSelected = leftPurchasedButton;
        }
        else
        {
            if(middlePurchasedButton.GetComponent<Button>().IsInteractable())
            {
                storeSelected = middlePurchasedButton;
            }
            else
            {
                if(rightPurchasedButton.GetComponent<Button>().IsInteractable())
                {
                    storeSelected = rightPurchasedButton;
                }
                else
                {
                    storeSelected = leaveButton;
                }
            }
        }
        ES.SetSelectedGameObject(storeSelected);
    }

    public void SetMainSelected()
    {
        ES.SetSelectedGameObject(shopButton);
    }

	public void SetState(int state)
	{
		if (state == 1) {
			currentState = MenuStates.Main;
		} else if (state == 2) {
			currentState = MenuStates.Search;
		} else if (state == 3) {
			currentState = MenuStates.Shop;
		}
	}

    private void ShowUpgradeImage(int item, int level, Image image)
    {
        if (item == 0)
        {
            image.sprite = hullSprite[level];
        }
        else if (item == 1)
        {
            image.sprite = toolboxSprite[level];
        }
        else if (item == 2)
        {
            image.sprite = starboardCannonSprite[level];
        }
        else if (item == 3)
        {
            image.sprite = starboardCannonSprite[level];
        }
        else if (item == 4)
        {
            image.sprite = wheelSprite[level];
        }
        else if (item == 5)
        {
            image.sprite = dynamiteSprite[level];
        }
        else if (item == 6)
        {
            image.sprite = portCannonSprite[level];
        }
        else if (item == 7)
        {
            image.sprite = portCannonSprite[level];
        }
    }

    private string GetUpgradeText(int value)
    {
        string upgradeText = "";
        if (value == 0)
        {
            upgradeText = Constants.HULL;
        }
        else if(value == 1)
        {
            upgradeText = Constants.TOOLBOX;
        }
        else if (value == 2)
        {
            upgradeText = Constants.STARBOARDPOWER;
        }
        else if (value == 3)
        {
            upgradeText = Constants.STARBOARDFIRERATE;
        }
        else if (value == 4)
        {
            upgradeText = Constants.WHEEL;
        }
        else if(value == 5)
        {
            upgradeText = Constants.DYNAMITE;
        }
        else if (value == 6)
        {
            upgradeText = Constants.PORTPOWER;
        }
        else if(value == 7)
        {
            upgradeText = Constants.PORTFIRERATE;
        }

        return upgradeText;
    }

    private int[] GetThreeUniqueNumber()
    {
        int size = stationsAndLevel.Count();

        List<int> randomList = new List<int>();
        while (randomList.Count() < 3)
        {
            int i = Random.Range(0, size);
            if(!randomList.Contains(i))
            {
                randomList.Add(i);
            }
        }

        return randomList.ToArray() ;
    }

    private void InitialiseStationAndLevel()
    {
        int hullLevel = ship.hulllevel;
        int toolBoxLevel = ship.toolboxlevel;
        int starboardCannonPowerLevel = ship.starboardcannonpowerlevel;
        int starboardFireRateLevel = ship.starboardcannonfireratelevel;
        int wheelLevel = ship.wheellevel;
        int dynamiteLevel = ship.dynamitelevel;
        int portCannonPowerLevel = ship.portcannonpowerlevel;
        int portCannonFireRateLevel = ship.portcannonfireratelevel;

        if (hullLevel < maxLevel)
        {
            stationsAndLevel.Add(0, hullLevel);
        }
        if(toolBoxLevel < maxLevel)
        {
            stationsAndLevel.Add(1, toolBoxLevel);
        }
        if(starboardCannonPowerLevel < maxLevel)
        {
            stationsAndLevel.Add(2, starboardCannonPowerLevel);
        }
        if(starboardFireRateLevel < maxLevel)
        {
            stationsAndLevel.Add(3, starboardFireRateLevel);
        }
        if(wheelLevel < maxLevel)
        {
            stationsAndLevel.Add(4, wheelLevel);
        }
        if(dynamiteLevel < maxLevel)
        {
            stationsAndLevel.Add(5, dynamiteLevel);
        }
        if(portCannonPowerLevel < maxLevel)
        {
            stationsAndLevel.Add(6, portCannonPowerLevel);
        }
        if(portCannonFireRateLevel < maxLevel)
        {
            stationsAndLevel.Add(7, portCannonFireRateLevel);
        }
    }

    private void UnPause()
    {
        upgradeScreen.gameObject.SetActive(false);
        Time.timeScale = 1;
        playerObj.GetComponent<Timer>().enabled = true;
    }

    void OnTriggerEnter2D(Collider2D obj)
	{
		if (obj.name.Equals ("Ship")) {
			if (islandNo == 4) {
				if (PlayerPrefs.GetInt (Constants.DEFEATDRAGON, 0) == 1) {
					PlayerPrefs.SetInt (Constants.ISLAND, islandNo);
					stationsAndLevel = new Dictionary<int, int> ();
					Debug.Log ("Calling upgrade screen");
					Pause ();	
				}
			
			} else {
			
				PlayerPrefs.SetInt (Constants.ISLAND, islandNo);
				stationsAndLevel = new Dictionary<int, int> ();
				Debug.Log ("Calling upgrade screen");
				Pause ();
			}
		}
	}
}
