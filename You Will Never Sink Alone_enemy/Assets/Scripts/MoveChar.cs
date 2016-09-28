using UnityEngine;
using System.Collections;

public class MoveChar : MonoBehaviour {

    public float moveSpeed;
    public int playerNo;

    //character states
    public bool isContactWheel;
    public bool isContactCannonRight;
    public bool isContactCannonLeft;
    public bool isContactToolbox;

    Animator anim;
    bool isCollideWheel;
    bool isCollideCannonLeft;
    bool isCollideCannonRight;
    bool isCollideToolbox;

    //for outlining of stations
    public GameObject Wheel;
    public GameObject CannonLeft;
    public GameObject CannonRight;
    public GameObject Toolbox;
    public GameObject Ship;

    void Start () {
        anim = GetComponent<Animator>();
    }
	
	void Update () {

        float inputX = Input.GetAxisRaw("Horizontal" + playerNo);
        float inputY = Input.GetAxisRaw("Vertical" + playerNo);

        anim.SetFloat("SpeedX", inputX);
        anim.SetFloat("SpeedY", inputY);

        //player movement when not stationed
        if (!isContactWheel && !isContactCannonLeft && !isContactCannonRight && !isContactToolbox)
        {

            Vector3 movement = new Vector3(inputX, inputY, 0f);

            transform.Translate(movement * moveSpeed * Time.deltaTime);


            //reset state when disengaged
        } else if (Input.GetAxisRaw("Interact" + playerNo) < -0.5f)
            {
                isContactWheel = false;
                isContactCannonLeft = false;
                isContactCannonRight = false;
                isContactToolbox = false;
                transform.parent = Ship.transform;
                transform.localRotation = Quaternion.Euler(0f,0f,0f);
                Wheel.GetComponent<SpriteOutlineGreen>().enabled = false;
                CannonLeft.GetComponent<SpriteOutlineGreen>().enabled = false;
                CannonRight.GetComponent<SpriteOutlineGreen>().enabled = false;
                Toolbox.GetComponent<SpriteOutlineGreen>().enabled = false;

            //lock character in place when stationed
        } else if (isContactCannonLeft)
            {
                anim.SetBool("walking", false);
                anim.SetFloat("LastMoveX", -1f);
                anim.SetFloat("LastMoveY", 0f);
                transform.parent = CannonLeft.transform;
                transform.localPosition = new Vector3(0.33f, 0.0f, 0.0f);
                CannonLeft.GetComponent<SpriteOutlineWhite>().enabled = false;
                CannonLeft.GetComponent<SpriteOutlineGreen>().enabled = true;
        } else if (isContactCannonRight)
            {
                anim.SetBool("walking", false);
                anim.SetFloat("LastMoveX", 1f);
                anim.SetFloat("LastMoveY", 0f);
                transform.parent = CannonRight.transform;
                transform.localPosition = new Vector3(0.33f, 0.0f, 0.0f);
                CannonRight.GetComponent<SpriteOutlineWhite>().enabled = false;
                CannonRight.GetComponent<SpriteOutlineGreen>().enabled = true;
        } else if (isContactWheel)
            {
                anim.SetBool("walking", false);
                anim.SetFloat("LastMoveX", 0f);
                anim.SetFloat("LastMoveY", 1f);
                transform.localPosition = new Vector3(0.04f, -1.6f, 0.0f);
                Wheel.GetComponent<SpriteOutlineWhite>().enabled = false;
                Wheel.GetComponent<SpriteOutlineGreen>().enabled = true;
        } else if (isContactToolbox)
            {
                anim.SetBool("walking", false);
                anim.SetFloat("LastMoveX", 0f);
                anim.SetFloat("LastMoveY", -1f);
                transform.localPosition = new Vector3(0.06f, 2.04f, 0.0f);
                Toolbox.GetComponent<SpriteOutlineWhite>().enabled = false;
                Toolbox.GetComponent<SpriteOutlineGreen>().enabled = true;
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
        if (isCollideToolbox)
        {
            if (Input.GetAxis("Interact" + playerNo) > 0.5)
                isContactToolbox = true;
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
        if (col.gameObject == Wheel)
        {
            isCollideWheel = true;
            Wheel.GetComponent<SpriteOutlineWhite>().enabled = true;
        }

        if (col.gameObject == CannonLeft)
        {
            isCollideCannonLeft = true;
            CannonLeft.GetComponent<SpriteOutlineWhite>().enabled = true;
        }

        if (col.gameObject == CannonRight)
        {
            isCollideCannonRight = true;
            CannonRight.GetComponent<SpriteOutlineWhite>().enabled = true;
        }
        if (col.gameObject == Toolbox)
        {
            isCollideToolbox = true;
            Toolbox.GetComponent<SpriteOutlineWhite>().enabled = true;
        }
    }

    void OnTriggerExit2D (Collider2D col)
    {
        if (col.gameObject == Wheel)
        {
            isCollideWheel = false;
            Wheel.GetComponent<SpriteOutlineWhite>().enabled = false;
        }

        if (col.gameObject == CannonLeft)
        {
            isCollideCannonLeft = false;
            CannonLeft.GetComponent<SpriteOutlineWhite>().enabled = false;
        }

        if (col.gameObject == CannonRight)
        {
            isCollideCannonRight = false;
            CannonRight.GetComponent<SpriteOutlineWhite>().enabled = false;
        }
        if (col.gameObject == Toolbox)
        {
            isCollideToolbox = false;
            Toolbox.GetComponent<SpriteOutlineWhite>().enabled = false;
        }
    }
}
