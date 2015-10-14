using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Game : MonoBehaviour
{
	public static Game Me;

	public ElementLayer[] ElementLayers;
	public PairLayer[] PairLayers;
	public SpriteRenderer LandscapeRenderer;
	public PapaMover PapaMover;
	public GameObject Curtain;
	private bool GameStarted;
	private bool CanAnimCurtains;
	private GameModel GameModel;
	private int ActualScene;
	public Canvas Canvas;
	private bool Preparing = false;

	void Start ()
	{
		Me = this;
		XMLLoader xmlLoader = new XMLLoader();
		GameModel = xmlLoader.LoadGame(Resources.Load<TextAsset>("model").text);
		Debug.Log("loaded " + GameModel.Scenes.Count + " scenes");
		PrepareNewScene();
	}

	private void PrepareNewScene()
	{
		Preparing = true;

		PapaMover.Restart(GameModel.Scenes[ActualScene].Time);
		PapaMover.gameObject.transform.GetChild(0).gameObject.SetActive(false);
		PapaMover.enabled = false;

		GetComponent<CameraController>().Restart(GameModel.Scenes[ActualScene].Time);
		GetComponent<CameraController>().Started = false;
		
		Canvas.gameObject.SetActive(true);
		
		Scene scene = GameModel.Scenes[ActualScene];
		Debug.Log("Preparing new scene: " + ActualScene + ", enemies count: " + scene.EnemiesCount);
		CanAnimCurtains = false;
		GameStarted = false;

		Vector3 pos = Curtain.transform.GetChild(0).localPosition;
		Curtain.transform.GetChild(0).localPosition = new Vector3( 0, 0, pos.z);
		Curtain.transform.GetChild(1).localPosition = new Vector3(0, 0, pos.z);
		foreach (ElementLayer el in ElementLayers)
		{
			el.gameObject.SetActive(false);
			if (scene.Layers.ContainsKey(el.LayerType))
			{
				el.gameObject.SetActive(true);
				el.Prepare(scene.Layers[el.LayerType]);
			} 
		}

		int enemiesCount = scene.EnemiesCount;
		foreach (PairLayer pr in PairLayers)
		{
			enemiesCount -= pr.Prepare(scene.EnemiesParty1, scene.EnemiesParty2, enemiesCount, scene.Words);
		}
		LandscapeRenderer.sprite = scene.Landscape;
		Debug.Log("new scene prepared");
		Preparing = false;
	}

	void Update()
	{
		if (Preparing)
		{
			return;
		}


		if (CanAnimCurtains)
		{
			AnimCurtains();
		}

		if (Input.GetKeyDown (KeyCode.Space) && !GameStarted)
		{
			CanAnimCurtains=true;
		}

		if (Input.anyKeyDown && (string)Input.inputString != "" && GameStarted)
		{
			bool anyPairsLeft = false;
			foreach (PairLayer pr in PairLayers)
			{
				pr.SendLetter((string)Input.inputString);
				if (pr.AnyPairsLeft())
				{
					anyPairsLeft = true;
				}
			}
			if (anyPairsLeft == false)
			{
				//end game
				NewScene();
			}
		}

	}

	private void NewScene()
	{
		ActualScene++;
		if (ActualScene >= GameModel.Scenes.Count)
		{
			ActualScene = 0;
		}
		PrepareNewScene();
	}

	private void AnimCurtains(){
		Canvas.gameObject.SetActive (false);

		if (Curtain.transform.GetChild(0).localPosition.x > -4.1f)
		{
			Curtain.transform.GetChild(0).transform.position -= new Vector3(0.1f, 0, 0);
		}
		if (Curtain.transform.GetChild(1).localPosition.x < 4.1f)
		{
			Curtain.transform.GetChild(1).transform.position += new Vector3(0.1f, 0, 0);
		} else
		{
			StartGame();
		}

	}

	private void StartGame()
	{
		PapaMover.enabled = true;
		PapaMover.gameObject.transform.GetChild (0).gameObject.SetActive (true);
		CanAnimCurtains = false;
		PapaMover.Restart(GameModel.Scenes[ActualScene].Time);
		GetComponent<CameraController>().Started = true;
		GameStarted = true;
	}
	
}
