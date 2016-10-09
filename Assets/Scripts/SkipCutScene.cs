using UnityEngine;
using System.Collections;
using TeamUtility.IO;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class SkipCutScene : MonoBehaviour {

    public EventSystem ES;
    private GameObject storeSelected;
    public GameObject firstSelected;

    public GameObject pauseScreen;
    public string nextScene;

    // Use this for initialization
    void Start () {
    }
	
	// Update is called once per frame
	void Update () {
        if (InputManager.GetButtonDown("Pause"))
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
    }

    private void Pause()
    {
        if (pauseScreen.activeInHierarchy == false)
        {
            pauseScreen.SetActive(true);
            Time.timeScale = 0;
            ES.SetSelectedGameObject(firstSelected);
        }
        else
        {
            pauseScreen.SetActive(false);
            Time.timeScale = 1;
        }
    }

    public void OnClickYes()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(nextScene);
    }

    public void OnClickNo()
    {
        Pause();
    }


}
