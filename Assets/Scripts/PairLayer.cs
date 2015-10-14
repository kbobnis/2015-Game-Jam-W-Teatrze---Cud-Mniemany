using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PairLayer : MonoBehaviour
{
	/**
	 * @returns how many enemies were prepared
	 */
	internal int Prepare(List<Enemy> lefts, List<Enemy> rights, int enemiesToPrepare, List<Word> words)
	{
		int i = 0;
		int created = 0;
		foreach (Pair pair in gameObject.GetComponentsInChildren<Pair>())
		{
			pair.gameObject.SetActive(false);

			if (enemiesToPrepare > i)
			{
				created++;
				pair.gameObject.SetActive(true);

				Enemy left = lefts[UnityEngine.Random.Range(0, lefts.Count-1)];
				Enemy right = rights[UnityEngine.Random.Range(0, rights.Count-1)];
				Word word = words[UnityEngine.Random.Range(0, words.Count - 1)];

				pair.Prepare(left, right, word);
			}
			i++;
		}
		return created;
	}

	internal void SendLetter(string p)
	{
		foreach (Pair pair in gameObject.GetComponentsInChildren<Pair>())
		{
			if (pair.gameObject.activeSelf)
			{
				pair.SendLetter(p);
			}
		}
	}
}
