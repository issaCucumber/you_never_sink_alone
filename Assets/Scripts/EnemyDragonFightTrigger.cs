using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class EnemyDragonFightTrigger : MonoBehaviour {

	Camera camera;
	public GameObject playerObj;
	bool enabled = true;

	void Start () {
		//Debug.Log ("EnemyDragonFightTrigger Start");
	}

	void OnTriggerEnter2D(Collider2D other) {
		ShipActions ship = playerObj.GetComponent<ShipActions> ();
		//Debug.Log ("EnemyDragonFightTrigger bf PlayerPrefs.GetInt(Constants.DEFEATDRAGON,0)="+PlayerPrefs.GetInt(Constants.DEFEATDRAGON,0));
		if (other.name.StartsWith("Ship") && PlayerPrefs.GetInt (Constants.DEFEATDRAGON, 0) == 0) {
			camera = GameObject.Find ("Ship").transform.Find ("Camera").GetComponent<Camera>();
			camera.orthographicSize = 8;
			Timer timeClass = playerObj.GetComponent<Timer> ();
			PlayerPrefs.SetInt(Constants.HULLCURRVALUE, ship.hullcurrent);
			PlayerPrefs.SetInt(Constants.DYNAMITECURRCOOLDOWN, 0);
			PlayerPrefs.SetInt(Constants.PRESTIGEEARN, ship.GetCurrentPrestige());
			PlayerPrefs.SetFloat (Constants.TIMELEFT, timeClass.GetTimeLeft ());
			PlayerPrefs.SetInt (Constants.CURRCREWSAVED, ship.crewsaved);
			PlayerPrefs.SetInt (Constants.DEFEATDRAGON, 1);
			//Debug.Log ("EnemyDragonFightTrigger af PlayerPrefs.GetInt(Constants.DEFEATDRAGON,0)="+PlayerPrefs.GetInt(Constants.DEFEATDRAGON,0));
			enabled = false;
			SceneManager.LoadScene("DragonFight");
		}
	}
}
