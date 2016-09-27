using UnityEngine;
using System.Collections;

public class SuperAttackActions : MonoBehaviour {

	public GameObject Ship;
	private float speed = 20.0f;

    // Use this for initialization
    void Start () {

    }

    // Update is called once per frame
    void Update () {
        CircleCollider2D myCollider = transform.GetComponent<CircleCollider2D>();
        transform.localScale += new Vector3(1,1,1)*speed*Time.deltaTime;
        myCollider.radius += speed * Time.deltaTime;
        if (myCollider.radius > 10.0f)
        {
            Destroy(this.gameObject);
        }
    }

	void OnTriggerEnter2D(Collider2D coll) {
		//if (coll.gameObject != Ship.GetChild)
			Debug.Log ("Collide with smth");
	}
}
