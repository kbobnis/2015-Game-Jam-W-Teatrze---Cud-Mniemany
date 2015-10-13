using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Game : MonoBehaviour
{

	void Start ()
	{
		XMLLoader xmlLoader = new XMLLoader();
		
		List<Scene> scenes = xmlLoader.LoadScenes(Resources.Load<TextAsset>("model").text);
	}
	
}
