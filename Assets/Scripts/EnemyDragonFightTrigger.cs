using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class EnemyDragonFightTrigger : MonoBehaviour {
	
	void OnTriggerEnter2D(Collider2D other) {
		if (other.name.StartsWith("Ship")) {
			Application.LoadLevel ("DragonFight");
		}
	}
}
