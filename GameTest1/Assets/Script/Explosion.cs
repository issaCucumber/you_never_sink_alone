using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour {

    private float starttime;
    public int level = 1;

    // Use this for initialization
    void Start () {
        starttime = Time.time;

        // Scale explosion size according to level
        transform.localScale = new Vector3((float)level / 2.0f, (float)level / 2.0f, (float)level / 2.0f);
    }

    // Update is called once per frame
    void Update () {
	    if (Time.time - starttime > 0.3f)
        {
            Destroy(this.gameObject);
        }
    }
}
