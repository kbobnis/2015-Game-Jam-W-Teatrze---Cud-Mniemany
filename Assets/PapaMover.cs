using UnityEngine;
using System.Collections;

public class PapaMover : MonoBehaviour
{

	private float MinX ;
	private float MaxX = 4;
	private float Speed;

	void OnEnable()
	{
		if (MinX == 0)
		{
			MinX = gameObject.transform.position.x;
		}
	}

	// Update is called once per frame
	void Update()
	{
		gameObject.transform.position += new Vector3(Speed * Time.deltaTime, 0, 0);
	}

	internal void Restart(int p)
	{
		gameObject.transform.position = new Vector3(MinX, transform.position.y, transform.position.z);
		Speed = (MaxX - MinX) / p;
	}
}
