using UnityEngine;
using System.Collections;
using TeamUtility.IO;

public class MoveChar : MonoBehaviour {

    public float moveSpeed;
    public int playerNo;

    //character states
    public bool isContactWheel;
    public bool isContactCannonRight;
    public bool isContactCannonLeft;
    public bool isContactToolbox;
    public bool isContactDynamite;

    Animator anim;
    bool isCollideWheel;
    bool isCollideCannonLeft;
    bool isCollideCannonRight;
    bool isCollideToolbox;
    bool isCollideDynamite;

    //for outlining of stations
    public GameObject Wheel;
    public GameObject CannonLeft;
    public GameObject CannonRight;
    public GameObject Toolbox;
    public GameObject Dynamite;
	public GameObject Ship;

	bool hypnotized = false;
	Transform ship;

    void Start () {

		ship = GameObject.Find ("Ship").transform;
        anim = GetComponent<Animator>();
    }
	
	void Update () {
		hypnotized = ship.GetComponent<ShipActions> ().isHypnotized;

		float inputX = (hypnotized == true ? -1 : 1) * InputManager.GetAxisRaw("Horizontal" + playerNo);
		float inputY = (hypnotized == true ? -1 : 1) * InputManager.GetAxisRaw("Vertical" + playerNo);

        anim.SetFloat("SpeedX", inputX);
        anim.SetFloat("SpeedY", inputY);

        //player movement when not stationed
        if (!isContactWheel && !isContactCannonLeft && !isContactCannonRight && !isContactToolbox && !isContactDynamite)
        {

            Vector3 movement = new Vector3(inputX, inputY, 0f);

            transform.Translate(movement * moveSpeed * Time.deltaTime);


            //reset state when disengaged
		//} else if (InputManager.GetAxisRaw("Interact" + playerNo) < -0.5f)
		} else if (InputManager.GetButtonDown("Disengage" + playerNo))	
            {
                isContactWheel = false;
                isContactCannonLeft = false;
                isContactCannonRight = false;
                isContactToolbox = false;
                isContactDynamite = false;
				Dynamite.GetComponent<DynamiteActions>().activation[playerNo] = false;
                transform.parent = Ship.transform;
                transform.localRotation = Quaternion.Euler(0f,0f,0f);
                Wheel.GetComponent<SpriteOutlineGreen>().enabled = false;
                CannonLeft.GetComponent<SpriteOutlineGreen>().enabled = false;
                CannonRight.GetComponent<SpriteOutlineGreen>().enabled = false;
                Toolbox.GetComponent<SpriteOutlineGreen>().enabled = false;
                Dynamite.GetComponent<SpriteOutlineGreen>().enabled = false;

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
        } else if (isContactDynamite)
            {
                anim.SetBool("walking", false);
                anim.SetFloat("LastMoveX", 0f);
                anim.SetFloat("LastMoveY", 1f);
                Dynamite.GetComponent<SpriteOutlineWhite>().enabled = false;
                Dynamite.GetComponent<SpriteOutlineGreen>().enabled = true;
        }

        if (isCollideWheel)
        {
            //if (Input.GetAxis("Interact" + playerNo) > 0.5)
			if (InputManager.GetButtonDown("Interact" + playerNo))
                isContactWheel = true;
        }
        if (isCollideCannonLeft)
        {
			if (InputManager.GetButtonDown("Interact" + playerNo))
                isContactCannonLeft = true;
         }
        if(isCollideCannonRight)
        {
			if (InputManager.GetButtonDown("Interact" + playerNo))
                isContactCannonRight = true;
        }
        if (isCollideToolbox)
        {
			if (InputManager.GetButtonDown("Interact" + playerNo))
                isContactToolbox = true;
        }
        if (isCollideDynamite)
        {
			if (InputManager.GetButtonDown("Interact" + playerNo))
                isContactDynamite = true;
        }

    }

    void FixedUpdate()
	{
		hypnotized = ship.GetComponent<ShipActions> ().hypnotizenow;
		float lastInputX = (hypnotized == true ? -1 : 1) * InputManager.GetAxisRaw("Horizontal" + playerNo);
		float lastInputY = (hypnotized == true ? -1 : 1) * InputManager.GetAxisRaw("Vertical" + playerNo);
    
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
        if (col.gameObject == Dynamite)
        {
            isCollideDynamite = true;
            Dynamite.GetComponent<SpriteOutlineWhite>().enabled = true;
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
        if (col.gameObject == Dynamite)
        {
            isCollideDynamite = false;
            Dynamite.GetComponent<SpriteOutlineWhite>().enabled = false;
        }
    }
}
