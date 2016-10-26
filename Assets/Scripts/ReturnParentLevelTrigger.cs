using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ReturnParentLevelTrigger : MonoBehaviour {

	// Use this for initialization
	public GameObject playerObj;
	void Start () {
        PlayerPrefs.SetInt(Constants.DRAGONSEEN, 1);
    }
	
	// Update is called once per frame
	void Update () {

        if (GameObject.Find ("Dragon") == null) {
			GameObject.Find ("Ship").transform.Find ("Camera").GetComponent<Camera>().orthographicSize = 5;
			ShipActions ship = playerObj.GetComponent<ShipActions> ();
			//Timer timeClass = playerObj.GetComponent<Timer> ();
			
            //PlayerPrefs.SetInt(Constants.DYNAMITECURRCOOLDOWN, 0);
            //layerPrefs.SetInt(Constants.PRESTIGEEARN, ship.GetCurrentPrestige());
            //PlayerPrefs.SetFloat (Constants.TIMELEFT, timeClass.GetTimeLeft ());
            //PlayerPrefs.SetInt (Constants.CURRCREWSAVED, ship.crewsaved);
            //Debug.Log ("ReturnParentLevelTrigger is "+PlayerPrefs.GetInt (Constants.CURRCREWSAVED,0));

            DynamiteActions da = playerObj.GetComponentInChildren<DynamiteActions>();
            float timePast = da.GetTimePast();
            PlayerPrefs.SetInt(Constants.HULLVALUE_TEMP, ship.hullcurrent);
            PlayerPrefs.SetFloat(Constants.DYNAMITECOOLDOWN_TEMP, timePast);
            Timer time = playerObj.GetComponent<Timer>();
            PlayerPrefs.SetFloat(Constants.TIMELEFT_TEMP, time.GetTimeLeft());

            SceneManager.LoadScene("Level 1");
		}
	}
}
