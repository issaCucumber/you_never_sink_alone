using UnityEngine;
using System.Collections;

public class EnemyDragonGateHandler : MonoBehaviour {
	
	public GameObject explode;
	int health = 20;

	ShipActions sa;	

	void Start () {
		sa = GameObject.Find ("Ship").transform.GetComponent<ShipActions>();

		//PlayerPrefs.SetInt (Constants.DEFEATDRAGON, 0);//TODO 

	}

	void OnTriggerEnter2D(Collider2D other) {
		if (PlayerPrefs.GetInt (Constants.DEFEATDRAGON, 0) == 1) {
			gameObject.SetActive (false);
		}
		if (other.name.StartsWith("SuperAttack")) {
			Debug.Log("hit SuperAttack");
			health -= 20;
		}

		if (health <= 0 ) {
			
			Vector3 pos = transform.position;
			pos = new Vector3(pos.x + 1.8f, pos.y + 7.4f, pos.z);

			GameObject go = (GameObject)Instantiate (explode, pos, transform.rotation);
			//defeatdragon = true;
			Destroy (gameObject);

		}
	}

}
