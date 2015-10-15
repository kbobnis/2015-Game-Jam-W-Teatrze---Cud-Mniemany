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
	public int ActualScene;
	public Canvas Canvas;
	private bool Preparing = false;
	public int PairsOnScene=0;
	public AudioClip PapaSound;
	public AudioSource[] AudioSources;
	private bool newSceneInvoked;
	void Start ()
	{
		Me = this;
		XMLLoader xmlLoader = new XMLLoader();
		GameModel = xmlLoader.LoadGame(Resources.Load<TextAsset>("model").text);
		Debug.Log("loaded " + GameModel.Scenes.Count + " scenes");


		AudioSources = Camera.main.GetComponents<AudioSource> ();
		Debug.Log ("A" + AudioSources.Length);
		PrepareNewScene("Cud mniemany");
	}

	public void PrepareNewScene(string text="")
	{
		AudioSources[0].volume = 1;
		AudioSources [1].volume = 1;
		AudioSources[0].Stop ();
		AudioSources[1].Stop ();
		Preparing = true;

		PapaMover.Restart(GameModel.Scenes[ActualScene].Time);
		PapaMover.gameObject.transform.GetChild(0).gameObject.SetActive(false);
		PapaMover.enabled = false;

		GetComponent<CameraController>().Restart(GameModel.Scenes[ActualScene].Time);
		GetComponent<CameraController>().Started = false;
		
		Canvas.gameObject.SetActive(true);
		Canvas.gameObject.transform.GetChild(1).GetComponent<Text>().text = text;
		
		Scene scene = GameModel.Scenes[ActualScene];
		PairsOnScene = scene.EnemiesCount;
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
			AudioSources[0].Play ();
			AudioSources[1].Play ();
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
				if(!newSceneInvoked)
				{
					Invoke("NewScene",1);
					newSceneInvoked=true;
				}
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
		PrepareNewScene("Kolejny akt");
		newSceneInvoked = false;
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
