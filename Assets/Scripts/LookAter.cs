using UnityEngine;
using System.Collections;

public class LookAter : MonoBehaviour {

	public GameObject LookAtObject1, LookAtObject2;

	void Update ()
	{
		Vector3 center = ((LookAtObject2.transform.position - LookAtObject1.transform.position) / 2.0f) + LookAtObject1.transform.position;
		transform.LookAt(center);
	}

}
