using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;
using UnityEditor.SceneManagement;

public class GUIManager : MonoBehaviour {

    public EventSystem ES;
    private GameObject storeSelected;

    public GameObject firstSelectedInMenu;
    public GameObject firstSelectedInOptions;
    public GameObject firstSelectedInCredits;

    public AudioSource audioSource;
    public AudioClip buttonEnterSound;
    public AudioClip buttonClickSound;

    public Button continueGameBtn;

    public enum MenuStates { Main , Options, Credits };
    public MenuStates currentState;

    //menu objects
    public GameObject mainMenu;
    public GameObject optionsMenu;
    public GameObject creditsMenu;

    public Slider[] volumeSliders;


	// Use this for initialization
	void Start ()
    {
        currentState = MenuStates.Main;
        storeSelected = ES.firstSelectedGameObject;

        continueGameBtn.enabled = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
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
        SceneManager.LoadScene("Main Game");
        //EditorSceneManager.LoadScene("Main Game");
    }

    public void OnClickContinue()
    {
        Debug.Log("Cont button is pressed");
    }

    public void OnClickOptions()
    {
        currentState = MenuStates.Options;
        ES.SetSelectedGameObject(firstSelectedInOptions);
    }

    public void OnClickCredits()
    {
        currentState = MenuStates.Credits;
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
        ES.SetSelectedGameObject(firstSelectedInMenu);
    }

    public void PlayOnEnter()
    {
        //buttonEnterSound
        //audioSource.clip = buttonEnterSound;
        audioSource.PlayOneShot(buttonEnterSound);
    }

    public void PlayOnClick()
    {
        audioSource.PlayOneShot(buttonClickSound);
    }

    public void SetMusicVolume(float value)
    {
    }

    public void SetSfxVolume(float value)
    {

    }
}
