using UnityEngine;
using System.Collections;

public class MoveShip : MonoBehaviour {

    public float maxSpeed;
    public GameObject[] charArray;
    Animator anim;

	// Use this for initialization
	void Start () {
        //anim = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {

        for (int k = 0; k < 2; k++) {

            if (charArray[k].GetComponent<MoveChar>().isContactWheel)
            {

                //float move = Input.GetAxis("Horizontal");

                //anim.SetFloat("Speed", Mathf.Abs(move));

                int i = charArray[k].GetComponent<MoveChar>().playerNo;
                charArray[0].GetComponent<Rigidbody2D>().velocity = GetComponent<Rigidbody2D>().velocity;
                charArray[1].GetComponent<Rigidbody2D>().velocity = GetComponent<Rigidbody2D>().velocity;
                if (Input.GetAxisRaw("Interact" + i) > 0.5f)
                {
                    GetComponent<Rigidbody2D>().AddForce(transform.up * maxSpeed);
                    //charArray[k].GetComponent<Rigidbody2D>().velocity = GetComponent<Rigidbody2D>().velocity;
                    //transform.position += transform.right * Time.smoothDeltaTime * maxSpeed;
                }
                if (Input.GetAxisRaw("Horizontal" + i) < -0.5f)
                    RotateLeft();

                if (Input.GetAxisRaw("Horizontal" + i) > 0.5f)
                    RotateRight();

            }
        }

    }

    void RotateLeft()
    {
        transform.Rotate(Vector3.forward);
    }

    void RotateRight()
    {
        transform.Rotate(Vector3.forward * -1);
    }

}
