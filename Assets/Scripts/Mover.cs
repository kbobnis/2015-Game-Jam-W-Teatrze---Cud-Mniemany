using UnityEngine;
using System.Collections;

public class Mover : MonoBehaviour
{
	private float MinX = -3.346683f;
	private float MaxX = 3.5f;
	private float Speed;

	void Update()
	{
		if (transform.position.x < MaxX)
		{
			transform.position += new Vector3(Speed * Time.deltaTime, 0, 0);
		}
	}

	internal void Prepare(int p)
	{
		Speed = (MaxX - MinX) / p;
	}
}
