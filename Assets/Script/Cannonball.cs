using UnityEngine;
using System.Collections;

public class Cannonball : MonoBehaviour {

    public float speedCannon;

	// Use this for initialization
	void Start () {
        GetComponent<Rigidbody2D>().velocity = transform.right* speedCannon;

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
