using UnityEngine;
using System.Collections;

public class Rock : MonoBehaviour {

	private const int HULL_REDUCE_BY_ROCKS 	= 1;
	private const string ROCK_SPRITE_PREFIX = "rocks";

	// Use this for initialization
	void Start () {

		int rock_index = Mathf.RoundToInt (Random.Range (1.0f, 4.0f));
		string rock_image = string.Concat ("rocks", rock_index.ToString ());
		GetComponent<SpriteRenderer> ().sprite = Resources.Load(rock_image, typeof(Sprite)) as Sprite;

	}
}
