using UnityEngine;
using System.Collections;

public class SuperAttackActions : MonoBehaviour {

    private float speed = 20.0f;

    // Use this for initialization
    void Start () {

    }

    // Update is called once per frame
    void Update () {
        CircleCollider2D myCollider = transform.GetComponent<CircleCollider2D>();
        myCollider.radius += speed * Time.deltaTime;
        if (myCollider.radius > 11.0f)
        {
            Destroy(this.gameObject);
        }
    }
}
