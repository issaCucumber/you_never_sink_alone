using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelCompleteController : MonoBehaviour {

    public GameObject levelCompleteScreen;
    public Transform playerObj;
 
    public Text timeLeftText;
    public Text prestigeText;
    public int speed;


    private Timer timer;
    private ShipActions ship;
    private float startTime;
    private float timeLeft;

    private int finalPrestige;
    private int originalPrestige;

	// Use this for initialization
	void Start () {
        timer = playerObj.GetComponent<Timer>();
        ship = playerObj.GetComponent<ShipActions>();
	}
	
	// Update is called once per frame
	void Update () {

        if (levelCompleteScreen.activeInHierarchy == true)
        {
            if(timeLeft > 0)
            {
                timeLeft -= speed;
            }
            else
            {
                timeLeft = 0;
            }
            Debug.Log("time left " + timeLeft);

            if(originalPrestige < finalPrestige)
            {
                originalPrestige += speed;
            }
            else
            {
                originalPrestige = finalPrestige;
            }

            string minutes = ((int)timeLeft / 60).ToString();

            float seconds = timeLeft % 60;
            string secondsString = "";
            if (seconds < 10)
            {
                secondsString += "0";
            }

            secondsString += seconds.ToString("f0");
            timeLeftText.text = minutes + ":" + secondsString;

            prestigeText.text = originalPrestige.ToString();
        }
    }

    void OnTriggerEnter2D(Collider2D obj)
    {
        if (obj.name.Equals("Ship"))
        {
            if (levelCompleteScreen.activeInHierarchy == false)
            {
                playerObj.GetComponent<Timer>().enabled = false;
                timeLeftText.text = timer.GetTimeString();
                originalPrestige = ship.GetCurrentPrestige();
                prestigeText.text = System.Convert.ToString(ship.GetCurrentPrestige());
                int bonusPrestige = System.Convert.ToInt32(timer.GetTimeLeft()) * 3;
                finalPrestige = originalPrestige + bonusPrestige;
                ship.SetCurrentPrestige(finalPrestige);
                timeLeft = timer.GetTimeLeft();
                startTime = Time.time;

                levelCompleteScreen.SetActive(true);
                Time.timeScale = 0;
                Debug.Log("calling level complete");
            }
        }
    }
    
    void OnClickContinue()
    {
        Time.timeScale = 1;
        //save the progress of current level
        //to-do load level complete screen
    }
}
