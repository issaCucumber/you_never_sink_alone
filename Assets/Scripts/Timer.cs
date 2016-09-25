using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Timer : MonoBehaviour {

    public Text timerText;
    public int timerInMins;

    private float startTime;

	// Use this for initialization
	void Start () {
        startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
        float t = (timerInMins*60) - Time.time - startTime;

        string minutes = ((int)t / 60).ToString();

        float seconds = t % 60;
        string secondsString = "";
        if (seconds < 10)
        {
            secondsString += "0";
        }

        secondsString += seconds.ToString("f0");


        timerText.text = minutes + ":" + secondsString;
	}
}
