using UnityEngine;
using System.Collections;

public class PapaLightAndEnd : MonoBehaviour
{
	private float MinX = -3.346683f;
	private float MaxX = 3.5f;
	private float Speed;

	void Update()
	{
		if (transform.position.x > MaxX)
		{
			Game.Me.ActualScene = 0;
			Game.Me.PrepareNewScene("Koniec i bomba, kto zagrał ten trąba");
		}
	}

	void Start()
	{
		gameObject.transform.GetChild(0).gameObject.SetActive(true);
	}
}
