using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Game : MonoBehaviour
{
	private bool GameStarted;
	private bool Preparing = false;

	public static Game Me;
	public ElementLayer[] ElementLayers;
	public PairLayer[] PairLayers;
	public SpriteRenderer LandscapeRenderer;
	public GameObject Curtain;
	public GameModel GameModel;
	public int ActualScene;
	public Canvas Canvas;
	public int PairsOnScene=0;
	public AudioClip PapaSound;
	public AudioSource[] AudioSources;
	public GameObject PapaGameObject;
	public GameObject MiddleOfScreen;

	void Start ()
	{
		Me = this;
		XMLLoader xmlLoader = new XMLLoader();
		GameModel = xmlLoader.LoadGame(Resources.Load<TextAsset>("model").text);
		Debug.Log("loaded " + GameModel.Scenes.Count + " scenes");

		AudioSources = Camera.main.GetComponents<AudioSource> ();
		PrepareNewScene("Cud mniemany");
	}

	public void PrepareNewScene(string text="")
	{
		Preparing = true;
		AudioSources[0].volume = 1;
		AudioSources [1].volume = 1;
		AudioSources[0].Stop ();
		AudioSources[1].Stop ();

		Destroy(GetComponent<Mover>());
		Destroy(PapaGameObject.GetComponent<Mover>());

		gameObject.AddComponent<RollBackToStart>().Prepare(-3.3f);
		PapaGameObject.AddComponent<RollBackToStart>().Prepare(-3.346683f);
		
		Canvas.gameObject.SetActive(true);
		Canvas.gameObject.transform.GetChild(1).GetComponent<Text>().text = text;
		
		Scene scene = GameModel.Scenes[ActualScene];
		PairsOnScene = scene.EnemiesCount;

		Canvas.gameObject.SetActive(true);
		Curtain.AddComponent<AnimCurtains>().Prepare(Type.Close);

		GameStarted = false;

		foreach (ElementLayer el in ElementLayers)
		{
			el.gameObject.SetActive(false);
			if (scene.Layers.ContainsKey(el.LayerType))
			{
				el.gameObject.SetActive(true);
				el.Prepare(scene.Layers[el.LayerType]);
			}
		}

		LandscapeRenderer.sprite = scene.Landscape;

		Preparing = false;
		Debug.Log("new scene prepared");
	}

	void Update()
	{
		if (Preparing)
		{
			return;
		}

		if (Input.GetKeyDown (KeyCode.Space) && !GameStarted)
		{
			StartGame();
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
		PrepareNewScene("Kolejny akt");
	}

	private void StartGame()
	{
		Debug.Log("Start game");
		Scene scene = GameModel.Scenes[ActualScene];
		int enemiesCount = scene.EnemiesCount;
		foreach (PairLayer pr in PairLayers)
		{
			enemiesCount -= pr.Prepare(enemiesCount, scene.Words);
		}

		AudioSources[0].Play();
		AudioSources[1].Play();
		if (Curtain.GetComponent<AnimCurtains>() != null)
		{
			Curtain.GetComponent<AnimCurtains>().Prepare(Type.Open);
		} else
		{
			Curtain.AddComponent<AnimCurtains>().Prepare(Type.Open);
		}
		
		Mover cc = gameObject.AddComponent<Mover>();
		cc.Prepare(GameModel.Scenes[ActualScene].Time);

		PapaGameObject.AddComponent<Mover>().Prepare(GameModel.Scenes[ActualScene].Time);

		Canvas.gameObject.SetActive(false);

		GameStarted = true;
	}
}
