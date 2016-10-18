using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CrewScript : MonoBehaviour {

    public GameObject playerObj;
    public Text crewText;

    private ShipActions ship;

	// Use this for initialization
	void Start () {
        ship = playerObj.GetComponent<ShipActions>();
	}
	
	// Update is called once per frame
	void Update () {

        crewText.text = ship.crewsaved + "/" + ship.crewtosave;

    }
}
