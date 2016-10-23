using UnityEngine;
using System.Collections;

public class EnermySpawn2 : MonoBehaviour {
	public GameObject[] enermyPrefabs = new GameObject[4];
	public GameObject[] islands = new GameObject[5];
	public GameObject ship;
	private Vector3 spawnpos;
	public float spawnerRadius = 25f;
	public float timeInterval = 3f;
	public float increaseRate = 0.1f;//increase x persent per minutes
	float timer = 0f;
	float globaltimer = 0f;

	void Update () {
		globaltimer -= Time.deltaTime;
		if (timer <= 0) {
			Spawn ();
		} else {
			timer -= Time.deltaTime;
		}
	}

	void Spawn(){
		int enemytype = Random.Range (0,3);
		float radius = Random.Range(spawnerRadius * 0.7f, spawnerRadius);
		float islandx;
		float islandy;
		float islandlength;
		timer = timeInterval * (1+ ((globaltimer/60) * increaseRate));

		Vector3 randomunitsphere = Random.onUnitSphere.normalized;
		Vector3 randomunitcircel = new Vector3 (randomunitsphere.x,randomunitsphere.y,0);;
		spawnpos = ship.transform.position + randomunitcircel * radius;

		for (int i = 0; i < 5; i++) {
			islandx = islands [i].gameObject.GetComponent<SpriteRenderer> ().bounds.size.x;
			islandy = islands [i].gameObject.GetComponent<SpriteRenderer> ().bounds.size.y;
			islandlength = islandx > islandy ? islandx : islandy;

			if (Vector3.Distance(spawnpos, islands[i].transform.position) < islandlength/2f + 1) {//approximate
				return;
			}
		}

		Instantiate (enermyPrefabs [enemytype], spawnpos, Quaternion.identity);
	}
}
