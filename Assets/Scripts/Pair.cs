using UnityEngine;
using System.Collections;

public class Pair : MonoBehaviour
{
	private int ActualIndex = 0;
	private string Text;
	public bool GoDown = false;

	internal void Prepare(Word word)
	{
		Vector3 v = gameObject.transform.localPosition;
		gameObject.transform.localPosition = new Vector3(v.x, 0, v.z);

		GoDown = false;
		ActualIndex = 0;

		GameObject go = gameObject.transform.GetChild(2).gameObject;
		go.transform.position = transform.TransformPoint(new Vector3(-4.5f, 0.5f));;
		gameObject.transform.GetChild(2).gameObject.SetActive(true);
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
				Game.Me.GetComponent<Game>().PapaMover.gameObject.GetComponent<AudioSource>().PlayOneShot(Camera.main.GetComponent<Game>().PapaSound,1);
				Game.Me.GetComponent<Game>().AudioSources[0].volume-=(1.0f/Camera.main.GetComponent<Game>().PairsOnScene);
				Game.Me.GetComponent<Game>().AudioSources[1].volume-=(1.0f/Camera.main.GetComponent<Game>().PairsOnScene);

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
		if (GoDown == false)
		{
			GameObject go = gameObject.transform.GetChild(2).gameObject;
			go.GetComponent<TextMesh>().text = "<color=\"red\">" + Text.Substring(0, ActualIndex) + "</color>" + Text.Substring(ActualIndex);
		}
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
