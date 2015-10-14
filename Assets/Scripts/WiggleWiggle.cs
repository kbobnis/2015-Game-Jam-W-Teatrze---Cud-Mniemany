using UnityEngine;
using System.Collections;

public class WiggleWiggle : MonoBehaviour {
	private GameObject ChildLeft, ChildRight;
	private int ChildLeftDirection, ChildRightDirection; 

	public float Angle=10;
	public float Speed=30;
	// Use this for initialization
	void Start () 
	{
		ChildLeft = transform.GetChild (0).gameObject;
		ChildRight = transform.GetChild (1).gameObject;
		ChildLeftDirection = -1;
		ChildRightDirection = 1;

		ChildLeft.transform.localEulerAngles = new Vector3 (0, 0, Random.Range (-Angle, Angle));
		ChildRight.transform.localEulerAngles = new Vector3 (0, 0, Random.Range (-Angle, Angle));
	}
	
	// Update is called once per frame
	void Update () 
	{

		ChildLeft.transform.RotateAround (ChildLeft.transform.position-ChildLeft.transform.up, transform.forward, ChildLeftDirection * Speed * UnityEngine.Time.deltaTime);
		ChildRight.transform.RotateAround (ChildRight.transform.position-ChildRight.transform.up, transform.forward, ChildRightDirection * Speed * UnityEngine.Time.deltaTime);

		if (ChildLeft.transform.localEulerAngles.z > Angle && ChildLeft.transform.localEulerAngles.z < Angle*3) {
			ChildLeftDirection = -1;
		} else if (ChildLeft.transform.localEulerAngles.z < 360-Angle && ChildLeft.transform.localEulerAngles.z > 360-Angle*3) {
			ChildLeftDirection = 1;
		}

		if (ChildRight.transform.localEulerAngles.z > Angle && ChildRight.transform.localEulerAngles.z < Angle*3) {
			ChildRightDirection = -1;
		} else if (ChildRight.transform.localEulerAngles.z < 360-Angle && ChildRight.transform.localEulerAngles.z > 360-Angle*3) {
			ChildRightDirection = 1;
		}

	}



}
