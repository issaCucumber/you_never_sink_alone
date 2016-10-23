using UnityEngine;
using System.Collections;

public class EnermySpawn : MonoBehaviour {
	public GameObject enermyPrefab;
	GameObject enermyInstance;
	bool startSpawn = false;

	int i;
	public int maxNumOfEnermy = 6;
	float timer = 5f;
	float spawnTimeBetween = 3f;
	float timerRecorder;
	float radius;
	//float leaveDelayTime = 15f;

	// Use this for initialization
	void Start () {
	}
	void Update () {
		if (startSpawn) {
			timer += Time.deltaTime;
			if (timer >= spawnTimeBetween /*&& i < maxNumOfEnermy*/) {
				SpawnEnermy ();
				timerRecorder = timer;
			}
		}
	}

	void OnTriggerEnter2D(Collider2D obj){
		if (!startSpawn && obj.name.Equals("Ship") ) {//need to unify name
			startSpawn = true;
		} else {
		}

	}

	void OnTriggerExit2D(Collider2D obj){
		if (startSpawn && obj.name.Equals("Ship") && obj.transform.parent == null) {//need to unify name
			startSpawn = false;
			//generate enermy at least once
			SpawnEnermy ();
		} else {
		}
	}

	void SpawnEnermy(){
		enermyInstance = (GameObject)Instantiate (enermyPrefab, transform.position, Quaternion.identity);
		timer = 0;
		i+=1;
	}

}
