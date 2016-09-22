using UnityEngine;
using System.Collections;

public class MoveChar : MonoBehaviour {

    public float moveSpeed;
    public int playerNo;

    //character states
    public bool isContactWheel;
    public bool isContactCannonRight;
    public bool isContactCannonLeft;

    Animator anim;
    bool isCollideWheel;
    bool isCollideCannonLeft;
    bool isCollideCannonRight;

    //for outlining of stations
    public GameObject Wheel;
    public GameObject CannonLeft;
    public GameObject CannonRight;

    void Start () {
        anim = GetComponent<Animator>();
    }
	
	void Update () {

        float inputX = Input.GetAxisRaw("Horizontal" + playerNo);
        float inputY = Input.GetAxisRaw("Vertical" + playerNo);

        anim.SetFloat("SpeedX", inputX);
        anim.SetFloat("SpeedY", inputY);

        //player movement when not stationed
        if (!isContactWheel && !isContactCannonLeft && !isContactCannonRight)
        {

            Vector3 movement = new Vector3(inputX, inputY, 0f);

            transform.Translate(movement * moveSpeed * Time.deltaTime, Space.World);


        //reset state when disengaged
        } else if (Input.GetAxisRaw("Interact" + playerNo) < -0.5f)
            {
                isContactWheel = false;
                isContactCannonLeft = false;
                isContactCannonRight = false;
                Wheel.GetComponent<SpriteOutlineGreen>().enabled = false;
                CannonLeft.GetComponent<SpriteOutlineGreen>().enabled = false;
                CannonRight.GetComponent<SpriteOutlineGreen>().enabled = false;

            //lock character in place when stationed
        } else if (isContactCannonLeft)
            {
                anim.SetBool("walking", false);
                anim.SetFloat("LastMoveX", -1f);
                anim.SetFloat("LastMoveY", 0f);
                CannonLeft.GetComponent<SpriteOutlineWhite>().enabled = false;
                CannonLeft.GetComponent<SpriteOutlineGreen>().enabled = true;
        } else if (isContactCannonRight)
            {
                anim.SetBool("walking", false);
                anim.SetFloat("LastMoveX", 1f);
                anim.SetFloat("LastMoveY", 0f);
                CannonRight.GetComponent<SpriteOutlineWhite>().enabled = false;
                CannonRight.GetComponent<SpriteOutlineGreen>().enabled = true;
        } else if (isContactWheel)
            {
                anim.SetBool("walking", false);
                anim.SetFloat("LastMoveX", 0f);
                anim.SetFloat("LastMoveY", 1f);
                Wheel.GetComponent<SpriteOutlineWhite>().enabled = false;
                Wheel.GetComponent<SpriteOutlineGreen>().enabled = true;
        }

        if (isCollideWheel)
        {
            if (Input.GetAxis("Interact" + playerNo) > 0.5)
                isContactWheel = true;
        }
        if (isCollideCannonLeft)
        {
            if (Input.GetAxis("Interact" + playerNo) > 0.5)
                isContactCannonLeft = true;
         }
        if(isCollideCannonRight)
        {
            if (Input.GetAxis("Interact" + playerNo) > 0.5)
                isContactCannonRight = true;
        }


    }

    void FixedUpdate()
    {
        float lastInputX = Input.GetAxisRaw("Horizontal" + playerNo);
        float lastInputY = Input.GetAxisRaw("Vertical" + playerNo);

            //get last input to display static animation
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
        {
            isCollideWheel = true;
            Wheel.GetComponent<SpriteOutlineWhite>().enabled = true;
        }

        if (col.gameObject.name == "cannon")
        {
            isCollideCannonLeft = true;
            CannonLeft.GetComponent<SpriteOutlineWhite>().enabled = true;
        }

        if (col.gameObject.name == "cannon right")
        {
            isCollideCannonRight = true;
            CannonRight.GetComponent<SpriteOutlineWhite>().enabled = true;
        }
    }

    void OnTriggerExit2D (Collider2D col)
    {
        if (col.gameObject.tag == "wheel")
        {
            isCollideWheel = false;
            Wheel.GetComponent<SpriteOutlineWhite>().enabled = false;
        }

        if (col.gameObject.name == "cannon")
        {
            isCollideCannonLeft = false;
            CannonLeft.GetComponent<SpriteOutlineWhite>().enabled = false;
        }

        if (col.gameObject.name == "cannon right")
        {
            isCollideCannonRight = false;
            CannonRight.GetComponent<SpriteOutlineWhite>().enabled = false;
        }
    }
}
