using UnityEngine;
using System.Collections;

public class PapaMover : MonoBehaviour
{

	private float MinX = -4;
	private float MaxX = 4;
	private float Speed;

	// Update is called once per frame
	void Update()
	{
		transform.position += new Vector3(Speed * Time.deltaTime, 0, 0);
	}

	internal void Restart(int p)
	{
		transform.position.Set(MinX, transform.position.y, transform.position.z);
		Speed = (MaxX - MinX) / p;
	}
}
