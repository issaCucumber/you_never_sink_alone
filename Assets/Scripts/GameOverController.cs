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
				reasonText.text = "Time has run out";
				Time.timeScale = 0;
				timer.enabled = false;
			}

			if (shipAction.hullcurrent <= 0) {
				gameOverScreen.SetActive (true);
				reasonText.text = "Ship is destroyed";
				Time.timeScale = 0;
				timer.enabled = false;
			}
		} else {
			if (shipAction.touchWhirlpool) {
				reasonText.text = "xx xx xx whirlpool";
			}
		}
	}

    public void OnClickRetry()
    {
        Time.timeScale = 1;
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		SceneManager.LoadScene(restartScene);
    }

    public void OnClickExit()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Main Menu");
    }
}
