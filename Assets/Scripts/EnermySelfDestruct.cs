using UnityEngine;
using System.Collections;

public class EnermySelfDestruct : MonoBehaviour {//some problems to be discussed.
//	bool inVisible;
	public float selfDestructTime = 10f;
	float timeRecorder;
	Vector2 cameraPos;
	Vector2 enemyPos;
	void Start(){
		timeRecorder = selfDestructTime;
	}

	void FixedUpdate () {
		cameraPos = Camera.main.gameObject.transform.position;
		enemyPos = gameObject.transform.position;
		if (distBwtCameraNEnemy (cameraPos, enemyPos)) {
//			Debug.Log ("Time left:" + selfDestructTime);
			selfDestructTime -= Time.deltaTime;
			if (selfDestructTime <= 0) {
				Die ();
				Debug.Log ("finished");
			}
		}
	}

	bool distBwtCameraNEnemy(Vector2 cameraPos, Vector2 enemyPos){
		float dist;
		dist = Mathf.Sqrt ((cameraPos.x-enemyPos.x)*(cameraPos.x-enemyPos.x)+(cameraPos.y-enemyPos.y)*(cameraPos.y-enemyPos.y));
//		Debug.Log ("Distance bwt enemy and ship: "+ dist);
		if (dist >= 15) {
			return true;
		} else {
			selfDestructTime = timeRecorder;
			return false;
		}
	}
	void Die(){
		Destroy (gameObject);
	}
}
