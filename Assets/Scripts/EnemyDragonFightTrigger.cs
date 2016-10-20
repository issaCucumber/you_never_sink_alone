using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class EnemyDragonFightTrigger : MonoBehaviour {

	Camera camera;

	void OnTriggerEnter2D(Collider2D other) {
		if (other.name.StartsWith("Ship")) {
			camera = GameObject.Find ("Ship").transform.Find ("Camera").GetComponent<Camera>();
			camera.orthographicSize = 8;
			SceneManager.LoadScene("DragonFight");
		}
	}
}
