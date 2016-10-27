using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GameOverController : MonoBehaviour {

    public EventSystem ES;
    private GameObject storeSelected;
    public GameObject retryBtn;

    public GameObject gameOverScreen;
    public Transform playerObj;
    public Text reasonText;
	public string restartScene;

    private Timer timer;
    private ShipActions shipAction;

    // Use this for initialization
    void Start () {
        timer = playerObj.GetComponent<Timer>();
        shipAction = playerObj.GetComponent<ShipActions>();
        storeSelected = retryBtn;
        ES.SetSelectedGameObject(retryBtn);
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

		if (gameOverScreen.activeInHierarchy == false) {
			if (timer.GetTimeLeft () <= 0) {
				gameOverScreen.SetActive (true);
                ES.SetSelectedGameObject(retryBtn);
                reasonText.text = "Time has run out";
				Time.timeScale = 0;
				timer.enabled = false;
			}

			if (shipAction.hullcurrent <= 0) {
				gameOverScreen.SetActive (true);
                ES.SetSelectedGameObject(retryBtn);
                reasonText.text = "Ship is destroyed";
				Time.timeScale = 0;
				timer.enabled = false;
			}
		} else {
			if (shipAction.touchWhirlpool) {
                ES.SetSelectedGameObject(retryBtn);
                reasonText.text = "Destroyed by Whirlpool ";
                shipAction.touchWhirlpool = false;

            }
		}
	}

    public void OnClickRetry()
    {
        Time.timeScale = 1;
        PlayerPrefs.SetInt(Constants.DEFEATDRAGON, 0);
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        SceneManager.LoadScene(restartScene);
    }

    public void OnClickExit()
    {
        Time.timeScale = 1;
        PlayerPrefs.SetInt(Constants.DEFEATDRAGON, 0);
        SceneManager.LoadScene("Main Menu");
    }
}
