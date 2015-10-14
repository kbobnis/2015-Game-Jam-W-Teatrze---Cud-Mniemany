using UnityEngine;
using System.Collections;

public class Glow : MonoBehaviour
{
	private float BaseIntensity;
	private bool IsUp = true;
	public float Speed = 2.5f;
	public float Delta = 1;

	void Start()
	{
		BaseIntensity = GetComponent<Light>().intensity;
	}

	void Update ()
	{
		float intensity = GetComponent<Light>().intensity;
		float delta = Speed * Time.deltaTime * (IsUp ? 1 : -1);
		
		 GetComponent<Light>().intensity += delta;

		 if (GetComponent<Light>().intensity > BaseIntensity + Delta/2)
		 {
			 IsUp = false;
		 }
		 if (GetComponent<Light>().intensity < BaseIntensity - Delta/2)
		 {
			 IsUp = true;
		 }
	}
}
