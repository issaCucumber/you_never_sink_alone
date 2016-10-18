using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using TeamUtility.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseGame : MonoBehaviour {

    public EventSystem ES;
    private GameObject storeSelected;

    private enum MenuStates { Main, Status, Encyclopedia, Options, Audios, Controls};
    private MenuStates currentState;

    public Transform canvas;
    public Transform playerObj;

    public GameObject mainScreen;
    public GameObject statusScreen;
    public GameObject encyclopediaScreen;
    public GameObject optionsScreen;
    public GameObject audioOptionScreen;
    public GameObject controlOptionScreen;

    public GameObject resumeBtn;
    public GameObject firstSelectedInAudio;
    public GameObject firstSelectedInControl;
    public GameObject firstSelectedInOption;

    public GameObject keyboard1Radio;
    public GameObject controller1Radio;
    public GameObject keyboard2Radio;
    public GameObject controller2Radio;
    public GameObject keyboard1Control;
    public GameObject controller1Control;
    public GameObject keyboard2Control;
    public GameObject controller2Control;

    public Sprite normalRadio;
    public Sprite selectedRadio;

    public Sprite highlightPort;
    public Sprite normalPort;
    public Sprite highlightStarboard;
    public Sprite normalStarBoard;
    public Sprite highlightHull;
    public Sprite normalHull;
    public Sprite highlightToolBox;
    public Sprite normalToolBox;
    public Sprite highlightDynamite;
    public Sprite normalDynamite;
    public Sprite highlightWheel;
    public Sprite normalWheel;

    public GameObject portPowerLevelButton;
    public GameObject portFireRateButton;
    public GameObject starBoardPowerLevelButton;
    public GameObject starBoardFireRateButton;
    public GameObject hullLevelButton;
    public GameObject toolBoxLevelButton;
    public GameObject dynamiteLevelButton;
    public GameObject wheelLevelButton;

    public GameObject portCannonImage;
    public GameObject starBoardCannonImage;
    public GameObject hullImage;
    public GameObject toolBoxImage;
    public GameObject dynamiteImage;
    public GameObject wheelImage;

    public Sprite revealFlyingFish;
    public Sprite revealElectricEel;
    public Sprite revealOctupus;
    public Sprite revealJellyfish;
    public Sprite revealDragon;

    public GameObject flyingFishImage;
    public GameObject electricEelImage;
    public GameObject octupusImage;
    public GameObject jellyfishImage;
    public GameObject dragonImage;

    private int player1Control;
    private int player2Control;

    float masterVolumePercent = 1f;
    float sfxVolumePercent = 1f;
    float musicVolumePercent = 1f;

    public GameObject masterSlider;
    public GameObject musicSlider;
    public GameObject fxSlider;

    private ShipActions ship;

    public Sprite[] highlightPortPowerLevelImage;
    public Sprite[] normalPortPowerLevelImage;
    public Sprite[] highlightPortFireRateLevelImage;
    public Sprite[] normalPortFireRateLevelImage;
    public Sprite[] highlightStarboardPowerLevelImage;
    public Sprite[] normalStarboardPowerLevelImage;
    public Sprite[] highlightStarboardFireRateLevelImage;
    public Sprite[] normalStarboardFireRateLevelImage;
    public Sprite[] highlightHullLevelImage;
    public Sprite[] normalHullLevelImage;
    public Sprite[] highlightToolBoxLevelImage;
    public Sprite[] normalToolBoxLevelImage;
    public Sprite[] highlightDynamiteLevelImage;
    public Sprite[] normalDynamiteLevelImage;
    public Sprite[] highlightWheelLevelImage;
    public Sprite[] normalWheelLevelImage;


    // Use this for initialization
    void Start () {
        //storeSelected = ES.firstSelectedGameObject;
        ship = playerObj.GetComponent<ShipActions>();
    }
	
	// Update is called once per frame
	void Update () {

	    if(InputManager.GetButtonDown("Pause"))
        {
            Pause();
        }
        if (ES.currentSelectedGameObject != storeSelected)
        {
            if (ES.currentSelectedGameObject == null)
            {
                ES.SetSelectedGameObject(storeSelected);
            }
            else
            {
                storeSelected = ES.currentSelectedGameObject;
            }
        }
        if (currentState == MenuStates.Main)
        {
            mainScreen.SetActive(true);
            statusScreen.SetActive(false);
            encyclopediaScreen.SetActive(false);
            optionsScreen.SetActive(false);
            audioOptionScreen.SetActive(false);
            controlOptionScreen.SetActive(false);
        }
        else if(currentState == MenuStates.Status)
        {
            mainScreen.SetActive(false);
            statusScreen.SetActive(true);
            encyclopediaScreen.SetActive(false);
            optionsScreen.SetActive(false);
            audioOptionScreen.SetActive(false);
            controlOptionScreen.SetActive(false);
            CheckCurrentHighLightState();
        }
        else if(currentState == MenuStates.Encyclopedia)
        {
            mainScreen.SetActive(false);
            statusScreen.SetActive(false);
            encyclopediaScreen.SetActive(true);
            optionsScreen.SetActive(false);
            audioOptionScreen.SetActive(false);
            controlOptionScreen.SetActive(false);
        }
        else if(currentState == MenuStates.Options)
        {
            mainScreen.SetActive(false);
            statusScreen.SetActive(false);
            encyclopediaScreen.SetActive(false);
            optionsScreen.SetActive(true);
            audioOptionScreen.SetActive(false);
            controlOptionScreen.SetActive(false);
        }
        else if(currentState == MenuStates.Audios)
        {
            mainScreen.SetActive(false);
            statusScreen.SetActive(false);
            encyclopediaScreen.SetActive(false);
            optionsScreen.SetActive(false);
            audioOptionScreen.SetActive(true);
            controlOptionScreen.SetActive(false);
        }
        else if(currentState == MenuStates.Controls)
        {
            SetPlayer1Radio(player1Control);
            SetPlayer2Radio(player2Control);

            mainScreen.SetActive(false);
            statusScreen.SetActive(false);
            encyclopediaScreen.SetActive(false);
            optionsScreen.SetActive(false);
            audioOptionScreen.SetActive(false);
            controlOptionScreen.SetActive(true);
        }
    }

    private void CheckCurrentHighLightState()
    {
        portCannonImage.GetComponent<Image>().sprite = normalPort;
        starBoardCannonImage.GetComponent<Image>().sprite = normalStarBoard;
        hullImage.GetComponent<Image>().sprite = normalHull;
        toolBoxImage.GetComponent<Image>().sprite = normalToolBox;
        dynamiteImage.GetComponent<Image>().sprite = normalDynamite;
        wheelImage.GetComponent<Image>().sprite = normalWheel;

        if (ES.currentSelectedGameObject == portPowerLevelButton)
        {
            Debug.Log("highlight port power level");
            portCannonImage.GetComponent<Image>().sprite = highlightPort;

        }   
        else if(ES.currentSelectedGameObject == portFireRateButton)
        {
            Debug.Log("highlight portFireRateButton");
            portCannonImage.GetComponent<Image>().sprite = highlightPort;
   
        }
        else if (ES.currentSelectedGameObject == starBoardPowerLevelButton)
        {
            Debug.Log("highlight starBoardPowerLevelButton");
            starBoardCannonImage.GetComponent<Image>().sprite = highlightStarboard;
 
        }
        else if (ES.currentSelectedGameObject == starBoardFireRateButton)
        {
            Debug.Log("highlightstarBoardFireRateButton");
            starBoardCannonImage.GetComponent<Image>().sprite = highlightStarboard;

        }
        else if (ES.currentSelectedGameObject == hullLevelButton)
        {
            Debug.Log("highlight hullLevelButton");
            hullImage.GetComponent<Image>().sprite = highlightHull;
        }
        else if (ES.currentSelectedGameObject == toolBoxLevelButton)
        {
            Debug.Log("highlight toolBoxLevelButton");
            toolBoxImage.GetComponent<Image>().sprite = highlightToolBox;

        }
        else if (ES.currentSelectedGameObject == dynamiteLevelButton)
        {
            Debug.Log("highlight dynamiteLevelButton");
            dynamiteImage.GetComponent<Image>().sprite = highlightDynamite;

        }
        else if (ES.currentSelectedGameObject == wheelLevelButton)
        {
            Debug.Log("highlight wheelLevelButton");
            wheelImage.GetComponent<Image>().sprite = highlightWheel;
        }

}


    private void SetPlayer1Radio(int player1Control)
    {
        if (player1Control == 1)
        {
            keyboard1Radio.GetComponent<Image>().sprite = selectedRadio;
            controller1Radio.GetComponent<Image>().sprite = normalRadio;
            keyboard1Control.SetActive(true);
            controller1Control.SetActive(false);

        }
        else if (player1Control == 3)
        {
            keyboard1Radio.GetComponent<Image>().sprite = normalRadio;
            controller1Radio.GetComponent<Image>().sprite = selectedRadio;
            keyboard1Control.SetActive(false);
            controller1Control.SetActive(true);
        }
    }

    private void SetPlayer2Radio(int player2Control)
    {
        if (player2Control == 2)
        {
            keyboard2Radio.GetComponent<Image>().sprite = selectedRadio;
            controller2Radio.GetComponent<Image>().sprite = normalRadio;
            keyboard2Control.SetActive(true);
            controller2Control.SetActive(false);
        }
        else if (player2Control == 4)
        {
            keyboard2Radio.GetComponent<Image>().sprite = normalRadio;
            controller2Radio.GetComponent<Image>().sprite = selectedRadio;
            keyboard2Control.SetActive(false);
            controller2Control.SetActive(true);
        }
    }

    private void Pause()
    {
        if (canvas.gameObject.activeInHierarchy == false)
        {
            canvas.gameObject.SetActive(true);
            Time.timeScale = 0;
			playerObj.GetComponent<Timer>().enabled = false;
            ES.SetSelectedGameObject(resumeBtn);
            //refresh state
            storeSelected = resumeBtn;
            RevealEnemies();
            ShowCorrectSpriteForStatus();
            currentState = MenuStates.Main;
            player1Control = PlayerPrefs.GetInt("Player1", 1);  //1 is keyboard1, 2 is keyboard2, 3 is controller1, 4 is controller2
            player2Control = PlayerPrefs.GetInt("Player2", 2);
        }
        else
        {
            canvas.gameObject.SetActive(false);
            Time.timeScale = 1;
			playerObj.GetComponent<Timer>().enabled = true;
        }
    }

    private void ShowCorrectSpriteForStatus()
    {
        int portPowerLevel = ship.portcannonpowerlevel;
        int portFireRateLevel = ship.portcannonfireratelevel;
        int starboardPowerLevel = ship.starboardcannonpowerlevel;
        int starboardFireRateLevel = ship.starboardcannonfireratelevel;
        int hullLevel = ship.hulllevel;
        int toolBoxLevel = ship.toolboxlevel;
        int dynamiteLevel = ship.dynamitelevel;
        int wheelLevel = ship.wheellevel;

        SpriteState spriteState = new SpriteState();
        portPowerLevelButton.GetComponent<Image>().sprite = normalPortPowerLevelImage[portPowerLevel - 1];
        spriteState = portPowerLevelButton.GetComponent<Button>().spriteState;
        spriteState.highlightedSprite = highlightPortPowerLevelImage[portPowerLevel - 1];
        portPowerLevelButton.GetComponent<Button>().spriteState = spriteState;

        spriteState = new SpriteState();
        portFireRateButton.GetComponent<Image>().sprite = normalPortFireRateLevelImage[portFireRateLevel - 1];
        spriteState = portFireRateButton.GetComponent<Button>().spriteState;
        spriteState.highlightedSprite = highlightPortFireRateLevelImage[portFireRateLevel - 1];
        portFireRateButton.GetComponent<Button>().spriteState = spriteState;

        spriteState = new SpriteState();
        starBoardPowerLevelButton.GetComponent<Image>().sprite = normalStarboardPowerLevelImage[starboardPowerLevel - 1];
        spriteState = starBoardPowerLevelButton.GetComponent<Button>().spriteState;
        spriteState.highlightedSprite = highlightStarboardPowerLevelImage[starboardPowerLevel - 1];
        starBoardPowerLevelButton.GetComponent<Button>().spriteState = spriteState;

        spriteState = new SpriteState();
        starBoardFireRateButton.GetComponent<Image>().sprite = normalStarboardFireRateLevelImage[starboardFireRateLevel - 1];
        spriteState = starBoardFireRateButton.GetComponent<Button>().spriteState;
        spriteState.highlightedSprite = highlightStarboardFireRateLevelImage[starboardFireRateLevel - 1];
        starBoardFireRateButton.GetComponent<Button>().spriteState = spriteState;

        spriteState = new SpriteState();
        hullLevelButton.GetComponent<Image>().sprite = normalHullLevelImage[hullLevel - 1];
        spriteState = hullLevelButton.GetComponent<Button>().spriteState;
        spriteState.highlightedSprite = highlightHullLevelImage[hullLevel - 1];
        hullLevelButton.GetComponent<Button>().spriteState = spriteState;

        spriteState = new SpriteState();
        toolBoxLevelButton.GetComponent<Image>().sprite = normalToolBoxLevelImage[toolBoxLevel - 1];
        spriteState = toolBoxLevelButton.GetComponent<Button>().spriteState;
        spriteState.highlightedSprite = highlightToolBoxLevelImage[toolBoxLevel - 1];
        toolBoxLevelButton.GetComponent<Button>().spriteState = spriteState;

        spriteState = new SpriteState();
        dynamiteLevelButton.GetComponent<Image>().sprite = normalDynamiteLevelImage[dynamiteLevel - 1];
        spriteState = dynamiteLevelButton.GetComponent<Button>().spriteState;
        spriteState.highlightedSprite = highlightDynamiteLevelImage[dynamiteLevel - 1];
        dynamiteLevelButton.GetComponent<Button>().spriteState = spriteState;

        spriteState = new SpriteState();
        wheelLevelButton.GetComponent<Image>().sprite = normalWheelLevelImage[wheelLevel - 1];
        spriteState = wheelLevelButton.GetComponent<Button>().spriteState;
        spriteState.highlightedSprite = highlightWheelLevelImage[wheelLevel - 1];
        wheelLevelButton.GetComponent<Button>().spriteState = spriteState;
}

    private void RevealEnemies()
    {
        int flyingFishSeen = PlayerPrefs.GetInt(Constants.FLYINGFISHSEEN, 0);
        if(flyingFishSeen > 0)
        {
            flyingFishImage.GetComponent<Image>().sprite = revealFlyingFish;
        }

        int electricEelSeen = PlayerPrefs.GetInt(Constants.ELECTRICEELSEEN, 0);
        if (electricEelSeen > 0)
        {
            electricEelImage.GetComponent<Image>().sprite = revealElectricEel;
        }

        int jellyFishSeen = PlayerPrefs.GetInt(Constants.JELLYFISHSEEN, 0);
        if (jellyFishSeen > 0)
        {
            jellyfishImage.GetComponent<Image>().sprite = revealJellyfish;
        }

        int octupusSeen = PlayerPrefs.GetInt(Constants.OCTUPUSSEEN, 0);
        if (octupusSeen > 0)
        {
            octupusImage.GetComponent<Image>().sprite = revealOctupus;
        }

        int dragonSeen = PlayerPrefs.GetInt(Constants.DRAGONSEEN, 0);
        if (dragonSeen > 0)
        {
            dragonImage.GetComponent<Image>().sprite = revealDragon;
        }

    }

    public void OnClickResume()
    {
        Pause();
    }

    public void OnClickStatus()
    {
        currentState = MenuStates.Status;
        //storeSelected = hullLevelButton;
        //ES.SetSelectedGameObject(hullLevelButton);
    }

    public void OnClickEncyclopedia()
    {
        currentState = MenuStates.Encyclopedia;
    }

    public void OnClickOption()
    {
        currentState = MenuStates.Options;
    }

    public void OnClickRestart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnClickExit()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Main Menu");
    }
    
    public void OnClickKeyboard1Radio()
    {
        PlayerPrefs.SetInt("Player1", 1);
        player1Control = 1;
    }

    public void OnClickController1Radio()
    {
        PlayerPrefs.SetInt("Player1", 3);
        player1Control = 3;
    }

    public void OnClickKeyboard2Radio()
    {
        PlayerPrefs.SetInt("Player2", 2);
        player2Control = 2;
    }

    public void OnClickController2Radio()
    {
        PlayerPrefs.SetInt("Player2", 4);
        player2Control = 4;
    }

    public void OnClickAudio()
    {
        masterVolumePercent = PlayerPrefs.GetFloat("master vol", masterVolumePercent);
        sfxVolumePercent = PlayerPrefs.GetFloat("sfx vol", sfxVolumePercent);
        musicVolumePercent = PlayerPrefs.GetFloat("music vol", musicVolumePercent);

        masterSlider.GetComponent<Slider>().value = masterVolumePercent;
        musicSlider.GetComponent<Slider>().value = musicVolumePercent;
        fxSlider.GetComponent<Slider>().value = sfxVolumePercent;
        currentState = MenuStates.Audios;
        ES.SetSelectedGameObject(firstSelectedInAudio);

    }

    public void OnClickControls()
    {
        currentState = MenuStates.Controls;
        ES.SetSelectedGameObject(firstSelectedInControl);
    }

    public void OnClickBackToOptions()
    {
        currentState = MenuStates.Options;
        ES.SetSelectedGameObject(firstSelectedInOption);
    }

    public void SetMasterVolume(float value)
    {
        AudioManager.instance.SetVolume(value, AudioManager.AudioChannel.Master);
    }

    public void SetMusicVolume(float value)
    {
        AudioManager.instance.SetVolume(value, AudioManager.AudioChannel.Music);
    }

    public void SetSfxVolume(float value)
    {
        AudioManager.instance.SetVolume(value, AudioManager.AudioChannel.Sfx);
    }
}
