using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameModel : MonoBehaviour {
	private Player Player;
	private List<Scene> Scenes;

	public GameModel(Player player, List<Scene> scenes)
	{
		Player = player;
		Scenes = scenes;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
