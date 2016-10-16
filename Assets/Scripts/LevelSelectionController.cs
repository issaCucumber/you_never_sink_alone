using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelectionController : MonoBehaviour {

    public EventSystem ES;
    private GameObject storeSelected;

    public Button levelTwoButton;
    public Button levelOneButton;

    // Use this for initialization
    void Start () {
        storeSelected = ES.firstSelectedGameObject;

        int maxLevelCleared = PlayerPrefs.GetInt(Constants.LEVELCLEARED, 0);
        if(maxLevelCleared > 0)
        {
            levelOneButton.interactable = true;
        }
        else
        {
            levelOneButton.interactable = false;
        }
        levelTwoButton.interactable = false;    //since there is no level 2

    }
	
	// Update is called once per frame
	void Update () {

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
    }

    public void OnClickTutorialLevel()
    {
        SceneManager.LoadScene("CutScene"); //back to beginning
    }

    public void OnClickLevelOne()
    {
        SceneManager.LoadScene("Level 1");
    }

    public void OnClickBack()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
