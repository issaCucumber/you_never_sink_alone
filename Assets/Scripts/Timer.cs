using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour {

    public Text timerText;
    public int timerInMins;

    private float startTime;
    private float timeLeft;
    private string timeString; 

	// Use this for initialization
	void Start () {

        timeLeft = timerInMins * 60;

        if (PlayerPrefs.GetInt(Constants.DEFEATDRAGON, 0) == 1)
        {
            timeLeft = PlayerPrefs.GetFloat(Constants.TIMELEFT_TEMP, -1);
        }

        startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {

        float t = (timerInMins * 60);
        if (PlayerPrefs.GetInt(Constants.DEFEATDRAGON, 0) == 1)
        {
            t = PlayerPrefs.GetFloat(Constants.TIMELEFT_TEMP, -1);
        }

        t = t - (Time.time - startTime);
        timeLeft = t;
        //if (SceneManager.GetActiveScene().name.Equals("Level 1"))
        //{
        //    t = t - (Time.time - startTime);
        //    timeLeft = t;
        //}

        string minutes = ((int)t / 60).ToString();

        float seconds = t % 60;
        string secondsString = "";
        if (seconds < 10)
        {
            secondsString += "0";
        }

        secondsString += seconds.ToString("f0");

        timeString = minutes + ":" + secondsString;
        timerText.text = timeString;
    }

    public float GetTimeLeft()
    {
        return timeLeft;
    }

    public string GetTimeString()
    {
        return timeString;
    }
}
