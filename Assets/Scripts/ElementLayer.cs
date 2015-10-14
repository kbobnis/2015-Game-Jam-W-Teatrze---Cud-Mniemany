using UnityEngine;
using System.Collections;

public class ElementLayer : MonoBehaviour
{
	public LayerType LayerType;

	internal void Prepare(Layer layer)
	{
		foreach (SpriteRenderer element in gameObject.GetComponentsInChildren<SpriteRenderer>(true))
		{
			Vector3 pos = element.gameObject.transform.position;
			Destroy(element.GetComponent<BoxCollider>());
			element.gameObject.transform.position = new Vector3(pos.x, 1.5f, pos.z);
			element.sprite = layer.GetRandomSprite();
			element.gameObject.AddComponent<BoxCollider>();
		}
	}
}

public enum LayerType
{
	Wsporniki, BackElements, WspornikiPrzod
}
