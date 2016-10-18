using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ActivateShipWreckCutScene : MonoBehaviour {

	public GameObject playerObj;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D other){

		int finalPrestige = playerObj.GetComponent<ShipActions> ().GetCurrentPrestige ();
		PlayerPrefs.SetInt(Constants.PRESTIGEEARN, finalPrestige);

		SceneManager.LoadScene ("CutScene2");
	}
}
