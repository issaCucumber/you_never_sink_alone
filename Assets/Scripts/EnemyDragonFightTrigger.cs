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

			PlayerPrefs.SetInt (Constants.DEFEATDRAGON, 1);

            //upgrades done
            PlayerPrefs.SetInt(Constants.HULL_TEMP, ship.hulllevel);
            PlayerPrefs.SetInt(Constants.TOOLBOX_TEMP, ship.toolboxlevel);
            PlayerPrefs.SetInt(Constants.STARBOARDPOWER_TEMP, ship.starboardcannonpowerlevel);
            PlayerPrefs.SetInt(Constants.STARBOARDFIRERATE_TEMP, ship.starboardcannonfireratelevel);
            PlayerPrefs.SetInt(Constants.WHEEL_TEMP, ship.wheellevel);
            PlayerPrefs.SetInt(Constants.DYNAMITE_TEMP, ship.dynamitelevel);
            PlayerPrefs.SetInt(Constants.PORTPOWER_TEMP, ship.portcannonpowerlevel);
            PlayerPrefs.SetInt(Constants.PORTFIRERATE_TEMP, ship.portcannonfireratelevel);

            DynamiteActions da = playerObj.GetComponentInChildren<DynamiteActions>();
            float timePast = da.GetTimePast();

            PlayerPrefs.SetInt(Constants.HULLVALUE_TEMP, ship.hullcurrent);
            PlayerPrefs.SetFloat(Constants.DYNAMITECOOLDOWN_TEMP, timePast);
            PlayerPrefs.SetInt(Constants.PRESTIGEEARN_TEMP, ship.GetCurrentPrestige());
            PlayerPrefs.SetFloat(Constants.TIMELEFT_TEMP, timeClass.GetTimeLeft());
            PlayerPrefs.SetInt(Constants.CREWSAVED_TEMP, ship.crewsaved);

            //Debug.Log ("EnemyDragonFightTrigger af PlayerPrefs.GetInt(Constants.DEFEATDRAGON,0)="+PlayerPrefs.GetInt(Constants.DEFEATDRAGON,0));
            enabled = false;
			SceneManager.LoadScene("DragonFight");
		}
	}
}
