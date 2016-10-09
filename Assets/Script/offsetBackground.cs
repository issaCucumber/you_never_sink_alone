using UnityEngine;
using System.Collections;

public class offsetBackground : MonoBehaviour {

	public GameObject Ship;

	private float scrollSpeed;
	private Vector3 shipPosition;

	// Use this for initialization
	void Start () {
		shipPosition = Ship.transform.position;
	}
	
	// Update is called once per frame
	void Update () {

//		if (Ship.transform.position == shipPosition) {
//			scrollSpeed = 0.01f;
//		} else {
//			scrollSpeed = 0.3f;
//		}
//
//		float y = Mathf.Repeat (Time.time * scrollSpeed, 2);
//		Vector2 offset = new Vector2 (0, y);
//		GetComponent<Renderer> ().sharedMaterial.SetTextureOffset ("_MainTex", offset);

		shipPosition = Ship.transform.position;
	}
}
