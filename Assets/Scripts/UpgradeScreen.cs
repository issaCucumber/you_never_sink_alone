using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;

public class UpgradeScreen : MonoBehaviour
{

    public Transform upgradeScreen;
    public Transform playerObj;

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

    public Button leftPurchasedButton;
    public Button middlePurchasedButton;
    public Button rightPurchasedButton;

    Dictionary<int, int> stationsAndLevel;
    int maxLevel = 5 ;
    ShipActions ship;
    // Use this for initialization
    void Start()
    {
        ship = playerObj.GetComponent<ShipActions>();
    }

    // Update is called once per frame
    void Update()
    {
        int currentPrestige = ship.GetCurrentPrestige();
        prestigeLeftText.text = System.Convert.ToString(currentPrestige);

        DisablePurchaseButton();
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

            leftPurchasedButton.interactable = true;
            middlePurchasedButton.interactable = true;
            rightPurchasedButton.interactable = true;

            leftUpgradeText.text = GetUpgradeText(leftPair.Key);
            int leftLevel = leftPair.Value + 1;
            leftPrestigeText.text = System.Convert.ToString(200 * leftLevel);

            middleUpgradeText.text = GetUpgradeText(middlePair.Key);
            int middleLevel = middlePair.Value + 1;
            middlePrestigeText.text = System.Convert.ToString(200 * middleLevel);

            rightUpgradeText.text = GetUpgradeText(rightPair.Key);
            int rightLevel = rightPair.Value + 1;
            rightPrestigeText.text = System.Convert.ToString(200 * rightLevel);

            DisablePurchaseButton();

            upgradeScreen.gameObject.SetActive(true);
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
            leftPurchasedButton.interactable = false;
        }

        int middlePrestigeValue = System.Convert.ToInt32(middlePrestigeText.text);
        if(currentPrestige < middlePrestigeValue)
        {
            middlePurchasedButton.interactable = false;
        }

        int rightPrestigeValue = System.Convert.ToInt32(rightPrestigeText.text);
        if(currentPrestige < rightPrestigeValue)
        {
            rightPurchasedButton.interactable = false;
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
        if (obj.name.Equals("Ship"))
        {
            stationsAndLevel = new Dictionary<int, int>();
            Debug.Log("Calling upgrade screen");
            Pause();
        }
    }
}
