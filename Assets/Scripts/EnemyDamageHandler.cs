using UnityEngine;
using System.Collections;

public class EnemyDamageHandler : MonoBehaviour {
	
	public GameObject explode;

	public bool clashShipDie = true;

	public int health = 1;

	bool exploded = false;

	float deathDelay = 0;

	ShipActions sa;	

	void Start () {
		sa = GameObject.Find ("Ship").transform.GetComponent<ShipActions>();
	}

	void OnTriggerEnter2D(Collider2D other) {
		//Debug.Log ("other name is "+other.name);
		if (other.name == "Ship") {
			if (clashShipDie) {
				//TODO Sound: EnemyCollideShip.wav
				sa.hullcurrent -= getEnemyDamageValue ("clash");
				health = 0;
			}
		} else if (other.name.StartsWith("Rock")) {
			health = 0;
			//Debug.Log("hit Rock health is "+health);
		} else if (other.name.StartsWith("Cannonball")) {
			//Debug.Log("hit Cannonball");
			Shoot bullet = other.GetComponent<Shoot>();
			if (bullet.level == 1) {
				health -= 2;
			} else if (bullet.level == 2) {
				health -= 4;
			} else if (bullet.level == 3) {
				health -= 6;
			} else if (bullet.level == 4) {
				health -= 8;
			} else if (bullet.level == 5) {
				health -= 10;
			} 
			//invulnTimer = 1f;
			//gameObject.layer = 10;

		}else if (other.name.StartsWith("SuperAttack")) {
			health -= 20;
		}

		float deathDelay = 0;

	}

	// Update is called once per frame
	void Update () {
		deathDelay -= Time.deltaTime;

		if (health <= 0 ) {
			if (!exploded && explode != null) {
				Instantiate (explode, transform.position, transform.rotation);
				exploded = true;
			}

			if(deathDelay <= 0) {
//				Debug.Log ("health="+health);
//				Debug.Log ("deathDelay="+deathDelay);
				sa.prestigevalue = getEnemyPrestigeValue();
				Die();
			}
		}
		
	}

	int getEnemyDamageValue(string attack) {
		if (attack.Equals("clash")) {
			if (gameObject.name.StartsWith ("FlyingFish")) {
				return 5;
			} else if (gameObject.name.StartsWith ("Octopus")) {
				return 10;
			}
		}
		return  0;
	}

	int getEnemyPrestigeValue() {
		if (gameObject.name.StartsWith ("Dragon")) {
			return 50;
		} else if (gameObject.name.StartsWith ("ElectricEel")) {
			return 10;
		} else if (gameObject.name.StartsWith ("FlyingFish")) {
			return 5;
		} else if (gameObject.name.StartsWith ("JellyFish")) {
			return 15;
		} else if (gameObject.name.StartsWith ("Octopus")) {
			return 20;
		}
		return  0;
	}

	void Die() {
		//Debug.Log ("I Die");
		Destroy (gameObject);
	}

}
