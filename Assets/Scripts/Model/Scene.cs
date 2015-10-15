using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Scene
{
	public int Time;
	public int EnemiesCount;
	public Dictionary<LayerType, Layer> Layers;
	public Sprite Landscape;
	public List<Word> Words;

	public Scene(int time, int enemiesCount, Sprite landscape, Dictionary<LayerType, Layer> layers, List<Word> words)
	{
		Time = time;
		EnemiesCount = enemiesCount;
		Landscape = landscape;
		Layers = layers;
		Words = words;
	}
}
