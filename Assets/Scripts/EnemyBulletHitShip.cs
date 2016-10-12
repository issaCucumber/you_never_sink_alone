using UnityEngine;
using System.Collections;

public class EnemyBulletHitShip : MonoBehaviour {

	// Use this for initialization
	bool hitship = false;
	Transform ship;
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (hitship) {
			ship.gameObject.GetComponent<ShipActions> ().hullcurrent -= getEnemyDamageValue();
			hitship = false;
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.name == "Ship") {
			hitship = true;
			ship = other.transform;
		}
	}



	int getEnemyDamageValue() {
		if (gameObject.name.StartsWith ("DragonFire")) {
			return 15;
		} else if (gameObject.name.StartsWith ("DragonWave")) {
			return 25;
		}
		return  0;
	}
}
