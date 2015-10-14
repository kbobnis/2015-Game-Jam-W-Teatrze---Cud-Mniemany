using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Layer : MonoBehaviour
{
	public const string LayerWsporniki = "wsporniki";
	public const string LayerBackLayer = "backLayer";
	public const string LayerWspornikiPrzod = "wspornikiPrzod";
	private string Name;
	public List<Sprite> Sprites;

	public Layer(string name, List<Sprite> sprites)
	{
		Name = name;
		Sprites = sprites;
	}

	public Sprite GetRandomSprite()
	{
		return Sprites[UnityEngine.Random.Range(0, Sprites.Count - 1)];
	}
}
