using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
	public GameObject LookAtObject;
	public float Time=20;

	void Start ()
	{
		if (LookAtObject == null)
		{
			throw new System.Exception("There is no look at object defined");
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Time > 0)
		{
			transform.LookAt (LookAtObject.transform);
			transform.RotateAround (LookAtObject.transform.position, Vector3.up, -10 * UnityEngine.Time.deltaTime);
			Time-=UnityEngine.Time.fixedDeltaTime;
		}
	}
}
