using UnityEngine;
using System.Collections;
using TeamUtility.IO;

public class ObjectiveManager : MonoBehaviour {

    public GameObject objectiveScreen;

	// Use this for initialization
	void Start () {

        if (PlayerPrefs.GetInt(Constants.DEFEATDRAGON, 0) == 0)
        {
            Time.timeScale = 0;
            objectiveScreen.SetActive(true);
        }
        else
        {
            objectiveScreen.SetActive(false);
        }

    }

    // Update is called once per frame
    void Update()
    {

        if (objectiveScreen.activeInHierarchy == true)
        {
            if (InputManager.GetKeyDown(KeyCode.Space) || InputManager.GetKeyDown(KeyCode.Joystick1Button2))
            {
                Time.timeScale = 1;
                objectiveScreen.SetActive(false);
            }
        }
    }
}
