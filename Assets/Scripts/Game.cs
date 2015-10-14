using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Game : MonoBehaviour
{
	public static Game Me;

	public ElementLayer[] ElementLayers;
	public PairLayer PairLayer1, PairLayer2, PairLayer3;
	public MeshRenderer LandscapeRenderer;
	public PapaMover PapaMover;

	void Start ()
	{
		Me = this;
		XMLLoader xmlLoader = new XMLLoader();
		GameModel gameModel = xmlLoader.LoadGame(Resources.Load<TextAsset>("model").text);
		Scene scene = gameModel.Scenes[0];
		foreach (ElementLayer el in ElementLayers)
		{
			el.Prepare(scene.Layers[el.LayerType]);
		}

		int enemiesCount = scene.EnemiesCount;
		enemiesCount -= PairLayer1.Prepare(scene.EnemiesParty1, scene.EnemiesParty2, enemiesCount, scene.Words);
		enemiesCount -= PairLayer2.Prepare(scene.EnemiesParty1, scene.EnemiesParty2, enemiesCount, scene.Words);
		enemiesCount -= PairLayer3.Prepare(scene.EnemiesParty1, scene.EnemiesParty2, enemiesCount, scene.Words);
		GetComponent<CameraController>().Restart(scene.Time);
		PapaMover.Restart(scene.Time);
	}

	void Update()
	{

		if (Input.anyKeyDown && (string)Input.inputString != "")
		{
			PairLayer1.SendLetter((string)Input.inputString);
			PairLayer2.SendLetter((string)Input.inputString);
			PairLayer3.SendLetter((string)Input.inputString);
		}
	}
	
}
