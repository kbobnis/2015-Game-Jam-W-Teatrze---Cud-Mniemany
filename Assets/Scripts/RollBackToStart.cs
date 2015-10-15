using UnityEngine;
using System.Collections;

public class RollBackToStart : MonoBehaviour {

	private float MinX;
	private float MaxX = 3.5f;
	private float Speed;
	public bool Started;

	// Update is called once per frame
	void Update()
	{
		//if there is already mover then removing itself
		if (transform.position.x < MinX || GetComponent<Mover>() != null)
		{
			Destroy(this);
		}

		transform.position -= new Vector3(Speed * Time.deltaTime, 0, 0);
	}

	internal void Prepare(float minX)
	{
		MinX = minX;
		Speed = (MaxX - MinX) / 8;
	}
}
