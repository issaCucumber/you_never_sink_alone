using UnityEngine;
using System.Collections;

public class DragonSpawner : MonoBehaviour {
	public GameObject enermyPrefab;
	GameObject enermyInstance;
	private static int dragonNumber = 0;

	void OnTriggerEnter2D(Collider2D obj){
		if (obj.name == "Ship") {//need to unify name
			
			if (enermyInstance==null) {
				enermyInstance = (GameObject)Instantiate (enermyPrefab, transform.position, Quaternion.identity);
				Debug.Log ("Enermy " + enermyInstance.name + " borned!");
				dragonNumber+=1;
			}	
		}
	}
}
