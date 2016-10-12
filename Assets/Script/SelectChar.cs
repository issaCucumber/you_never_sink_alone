using UnityEngine;
using System.Collections;

public class SelectChar : MonoBehaviour {

	public GameObject[] charArray;

	// Use this for initialization
	void Start () {
		int i = PlayerPrefs.GetInt ("Player1", 1);
		if (i == 1)
			charArray [0].SetActive(true);
		else if (i == 3)
			charArray [2].SetActive(true);

		int j = PlayerPrefs.GetInt ("Player2", 2);
		if (j == 2)
			charArray [1].SetActive(true);
		else if (j == 4)
			charArray [3].SetActive(true);
	
	}
}
