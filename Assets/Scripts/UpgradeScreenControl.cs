using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UpgradeScreenControl : MonoBehaviour {

    public Transform playerObj;
    public Transform upgradeScreen;

    public Text leftUpgradeName;
    public Text leftPrestigeText;
    public Button leftPurchasedButton;
    public Text middleUpgradeName;
    public Text middlePrestigeText;
    public Button middlePurchasedButton;
    public Text rightUpgradeName;
    public Text rightPrestigeText;
    public Button rightPurchasedButton;

    public GameObject[] upgradeTrigger;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    private void UpgradeShip(string text)
    {
        ShipActions ship = playerObj.GetComponent<ShipActions>();
        if(text.Equals(Constants.HULL))
        {
            ship.hulllevel++;
        }
        else if(text.Equals(Constants.DYNAMITE))
        {
            ship.dynamitelevel++;
        }
        else if (text.Equals(Constants.PORTFIRERATE))
        {
            ship.portcannonfireratelevel++;
        }
        else if (text.Equals(Constants.PORTPOWER))
        {
            ship.portcannonpowerlevel++;
        }
        else if (text.Equals(Constants.STARBOARDFIRERATE))
        {
            ship.starboardcannonfireratelevel++;
        }
        else if(text.Equals(Constants.STARBOARDPOWER))
        {
            ship.starboardcannonpowerlevel++;
        }
        else if(text.Equals(Constants.TOOLBOX))
        {
            ship.toolboxlevel++;
        }
        else if(text.Equals(Constants.WHEEL))
        {
            ship.wheellevel++;
        }


    }

    public void OnClickLeave()
    {
        upgradeScreen.gameObject.SetActive(false);
        Time.timeScale = 1;
        playerObj.GetComponent<Timer>().enabled = true;
    }

    public void OnClickLeftPurchaseButton()
    {
        int prestige = System.Convert.ToInt32(leftPrestigeText.text);
        ShipActions ship = playerObj.GetComponent<ShipActions>();
        ship.prestigevalue -= prestige;
        UpgradeShip(leftUpgradeName.text);
        leftPurchasedButton.interactable = false;
    }

    public void OnClickMiddlePurchaseButton()
    {
        int prestige = System.Convert.ToInt32(middlePrestigeText.text);
        ShipActions ship = playerObj.GetComponent<ShipActions>();
        ship.prestigevalue -= prestige;
        UpgradeShip(middleUpgradeName.text);
        middlePurchasedButton.interactable = false;
    }

    public void OnClickRightPurchaseButton()
    {
        int prestige = System.Convert.ToInt32(rightPrestigeText.text);
        ShipActions ship = playerObj.GetComponent<ShipActions>();
        ship.prestigevalue -= prestige;
        UpgradeShip(rightUpgradeName.text);
        rightPurchasedButton.interactable = false;
    }

    public void OnClickSearch()
    {
        int islandNo = PlayerPrefs.GetInt(Constants.ISLAND, 0);
        UpgradeScreen screen = upgradeTrigger[islandNo].GetComponent<UpgradeScreen>();
        screen.startTime = Time.realtimeSinceStartup;
		Debug.Log ("Search button At island " + islandNo);
		screen.SetState (2);
		//screen.currentState = screen.MenuStates.Search;
    }

    public void OnClickUpgrade()
    {
        int islandNo = PlayerPrefs.GetInt(Constants.ISLAND, 0);
        UpgradeScreen screen = upgradeTrigger[islandNo].GetComponent<UpgradeScreen>();
		//screen.currentState = screen.MenuStates.Shop;
		Debug.Log ("Upgrade button At island " + islandNo);
		screen.SetState (3);
        screen.SetEventSelected();
    }

    public void OnClickBack()
    {
        int islandNo = PlayerPrefs.GetInt(Constants.ISLAND, 0);
        UpgradeScreen screen = upgradeTrigger[islandNo].GetComponent<UpgradeScreen>();
		//screen.currentState = screen.MenuStates.Main;
		screen.SetState (1);
        screen.SetMainSelected();
    }

}
