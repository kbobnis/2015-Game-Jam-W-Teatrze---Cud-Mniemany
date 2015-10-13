using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Game : MonoBehaviour
{

	void Start ()
	{
		XMLLoader xmlLoader = new XMLLoader();
		
		GameModel gameModel = xmlLoader.LoadGame(Resources.Load<TextAsset>("model").text);

	}
	
}
