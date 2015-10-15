using UnityEngine;
using System.Collections;

public class AnimCurtains : MonoBehaviour
{
	public Type CurtainAnimation;

	void Update ()
	{
		Vector3 delta = new Vector3( 2f*Time.deltaTime, 0, 0);

		if (CurtainAnimation == Type.Open)
		{
			if (transform.GetChild(0).localPosition.x > -4.1f)
			{
				transform.GetChild(0).transform.position -= delta ;
			}
			if (transform.GetChild(1).localPosition.x < 4.1f)
			{
				transform.GetChild(1).transform.position += delta ;
			} else
			{
				Destroy(this);
			}
		} else
		{
			if (transform.GetChild(0).localPosition.x < 0)
			{
				transform.GetChild(0).transform.position += delta;
			}
			if (transform.GetChild(1).localPosition.x > 0)
			{
				transform.GetChild(1).transform.position -= delta;
			} else
			{
				Destroy(this);
			}
		}
	}

	internal void Prepare(Type curtainAnimation)
	{
		CurtainAnimation = curtainAnimation;
	}
}

public enum Type
{
	Open, Close
}