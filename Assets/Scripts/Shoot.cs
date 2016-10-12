using UnityEngine;
using System.Collections;

public class Shoot : MonoBehaviour {

    public float speed = 5;
    public int level = 1;
    public Transform explosion;
	private float starttime;

    // Use this for initialization
    void Start () {
		starttime = Time.time;

        // Scale cannonbal size according to level
        transform.localScale = new Vector3((float)level / 10.0f, (float)level / 10.0f, (float)level / 10.0f);

        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        //float cannonAngle = 0;
        //cannonAngle = transform.rotation.eulerAngles.z;
        //Vector3 direction = new Vector3(Mathf.Cos(cannonAngle * Mathf.Deg2Rad), Mathf.Sin(cannonAngle * Mathf.Deg2Rad), 0);
        rb.AddForce(transform.right * speed * 100 );
   }

    // Update is called once per frame
    void Update () {
		if (Time.time - starttime > 0.5f) {
			if (GetComponent<Rigidbody2D> ().velocity.magnitude < 0.5f) {
				Destroy (this.gameObject);
			}
		}
    }

    void OnBecameInvisible() {
//        Destroy(this.gameObject);
    }

    void OnTriggerEnter2D(Collider2D coll) {
//        if (coll.gameObject.tag == "Enemy")
//            coll.gameObject.SendMessage("ApplyDamage", 10);
		if (coll.name.StartsWith("Jellyfish") 
			|| coll.name.StartsWith("Octopus")
			|| coll.name.StartsWith("ElectricEel")
			|| coll.name.StartsWith("FlyingFish")
			|| coll.name.StartsWith("Dragon")
			|| coll.name.StartsWith("Rock")) {
			Destroy (this.gameObject);
			Transform myexplosion = (Transform)Instantiate (explosion, coll.transform.position, transform.rotation);
			myexplosion.GetComponent<Explosion> ().level = level;
		}
    }
}
