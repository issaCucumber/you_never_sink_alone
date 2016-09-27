using UnityEngine;
using System.Collections;

public class EnermySpawn : MonoBehaviour {
	public GameObject enermyPrefab;
	GameObject enermyInstance;
	bool startSpawn = false;

	int i;
	public int maxNumOfEnermy = 6;
	float timer = 0;
	float spawnTimeBetween = 3f;
	float timerRecorder;
	float leaveDelayTime = 15f;

	// Use this for initialization
	void Update () {
		if (startSpawn) {
			timer += Time.deltaTime;
			if (timer >= spawnTimeBetween && i <= maxNumOfEnermy) {
				SpawnEnermy ();
				timerRecorder = timer;
			}
		}
	}

	void OnTriggerEnter2D(){
		Debug.Log ("enter trigger");
		startSpawn = true;

	}

	void OnTriggerExit2D(){
		Debug.Log ("leave trigger");
		startSpawn = false;
		//generate enermy at least once
		SpawnEnermy ();
	}

	void SpawnEnermy(){
		enermyInstance = (GameObject)Instantiate (enermyPrefab, transform.position, Quaternion.identity);
		Debug.Log ("enermy borned");
		timer = 0;
		i+=1;
	}

}
