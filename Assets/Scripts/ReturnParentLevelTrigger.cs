using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ReturnParentLevelTrigger : MonoBehaviour {

	// Use this for initialization
	public GameObject playerObj;
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (GameObject.Find ("Dragon") == null) {
			GameObject.Find ("Ship").transform.Find ("Camera").GetComponent<Camera>().orthographicSize = 5;
			ShipActions ship = playerObj.GetComponent<ShipActions> ();
			Timer timeClass = playerObj.GetComponent<Timer> ();
			PlayerPrefs.SetInt(Constants.HULLCURRVALUE, ship.hullcurrent);
			PlayerPrefs.SetInt(Constants.DYNAMITECURRCOOLDOWN, 0);
			PlayerPrefs.SetInt(Constants.PRESTIGEEARN, ship.GetCurrentPrestige());
			PlayerPrefs.SetFloat (Constants.TIMELEFT, timeClass.GetTimeLeft ());
			PlayerPrefs.SetInt (Constants.CURRCREWSAVED, ship.crewsaved);
			//Debug.Log ("ReturnParentLevelTrigger is "+PlayerPrefs.GetInt (Constants.CURRCREWSAVED,0));

			SceneManager.LoadScene("Level 1");
		}
	}
}
