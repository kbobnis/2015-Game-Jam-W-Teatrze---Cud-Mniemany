﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Scene
{
	private int Time;
	private int EnemiesCount;
	private Player Player;
	private List<Layer> Layers;
	private List<Enemy> EnemiesParty1;
	private List<Enemy> EnemiesParty2;
	private List<Word> Words;

	public Scene(int time, int enemiesCount, Player player, System.Collections.Generic.List<Layer> layers, System.Collections.Generic.List<Enemy> enemiesParty1, System.Collections.Generic.List<Enemy> enemiesParty2, System.Collections.Generic.List<Word> words)
	{
		Time = time;
		EnemiesCount = enemiesCount;
		Player = player;
		Layers = layers;
		EnemiesParty1 = enemiesParty1;
		EnemiesParty2 = enemiesParty2;
		Words = words;
		Debug.Log("Created scene with time: " + time + ", enemies count: " + enemiesCount);
	}

}
