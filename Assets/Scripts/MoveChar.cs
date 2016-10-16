using UnityEngine;
using System.Collections;
using TeamUtility.IO;

public class MoveChar : MonoBehaviour {

	public float moveSpeed;
	public int controlNo;
	public int playerNo;

	//public character states
	public bool isContactWheel;
	public bool isContactCannonRight;
	public bool isContactCannonLeft;
	public bool isContactToolbox;
	public bool isContactDynamite;
	public bool[] rayColArr;

	//private character states
	bool isCollideWheel;
	bool isCollideCannonLeft;
	bool isCollideCannonRight;
	bool isCollideToolbox;
	bool isCollideDynamite;
	bool hypnotized = false;

	//stations
	public GameObject Wheel;
	public GameObject CannonLeft;
	public GameObject CannonRight;
	public GameObject Toolbox;
	public GameObject Dynamite;
	public GameObject Ship;



	Animator anim;

	void Start () {

		Transform ship = Ship.transform;
		anim = GetComponent<Animator>();
	}

	void Update () {

		if (controlNo == 4) {
			if (PlayerPrefs.GetInt ("Player2", 2) == 4) {
				if (PlayerPrefs.GetInt ("Player1", 1) == 1) {
					playerNo = 3;
				} else if (PlayerPrefs.GetInt ("Player1", 1) == 3) {
					playerNo = 4;
				}
			}
		}

		hypnotized = Ship.GetComponent<ShipActions> ().isHypnotized;
		rayColArr = gameObject.GetComponent<collisionDetection>().rayColArr;

		float inputX = (hypnotized == true ? -1 : 1) * modInputX (rayColArr, InputManager.GetAxis("Horizontal" + playerNo));
		float inputY = (hypnotized == true ? -1 : 1) * modInputY(rayColArr, InputManager.GetAxis("Vertical" + playerNo));

		anim.SetFloat("SpeedX", inputX);
		anim.SetFloat("SpeedY", inputY);

		//player movement when not stationed
		if (!isContactWheel && !isContactCannonLeft && !isContactCannonRight && !isContactToolbox && !isContactDynamite)
		{

			Vector3 movement = new Vector3(inputX, inputY, 0f);

			transform.Translate(movement * moveSpeed * Time.deltaTime);


			//reset state when disengaged
		} else if (InputManager.GetButtonDown("Disengage" + playerNo))	
		{
			//local state reset
			isContactWheel = false;
			isContactCannonLeft = false;
			isContactCannonRight = false;
			isContactToolbox = false;
			isContactDynamite = false;

			//public state reset
			Wheel.GetComponent<WheelActions>().wheelUsed = false;
			CannonLeft.GetComponent<PortCannonActions>().cannonUsed = false;
			CannonRight.GetComponent<StarboardCannonActions>().cannonUsed = false;
			Toolbox.GetComponent<ToolboxActions>().toolboxUsed = false;
			Dynamite.GetComponent<DynamiteActions>().activation[playerNo] = false;

			//reset char location and transform
			transform.parent = Ship.transform;
			transform.localRotation = Quaternion.Euler(0f,0f,0f);

			//remove outlines
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
			transform.localPosition = new Vector3(0.05f, 1.67F, 0.0f);
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
		hypnotized = Ship.GetComponent<ShipActions> ().hypnotizenow;
		float lastInputX = (hypnotized == true ? -1 : 1) * modInputX (rayColArr, InputManager.GetAxis("Horizontal" + playerNo));
		float lastInputY = (hypnotized == true ? -1 : 1) * modInputY (rayColArr, InputManager.GetAxis("Vertical" + playerNo));

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
			if (Wheel.GetComponent<WheelActions> ().wheelUsed == false) {
				isCollideWheel = true;
				Wheel.GetComponent<SpriteOutlineWhite> ().enabled = true;
			}
		}

		if (col.gameObject == CannonLeft)
		{
			if (CannonLeft.GetComponent<PortCannonActions> ().cannonUsed == false) {
				isCollideCannonLeft = true;
				CannonLeft.GetComponent<SpriteOutlineWhite> ().enabled = true;
			}
		}

		if (col.gameObject == CannonRight)
		{
			if (CannonRight.GetComponent<StarboardCannonActions> ().cannonUsed == false) {
				isCollideCannonRight = true;
				CannonRight.GetComponent<SpriteOutlineWhite> ().enabled = true;
			}
		}
		if (col.gameObject == Toolbox) {
			if (Toolbox.GetComponent<ToolboxActions> ().toolboxUsed == false) {
				isCollideToolbox = true;
				Toolbox.GetComponent<SpriteOutlineWhite> ().enabled = true;
			}
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

	float modInputX(bool[] rayColArr, float inputX)
	{
		float returnVal = inputX;
		/*Ray Parameters;
            Pos X = 3, 5, 6
            Neg X = 1, 2, 4
         */

		if (rayColArr[3] || rayColArr[5] || rayColArr[6])
		{
			if (returnVal > 0)
				returnVal = 0;
		}

		else if (rayColArr[1] || rayColArr[2] || rayColArr[4])
		{
			if (returnVal < 0)
				returnVal = 0;
		}

		return returnVal;
	}

	float modInputY(bool[] rayColArr, float inputY)
	{
		float returnVal = inputY;
		/*Ray Parameters;
            Pos Y = 0, 1, 5
            Neg Y = 2, 6, 7
         */

		if (rayColArr[0] || rayColArr[1] || rayColArr[5])
		{
			if (returnVal > 0)
				returnVal = 0;
		}

		else if (rayColArr[2] || rayColArr[6] || rayColArr[7])
		{
			if (returnVal < 0)
				returnVal = 0;
		}

		return returnVal;
	}
}