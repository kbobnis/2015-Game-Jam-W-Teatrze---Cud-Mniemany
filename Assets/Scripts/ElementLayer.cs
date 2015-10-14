using UnityEngine;
using System.Collections;

public class ElementLayer : MonoBehaviour
{
	public LayerType LayerType;

	internal void Prepare(Layer layer)
	{
		foreach (SpriteRenderer element in gameObject.GetComponentsInChildren<SpriteRenderer>())
		{
			element.sprite = layer.GetRandomSprite();
		}
	}
}

public enum LayerType
{
	Wsporniki, BackElements
}
