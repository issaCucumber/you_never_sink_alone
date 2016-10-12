using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class KeyBindScript : MonoBehaviour {

    private Dictionary<string, KeyCode> keys = new Dictionary<string, KeyCode>();

    public Text test;

    private GameObject currentKey;

    // Use this for initialization
    void Start() {
        keys.Add("Test", KeyCode.Space);

        test.text = keys["Test"].ToString();

    }

    // Update is called once per frame
    void Update() {

    }

    void OnGUI()
    {
        if (currentKey != null)
        {
            //System.Array values = System.Enum.GetValues(typeof(KeyCode));
            //foreach (KeyCode code in values)
            //{
            //    if (Input.GetKeyDown(code)) { print(System.Enum.GetName(typeof(KeyCode), code)); }
            //}
            //Debug.Log("ABC");
            if (Input.GetKeyDown(KeyCode.Joystick1Button2))
            {
                Debug.Log("wohoo!!");
                currentKey = null;
            }


            Event e = Event.current;

            if (e.isKey)
            {
                keys[currentKey.name] = e.keyCode;
                currentKey.GetComponent<Text>().text = e.keyCode.ToString();
                currentKey = null;
            }
            
        }
    }

    public void ChangeKey(GameObject clicked)
    {
        currentKey = clicked;
    }
}
