using UnityEngine;
using System.Collections;

public class EnermySpawn : MonoBehaviour {
	public GameObject enermyPrefab;
	GameObject enermyInstance;
	bool startSpawn = false;
	int offset = 15;

	int i;
	//public int maxNumOfEnermy = 6;
	float timer = 3f;
	float spawnTimeBetween = 3f;
	//float timerRecorder;
	//float leaveDelayTime = 15f;

	// Use this for initialization
	void Start () {
	}
	void Update () {
		if (startSpawn) {
			timer += Time.deltaTime;
			if (timer >= spawnTimeBetween /*&& i < maxNumOfEnermy*/) {
				SpawnEnermy (true,true);
				//timerRecorder = timer;
			}
		}
	}

	void OnTriggerEnter2D(Collider2D obj){
		if (!startSpawn && obj.name.Equals("Ship") ) {//need to unify name
			SpawnEnermy (true,false);
		} else {
		}

	}

	void OnTriggerExit2D(Collider2D obj){
		if (startSpawn && obj.name.Equals("Ship") && obj.transform.parent == null) {//need to unify name
			//startSpawn = false;
			//generate enermy at least once
			SpawnEnermy (false, true);
		} else {
		}
	}

	void SpawnEnermy(bool start,bool execute){
		startSpawn = start;
		if (execute) {
			Vector3 radomoffset = new Vector3 (Random.Range(-offset,offset),Random.Range(-offset,offset),0);
			enermyInstance = (GameObject)Instantiate (enermyPrefab, transform.position + radomoffset, Quaternion.identity);
			timer = 0;
			i+=1;
		}
	}

}
