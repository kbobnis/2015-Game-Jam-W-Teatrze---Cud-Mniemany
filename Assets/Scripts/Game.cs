using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Game : MonoBehaviour
{
	public ElementLayer[] ElementLayers;
	public PairLayer PairLayer1, PairLayer2, PairLayer3;
	public MeshRenderer LandscapeRenderer;
	public PapaMover PapaMover;
	public GameObject Curtain;
	private bool GameStarted;
	private bool CanAnimCurtains;
	private Scene Scene;
	public Canvas Canvas;

	void Start ()
	{
		XMLLoader xmlLoader = new XMLLoader();
		
		GameModel gameModel = xmlLoader.LoadGame(Resources.Load<TextAsset>("model").text);

		Scene = gameModel.Scenes[0];

		foreach (ElementLayer el in ElementLayers)
		{
			el.Prepare(Scene.Layers[el.LayerType]);
		}

		int enemiesCount = Scene.EnemiesCount;

		enemiesCount -= PairLayer1.Prepare(Scene.EnemiesParty1, Scene.EnemiesParty2, enemiesCount, Scene.Words);
		enemiesCount -= PairLayer2.Prepare(Scene.EnemiesParty1, Scene.EnemiesParty2, enemiesCount, Scene.Words);
		enemiesCount -= PairLayer3.Prepare(Scene.EnemiesParty1, Scene.EnemiesParty2, enemiesCount, Scene.Words);


	}

	void Update()
	{
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
			PairLayer1.SendLetter((string)Input.inputString);
			PairLayer2.SendLetter((string)Input.inputString);
			PairLayer3.SendLetter((string)Input.inputString);
		}
	}

	private void AnimCurtains(){
		Canvas.gameObject.SetActive (false);

		if(Curtain.transform.GetChild(0).localPosition.x>-4.0f) Curtain.transform.GetChild (0).transform.position -= new Vector3 (0.1f, 0, 0);
		if (Curtain.transform.GetChild (1).localPosition.x < 4.0f)
			Curtain.transform.GetChild (1).transform.position += new Vector3 (0.1f, 0, 0);
		else 
		{
			StartGame ();
		}

	}

	private void StartGame()
	{

		PapaMover.gameObject.transform.GetChild (0).gameObject.SetActive (true);
		CanAnimCurtains = false;
		GetComponent<CameraController>().Restart(Scene.Time);
		PapaMover.Restart(Scene.Time);
		GameStarted = true;

	}
	
}
