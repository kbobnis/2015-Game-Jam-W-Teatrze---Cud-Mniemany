using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Game : MonoBehaviour
{
	public GameObject SpectatorsRow1, SpectatorsRow2, SpectatorsRow3;
	public GameObject FrontLayer1, FrontLayer2, FrontLayer3;
	public GameObject WspornikiLayer, BackElementsLayer;
	public MeshRenderer LandscapeRenderer;
	public GameObject PairPrefab;

	void Start ()
	{
		XMLLoader xmlLoader = new XMLLoader();
		
		GameModel gameModel = xmlLoader.LoadGame(Resources.Load<TextAsset>("model").text);
	}

	void InsertPair(GameObject pair)
	{
		throw new System.Exception("Land this pair in one of the layers");
	}
	
}
