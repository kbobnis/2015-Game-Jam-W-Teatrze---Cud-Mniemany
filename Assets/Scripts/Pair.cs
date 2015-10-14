using UnityEngine;
using System.Collections;

public class Pair : MonoBehaviour
{
	private int ActualIndex = 0;
	private string Text;
	private bool GoDown = false;

	internal void Prepare(Enemy left, Enemy right, Word word)
	{
		gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = right.Anim;
		gameObject.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = left.Anim;

		Vector3 pos = transform.TransformPoint(new Vector3(-4.5f, 0.5f));

		GameObject go = gameObject.transform.GetChild(2).gameObject;
		go.transform.position = pos;
		go.GetComponent<TextMesh>().richText = true;
		Text = word.Text;
		UpdateText();
	}

	internal void SendLetter(string p)
	{
		if (GoDown == false && Text.Substring(ActualIndex, 1) == p)
		{ 
			ActualIndex++;
			if (ActualIndex >= Text.Length)
			{
				Game.Me.PapaMover.GetComponent<Animator>().SetTrigger("Success");
				gameObject.transform.GetChild(2).gameObject.SetActive(false);
				GoDown = true;
			}
		} else
		{
			ActualIndex = 0;
		}

		UpdateText();
	}

	private void UpdateText()
	{
		GameObject go = gameObject.transform.GetChild(2).gameObject;
		go.GetComponent<TextMesh>().text = "<color=\"red\">"+Text.Substring(0, ActualIndex)+"</color>" + Text.Substring(ActualIndex);
	}

	void Update()
	{
		if (GoDown)
		{
			gameObject.transform.position -= new Vector3(0, 0.5f * Time.deltaTime, 0);
			if (gameObject.transform.position.y < -3)
			{
				gameObject.SetActive(false);
			}
		}
	}
}
