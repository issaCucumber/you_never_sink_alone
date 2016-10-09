using UnityEngine;
using System.Collections;
using TeamUtility.IO;

public class MoveShip : MonoBehaviour {

    public float maxSpeed;
    public GameObject[] charArray;
    Animator anim;

	void Start () {
    }
	
	void Update () {

        for (int k = 0; k < 2; k++) {

            if (charArray[k].GetComponent<MoveChar>().isContactWheel)
            {

                int i = charArray[k].GetComponent<MoveChar>().playerNo;
                charArray[0].GetComponent<Rigidbody2D>().velocity = GetComponent<Rigidbody2D>().velocity;
                charArray[1].GetComponent<Rigidbody2D>().velocity = GetComponent<Rigidbody2D>().velocity;

                if (InputManager.GetAxisRaw("Interact" + i) > 0.5f)
                {
                    GetComponent<Rigidbody2D>().AddForce(transform.up * maxSpeed);
                }
				if (InputManager.GetAxisRaw("Horizontal" + i) < -0.5f)
                    RotateLeft();

				if (InputManager.GetAxisRaw("Horizontal" + i) > 0.5f)
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
