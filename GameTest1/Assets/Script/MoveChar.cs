using UnityEngine;
using System.Collections;

public class MoveChar : MonoBehaviour {

    public float moveSpeed;
    public bool isContactWheel;
    public bool isContactCannonRight;
    public bool isContactCannonLeft;

    public int playerNo;

    Animator anim;
    //float moveHorizontal, moveVertical;

    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {


        float inputX = Input.GetAxisRaw("Horizontal" + playerNo);
        float inputY = Input.GetAxisRaw("Vertical" + playerNo);

  anim.SetFloat("SpeedX", inputX);
            anim.SetFloat("SpeedY", inputY);

        if (!isContactWheel && !isContactCannonLeft && !isContactCannonRight)
        {

            Vector3 movement = new Vector3(inputX, inputY, 0f);

            transform.Translate(movement * moveSpeed * Time.deltaTime, Space.World);

            

            //moveHorizontal = Input.GetAxis ("Horizontal" + playerNo);
            //moveVertical = Input.GetAxis ("Vertical" + playerNo);

            /* if (Input.GetAxisRaw("Horizontal" + playerNo) > 0.5f || Input.GetAxisRaw("Horizontal" + +playerNo) < -0.5f)
                 //transform.position += Vector3.right * Input.GetAxisRaw("Horizontal" + playerNo) * moveSpeed * Time.deltaTime;
                 transform.Translate(new Vector3(Input.GetAxisRaw("Horizontal" + playerNo) * moveSpeed * Time.deltaTime, 0f, 0f));

             if (Input.GetAxisRaw("Vertical" + playerNo) > 0.5f || Input.GetAxisRaw("Vertical" + playerNo) < -0.5f)
                 //transform.position += Vector3.up * Input.GetAxisRaw("Vertical" + playerNo) * moveSpeed * Time.deltaTime;
                 transform.Translate(new Vector3(0f, Input.GetAxisRaw("Vertical" + playerNo) * moveSpeed * Time.deltaTime, 0f));

             if (Input.GetAxisRaw ("Vertical" + playerNo) < -0.5f)
                 GetComponent<SpriteRenderer>().flipY = true;
             else
                 GetComponent<SpriteRenderer>().flipY = false;

             if (Input.GetAxisRaw("Horizontal" + playerNo) < -0.5f)
                 GetComponent<SpriteRenderer>().flipX = true;
             else
                 GetComponent<SpriteRenderer>().flipX = false;

     */
        } else if (Input.GetAxisRaw("Interact" + playerNo) < -0.5f)
            {
                isContactWheel = false;
                isContactCannonLeft = false;
                isContactCannonRight = false;

        } else if (isContactCannonLeft)
        {
            anim.SetBool("walking", false);
            anim.SetFloat("LastMoveX", -1f);
            anim.SetFloat("LastMoveY", 0f);
        }
        else if (isContactCannonRight)
        {
            anim.SetBool("walking", false);
            anim.SetFloat("LastMoveX", 1f);
            anim.SetFloat("LastMoveY", 0f);
        }
        else if (isContactWheel)
        {
            anim.SetBool("walking", false);
            anim.SetFloat("LastMoveX", 0f);
            anim.SetFloat("LastMoveY", 1f);
        }
    }

    void FixedUpdate()
    {
        float lastInputX = Input.GetAxisRaw("Horizontal" + playerNo);
        float lastInputY = Input.GetAxisRaw("Vertical" + playerNo);

            if (lastInputX != 0 || lastInputY != 0)
            {
                anim.SetBool("walking", true);
                if (lastInputX > 0)
                {
                    anim.SetFloat("LastMoveX", 1f);
                }
                else if (lastInputX < 0)
                {
                    anim.SetFloat("LastMoveX", -1f);
                }
                else
                {
                    anim.SetFloat("LastMoveX", 0f);
                }

                if (lastInputY > 0)
                {
                    anim.SetFloat("LastMoveY", 1f);
                }
                else if (lastInputY < 0)
                {
                    anim.SetFloat("LastMoveY", -1f);
                }
                else
                {
                    anim.SetFloat("LastMoveY", 0f);
                }
            }
            else
            {
                anim.SetBool("walking", false);
            }
    }


void OnTriggerEnter2D (Collider2D col)
    {
        if (col.gameObject.tag == "wheel")  
            isContactWheel = true;
    
        if (col.gameObject.name == "cannon")
            isContactCannonLeft = true;

        if (col.gameObject.name == "cannon right")
            isContactCannonRight = true;
    }
}
