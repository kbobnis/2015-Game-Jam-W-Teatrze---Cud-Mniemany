using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Game : MonoBehaviour
{
	public ElementLayer[] ElementLayers;
	public PairLayer PairLayer1, PairLayer2, PairLayer3;
	public MeshRenderer LandscapeRenderer;

	void Start ()
	{
		XMLLoader xmlLoader = new XMLLoader();
		
		GameModel gameModel = xmlLoader.LoadGame(Resources.Load<TextAsset>("model").text);

		Scene scene = gameModel.Scenes[0];

		foreach (ElementLayer el in ElementLayers)
		{
			el.Prepare(scene.Layers[el.LayerType]);
		}

		int enemiesCount = scene.EnemiesCount;

		enemiesCount -= PairLayer1.Prepare(scene.EnemiesParty1, scene.EnemiesParty2, enemiesCount);
		enemiesCount -= PairLayer2.Prepare(scene.EnemiesParty1, scene.EnemiesParty2, enemiesCount);
		enemiesCount -= PairLayer3.Prepare(scene.EnemiesParty1, scene.EnemiesParty2, enemiesCount);
	}

	
}
