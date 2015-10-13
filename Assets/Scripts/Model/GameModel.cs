﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameModel
{
	private Player Player;
	private List<Scene> Scenes;

	public GameModel(Player player, List<Scene> scenes)
	{
		Player = player;
		Scenes = scenes;
	}

}
