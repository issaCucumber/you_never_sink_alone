using UnityEngine;
using System.Collections;

public class EnemyElectricShock : MonoBehaviour {

	public float bulletOffset = 5f;
	Transform ship;
	bool stopAfterShock = true;

	ShipActions sa;	
	EnemyDamageHandler edh;

	bool shocked = false;

	void Start () {

		edh = transform.GetComponent<EnemyDamageHandler> ();
		
	}

	void OnTriggerEnter2D(Collider2D other) {

		if (other.name.StartsWith("Ship")) {
			
			sa = GameObject.Find("Ship").transform.GetComponent<ShipActions>();

			sa.shocknow = true;
			sa.hullcurrent -= getEnemyDamageValue();

			transform.gameObject.GetComponent<Renderer> ().material.color = Color.cyan;

			if (stopAfterShock) {
				EnemyMoveForward emf = transform.GetComponent<EnemyMoveForward>();
				emf.stop = true;	
			}

			edh.health = 0;
			shocked = true;
		}
	}


	int getEnemyDamageValue() {
		if (gameObject.name.StartsWith ("ElectricEel")) {
			return 5;
		}
		return  0;
	}
}
