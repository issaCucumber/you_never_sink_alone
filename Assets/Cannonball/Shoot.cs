using UnityEngine;
using System.Collections;

public class Shoot : MonoBehaviour {

    public float speed = 5;
    public int level = 1;
    public Transform explosion;

    // Use this for initialization
    void Start () {
        // Scale cannonbal size according to level
        transform.localScale = new Vector3((float)level / 20.0f, (float)level / 20.0f, (float)level / 20.0f);

        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        float cannonAngle = 0;
        cannonAngle = transform.rotation.eulerAngles.z;
        Vector3 direction = new Vector3(Mathf.Cos(cannonAngle * Mathf.Deg2Rad), Mathf.Sin(cannonAngle * Mathf.Deg2Rad), 0);
        rb.AddForce(direction * speed * 100 * -1);
   }

    // Update is called once per frame
    void Update () {
    }

    void OnBecameInvisible() {
        Destroy(this.gameObject);
    }

    void OnTriggerEnter2D(Collider2D coll) {
//        if (coll.gameObject.tag == "Enemy")
//            coll.gameObject.SendMessage("ApplyDamage", 10);
        Transform myexplosion = (Transform)Instantiate(explosion, transform.position, transform.rotation);
        myexplosion.GetComponent<Explosion>().level = level;
        Destroy(this.gameObject);
    }
}
