using UnityEngine;
using System.Collections;

public class EnemyDisableArrows : MonoBehaviour {

	// Use this for initialization
	void Start () {
		if (PlayerPrefs.GetInt(Constants.DEFEATDRAGON,0)==1) {
			gameObject.SetActive (false);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
