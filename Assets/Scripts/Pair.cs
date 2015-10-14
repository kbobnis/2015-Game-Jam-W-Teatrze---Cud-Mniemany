using UnityEngine;
using System.Collections;

public class Pair : MonoBehaviour
{
	internal void Prepare(Enemy left, Enemy right)
	{
		gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = right.Anim;
		gameObject.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = left.Anim;
	}
}
