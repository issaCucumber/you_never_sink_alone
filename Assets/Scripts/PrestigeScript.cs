using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PrestigeScript : MonoBehaviour {

    [SerializeField]
    private Text valueText;

    private int currValue = 0;
    private int prevValue = 0;

    public int Value
    {
        set
        {
            currValue = value;
        }
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
        if(prevValue < currValue)
        {
            prevValue++;
        }
        else if (prevValue > currValue)
        {
            prevValue--;
        }

        valueText.text = "Prestige: " + prevValue.ToString();

	}
}
