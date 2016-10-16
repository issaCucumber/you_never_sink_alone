using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class GUIManager : MonoBehaviour {

    public EventSystem ES;
    private GameObject storeSelected;

    public GameObject firstSelectedInMenu;
    public GameObject firstSelectedInOptions;
    public GameObject firstSelectedInAudios;
    public GameObject firstSelectedInControls;
    public GameObject firstSelectedInCredits;

    public GameObject continueGameBtn;

    public enum MenuStates { Main , Options, Credits, Audios, Controls};
    public MenuStates currentState;

    //menu objects
    public GameObject mainMenu;
    public GameObject optionsMenu;
    public GameObject creditsMenu;
    
    public GameObject audioOptionMenu;

    public GameObject controlOptionMenu;

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

    private int player1Control;
    private int player2Control;

    float masterVolumePercent = 1f;
    float sfxVolumePercent = 1f;
    float musicVolumePercent = 1f;

    public GameObject masterSlider;
    public GameObject musicSlider;
    public GameObject fxSlider;

    // Use this for initialization
    void Start ()
    {
        currentState = MenuStates.Main;
        storeSelected = ES.firstSelectedGameObject;

        //continueGameBtn.enabled = false;
        if (PlayerPrefs.GetInt(Constants.LEVELCLEARED, 0) == 0)
        {
            Debug.Log("Disable continue btn " + PlayerPrefs.GetInt(Constants.LEVELCLEARED));
            continueGameBtn.GetComponent<Button>().interactable = false;
        }
        else
        {
            Debug.Log("Enable continue btn");
            continueGameBtn.GetComponent<Button>().interactable = true; 
        }

        player1Control = PlayerPrefs.GetInt("Player1", 1);  //1 is keyboard1, 2 is keyboard2, 3 is controller1, 4 is controller2
        player2Control = PlayerPrefs.GetInt("Player2", 2);

        SetPlayer1Radio(player1Control);
        SetPlayer2Radio(player2Control);
        //SpriteState spriteState = new SpriteState();
        //spriteState = continueGameBtn.GetComponent<Button>().spriteState;
        //spriteState.disabledSprite = testImage;
        //continueGameBtn.GetComponent<Button>().spriteState = spriteState;

        masterVolumePercent = PlayerPrefs.GetFloat("master vol", masterVolumePercent);
        sfxVolumePercent = PlayerPrefs.GetFloat("sfx vol", sfxVolumePercent);
        musicVolumePercent = PlayerPrefs.GetFloat("music vol", musicVolumePercent);

        masterSlider.GetComponent<Slider>().value = masterVolumePercent;
        musicSlider.GetComponent<Slider>().value = musicVolumePercent;
        fxSlider.GetComponent<Slider>().value = sfxVolumePercent;
    }

    private void SetPlayer1Radio(int player1Control)
    {
        if(player1Control == 1)
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

    // Update is called once per frame
    void Update ()
    {
       // Debug.Log("Store Selected " + storeSelected);
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


        switch(currentState)
        {
            case MenuStates.Main :                                    
                                    mainMenu.SetActive(true);
                                    optionsMenu.SetActive(false);
                                    creditsMenu.SetActive(false);
                break;
            case MenuStates.Options:
                                    mainMenu.SetActive(false);
                                    optionsMenu.SetActive(true);
                                    creditsMenu.SetActive(false);
                                    audioOptionMenu.SetActive(false);
                                    controlOptionMenu.SetActive(false);
                break;
            case MenuStates.Audios:
                                    optionsMenu.SetActive(false);
                                    audioOptionMenu.SetActive(true);
                                    controlOptionMenu.SetActive(false);
                break;
            case MenuStates.Controls:
                                    controlOptionMenu.SetActive(true);
                                    optionsMenu.SetActive(false);
                                    audioOptionMenu.SetActive(false);
                                    SetPlayer1Radio(player1Control);
                                    SetPlayer2Radio(player2Control);
                break;
            case MenuStates.Credits:
                                    mainMenu.SetActive(false);
                                    optionsMenu.SetActive(false);
                                    creditsMenu.SetActive(true);
                break;    
        }

	}

    public void OnClickPlay()
    {
        Debug.Log("Play button is pressed");
        ResetPlayerPrefData();
        SceneManager.LoadScene("CutScene");
    }

    private void ResetPlayerPrefData()
    {
        PlayerPrefs.SetInt(Constants.HULL, 1);
        PlayerPrefs.SetInt(Constants.TOOLBOX, 1);
        PlayerPrefs.SetInt(Constants.STARBOARDPOWER, 1);
        PlayerPrefs.SetInt(Constants.STARBOARDFIRERATE, 1);
        PlayerPrefs.SetInt(Constants.WHEEL, 1);
        PlayerPrefs.SetInt(Constants.DYNAMITE, 1);
        PlayerPrefs.SetInt(Constants.PORTPOWER, 1);
        PlayerPrefs.SetInt(Constants.PORTFIRERATE, 1);
        PlayerPrefs.SetInt(Constants.FLYINGFISHSEEN, 0);
        PlayerPrefs.SetInt(Constants.ELECTRICEELSEEN, 0);
        PlayerPrefs.SetInt(Constants.JELLYFISHSEEN, 0);
        PlayerPrefs.SetInt(Constants.OCTUPUSSEEN, 0);
        PlayerPrefs.SetInt(Constants.DRAGONSEEN, 0);
        PlayerPrefs.SetInt(Constants.LEVELCLEARED, 0);
        PlayerPrefs.SetInt(Constants.PRESTIGEEARN, 0);
    }

    public void OnClickContinue()
    {
        Debug.Log("Cont button is pressed");
		SceneManager.LoadScene ("Level Select");
    }

    public void OnClickOptions()
    {
        currentState = MenuStates.Options;
        //ES.SetSelectedGameObject(firstSelectedInOptions);
        storeSelected = firstSelectedInOptions;
        ES.SetSelectedGameObject(firstSelectedInOptions);

    }

    public void OnClickControls()
    {
        currentState = MenuStates.Controls;
        storeSelected = firstSelectedInControls;
        ES.SetSelectedGameObject(firstSelectedInControls);
    }

    public void OnClickCredits()
    {
        currentState = MenuStates.Credits;
        //ES.SetSelectedGameObject(firstSelectedInCredits);  
        storeSelected = firstSelectedInCredits;
        ES.SetSelectedGameObject(firstSelectedInCredits);

    }

    public void OnClickExit()
    {
        //exit game
        Debug.Log("Exit button is pressed");
        Application.Quit();
    }

    public void OnClickBack()
    {
        //back to main menu
        currentState = MenuStates.Main;
        //ES.SetSelectedGameObject(firstSelectedInMenu);
        storeSelected = firstSelectedInMenu;
        ES.SetSelectedGameObject(firstSelectedInMenu);

    }

    public void OnClickBackToOptions()
    {
        currentState = MenuStates.Options;
        storeSelected = firstSelectedInOptions;
        ES.SetSelectedGameObject(firstSelectedInOptions);

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

        storeSelected = firstSelectedInAudios;
        ES.SetSelectedGameObject(firstSelectedInAudios);

    }

    public void PlayOnEnter()
    {
        //buttonEnterSound
        //audioSource.clip = buttonEnterSound;
        //audioSource.PlayOneShot(buttonEnterSound);
        AudioManager.instance.PlaySound2D("shift");
    }

    public void PlayOnClick()
    {
        //audioSource.PlayOneShot(buttonClickSound);
        Debug.Log("Click button is pressed");
        AudioManager.instance.PlaySound2D("select");
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
