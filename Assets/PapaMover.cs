using UnityEngine;
using System.Collections;

public class PapaMover : MonoBehaviour
{

	private float MinX ;
	private float MaxX = 3.5f;
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
		if (transform.position.x < MaxX)
		{
			transform.position += new Vector3(Speed * Time.deltaTime, 0, 0);
		} else
		{
			Game.Me.ActualScene = 0;
			Game.Me.PrepareNewScene("Koniec i bomba, kto zagrał ten trąba");
		}
	}

	internal void Restart(int p)
	{
		gameObject.transform.position = new Vector3(MinX, transform.position.y, transform.position.z);
		Speed = (MaxX - MinX) / p;
	}
}
