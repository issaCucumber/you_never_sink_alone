using UnityEngine;
using System.Collections;

public class collisionDetection : MonoBehaviour {

	public float radius = 0.2f;
	public float rayLength = 0.15f;
	public bool[] rayColArr;

	void Start()
	{

	}
	void FixedUpdate()
	{

		int noRay = 8;
		float angle = 0;
		Vector2 dir = transform.parent.up* rayLength;
		rayColArr = new bool[noRay];
		Color[] rayColors = new Color[8] {Color.black,//Pos Y 
			Color.blue,//Pos Y Neg X
			Color.white,//Neg Y Neg X
			Color.green,//Pos X
			Color.grey,//Neg X
			Color.magenta,//Pos Y Pos X
			Color.red,//Neg Y Pos X
			Color.yellow};//Neg Y



		for (int i = 0; i < noRay; i++)
		{
			rayColArr[i] = false;
			angle = i * 360 / noRay;
			Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
			dir = q * dir;

			Debug.DrawRay(transform.position, dir, rayColors[i]);

			RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, rayLength);
			if (hit.collider != null)
			{
				rayColArr[i] = true;
				//print(i + " " + hit.collider.gameObject.name);
			}
		}
	}

	/* void DetectCollision(Vector3 center, float radius)
    {
        Collider[] hitColliders = Physics.OverlapSphere(center, radius);
        print(hitColliders.Length);
        for (int i = 0; i < hitColliders.Length; i++)
        {
            print(hitColliders[i].gameObject.name);
            isCollided = true;
        }
    }
    private void OnDrawGizmos()
    {
     Gizmos.color = Color.green;
     Gizmos.DrawWireSphere(transform.position , radius);
    }*/

}