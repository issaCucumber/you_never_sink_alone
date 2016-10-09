using UnityEngine;
using System.Collections;

public class EnemySelfDestroy : MonoBehaviour {

	// Use this for initialization
	public float delay = 1f;
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		delay -= Time.deltaTime;
		if (delay <= 0) {
			Destroy (gameObject);
		}
	}
}
