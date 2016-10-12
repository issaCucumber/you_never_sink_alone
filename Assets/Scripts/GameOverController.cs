using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverController : MonoBehaviour {

    public GameObject gameOverScreen;
    public Transform playerObj;
    public Text reasonText;

    private Timer timer;
    private ShipActions shipAction;

    // Use this for initialization
    void Start () {
        timer = playerObj.GetComponent<Timer>();
        shipAction = playerObj.GetComponent<ShipActions>();
	}
	
	// Update is called once per frame
	void Update () {
	    if(gameOverScreen.activeInHierarchy == false)
        {
            if(timer.GetTimeLeft() <= 0)
            {
                gameOverScreen.SetActive(true);
                reasonText.text = "Time has run out";
                Time.timeScale = 0;
                timer.enabled = false;
            }

            if( shipAction.hullcurrent <= 0)
            {
                gameOverScreen.SetActive(true);
                reasonText.text = "Ship is destroyed";
                Time.timeScale = 0;
                timer.enabled = false;
            }
        }
	}

    public void OnClickRetry()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnClickExit()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Main Menu");
    }
}
