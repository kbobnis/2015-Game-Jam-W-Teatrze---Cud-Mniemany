using UnityEngine;
using System.Collections;

public class cameraController : MonoBehaviour {
	public GameObject lookAtObject;
	public float time=20;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (time > 0) {
			transform.LookAt (lookAtObject.transform);
			transform.RotateAround (lookAtObject.transform.position, Vector3.up, -20 * Time.deltaTime);
			time-=Time.fixedDeltaTime;
		}
	}
}
