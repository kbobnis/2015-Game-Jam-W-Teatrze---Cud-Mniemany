using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Scene
{
	public int Time;
	public int EnemiesCount;
	public Dictionary<LayerType, Layer> Layers;
	public Sprite Landscape;
	public List<Enemy> EnemiesParty1;
	public List<Enemy> EnemiesParty2;
	public List<Word> Words;

	public Scene(int time, int enemiesCount, Sprite landscape, Dictionary<LayerType, Layer> layers, List<Enemy> enemiesParty1, System.Collections.Generic.List<Enemy> enemiesParty2, System.Collections.Generic.List<Word> words)
	{
		Time = time;
		EnemiesCount = enemiesCount;
		Landscape = landscape;
		Layers = layers;
		EnemiesParty1 = enemiesParty1;
		EnemiesParty2 = enemiesParty2;
		Words = words;
	}
}
