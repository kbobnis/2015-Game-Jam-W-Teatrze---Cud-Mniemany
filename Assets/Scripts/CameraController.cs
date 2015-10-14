using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
	public GameObject LookAtObject1, LookAtObject2;
	private float MinX = -4;
	private float MaxX = 4;
	private float Speed;

	void Start ()
	{
		if (LookAtObject1 == null || LookAtObject2 == null)
		{
			throw new System.Exception("There is no look at object defined");
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		transform.position += new Vector3(Speed * Time.deltaTime, 0, 0);
		Vector3 center = ((LookAtObject2.transform.position - LookAtObject1.transform.position) / 2.0f) + LookAtObject1.transform.position;
		transform.LookAt (center);
		//transform.RotateAround (LookAtObject.transform.position, Vector3.up, -10 * UnityEngine.Time.deltaTime);
	}

	internal void Restart(int p)
	{
		Speed = (MaxX - MinX) / p;

	}
}
