using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class PauseGame : MonoBehaviour {

    public EventSystem ES;
    private GameObject storeSelected;

    public Transform canvas;
    public Transform playerObj;

	// Use this for initialization
	void Start () {
        storeSelected = ES.firstSelectedGameObject;
    }
	
	// Update is called once per frame
	void Update () {

	    if(Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }

        if (ES.currentSelectedGameObject != storeSelected)
        {
            if (ES.currentSelectedGameObject == null)
            {
                ES.SetSelectedGameObject(storeSelected);
            }
            else
            {
                storeSelected = ES.currentSelectedGameObject;
            }
        }
    }

    private void Pause()
    {
        if (canvas.gameObject.activeInHierarchy == false)
        {
            canvas.gameObject.SetActive(true);
            Time.timeScale = 0;
            playerObj.GetComponent<Player>().enabled = false;
        }
        else
        {
            canvas.gameObject.SetActive(false);
            Time.timeScale = 1;
            playerObj.GetComponent<Player>().enabled = true;
        }
    }
}
