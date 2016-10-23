using UnityEngine;
using System.Collections;

public class EnemyRotate : MonoBehaviour {

	public float rotateSpeed = 100;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate (new Vector3(0,0,1)* rotateSpeed * Time.deltaTime);
	}
}
