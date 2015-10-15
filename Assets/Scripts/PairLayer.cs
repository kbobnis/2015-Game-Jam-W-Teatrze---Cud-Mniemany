using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PairLayer : MonoBehaviour
{
	/**
	 * @returns how many enemies were prepared
	 */
	internal int Prepare(int enemiesToPrepare, List<Word> words)
	{
		int i = 0;
		int created = 0;
		foreach (Pair pair in gameObject.GetComponentsInChildren<Pair>(true))
		{
			pair.gameObject.SetActive(false);

			if (enemiesToPrepare > i)
			{
				created++;
				pair.gameObject.SetActive(true);

				Word word = words[UnityEngine.Random.Range(0, words.Count - 1)];

				pair.Prepare(word);
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

	internal bool AnyPairsLeft()
	{
		foreach (Pair pair in gameObject.GetComponentsInChildren<Pair>())
		{
			if (!pair.GoDown)
			{
				return true;
			}
		}
		return false;
	}
}
