using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class EnemyDragonFightTrigger : MonoBehaviour {

	Camera camera;
	public GameObject playerObj;

	void OnTriggerEnter2D(Collider2D other) {
		if (other.name.StartsWith("Ship")) {
			camera = GameObject.Find ("Ship").transform.Find ("Camera").GetComponent<Camera>();
			camera.orthographicSize = 8;
			ShipActions ship = playerObj.GetComponent<ShipActions> ();
			Timer timeClass = playerObj.GetComponent<Timer> ();
			PlayerPrefs.SetInt(Constants.HULLCURRVALUE, ship.hullcurrent);
			PlayerPrefs.SetInt(Constants.DYNAMITECURRCOOLDOWN, 0);
			PlayerPrefs.SetInt(Constants.PRESTIGEEARN, ship.GetCurrentPrestige());
			PlayerPrefs.SetFloat (Constants.TIMELEFT, timeClass.GetTimeLeft ());
			PlayerPrefs.SetInt (Constants.CURRCREWSAVED, ship.crewsaved);

			SceneManager.LoadScene("DragonFight");
		}
	}
}
