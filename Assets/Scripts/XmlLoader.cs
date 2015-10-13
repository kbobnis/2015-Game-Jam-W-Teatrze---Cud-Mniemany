using UnityEngine;
using System.Xml;
using System.Collections;
using System.Collections.Generic;
using System;

public class XMLLoader
{

	public GameModel LoadGame(string xml)
	{
		XmlDocument xmlDoc = new XmlDocument();
		xmlDoc.LoadXml(xml);

		XmlNode scenesXml = xmlDoc.GetElementsByTagName("scenes").Item(0);

		Player player = LoadPlayer(scenesXml);
		
		List<Scene> scenes = new List<Scene>();
		foreach (XmlNode sceneXml in xmlDoc.GetElementsByTagName("scene"))
		{
			scenes.Add(LoadScene(sceneXml));
		}
		return new GameModel(player, scenes);
	}

	private Scene LoadScene(XmlNode sceneXml)
	{
		int time = int.Parse(sceneXml.Attributes["time"].Value);
		int enemiesCount = int.Parse(sceneXml.Attributes["enemies"].Value);
		string pathPrefix = sceneXml.Attributes["pathPrefix"].Value;
		
		List<Layer> layers = null;
		List<Enemy> enemiesParty1 = null;
		List<Enemy> enemiesParty2 = null;
		List<Word> words = null;
		foreach (XmlNode sceneNodeXml in sceneXml.ChildNodes)
		{
			switch (sceneNodeXml.Name) {
				case "layers":
					layers = new List<Layer>();
					foreach (XmlNode layerXml in sceneNodeXml.ChildNodes)
					{
						layers.Add(LoadLayer(layerXml, pathPrefix));
					}
					break;
				case "enemies":
					foreach (XmlNode partyXml in sceneNodeXml.ChildNodes)
					{
						switch(partyXml.Name)
						{
							case "party1":
								enemiesParty1 = LoadEnemies(partyXml.ChildNodes, pathPrefix);
								break;
							case "party2":
								enemiesParty2 = LoadEnemies(partyXml.ChildNodes, pathPrefix);
								break;
							default:
								throw new Exception("There should be parties only party1 and party2, but found: " + partyXml.Name);
						}
					}
					break;
				case "words":
					words = new List<Word>();
					foreach (XmlNode wordXml in sceneNodeXml.ChildNodes)
					{
						words.Add(LoadWord(wordXml));
					}
				break;
				default:
				throw new Exception("Scene node child not recognized: " + sceneNodeXml.Name);
			}
		}
		return new Scene(time, enemiesCount, layers, enemiesParty1, enemiesParty2, words);
	}

	private Word LoadWord(XmlNode wordXml)
	{
		return new Word(wordXml.Attributes["text"].Value);
	}

	private List<Enemy> LoadEnemies(XmlNodeList enemiesXml, string pathPrefix)
	{
		List<Enemy> enemies = new List<Enemy>();
		foreach (XmlNode enemyXml in enemiesXml)
		{
			enemies.Add(LoadEnemy(enemyXml, pathPrefix));
		}
		return enemies;
	}

	private Enemy LoadEnemy(XmlNode enemyXml, string pathPrefix)
	{
		Sprite[] anim = Resources.LoadAll<Sprite>(pathPrefix + enemyXml.Attributes["anim"].Value);
		Sprite[] reconciliationAnim = Resources.LoadAll<Sprite>(pathPrefix + enemyXml.Attributes["reconciliationAnim"].Value);
		Sprite[] deathAnim = Resources.LoadAll<Sprite>(pathPrefix + enemyXml.Attributes["deathAnim"].Value);
		return new Enemy(anim, reconciliationAnim, deathAnim);
	}

	private Player LoadPlayer(XmlNode playerXml)
	{
		Sprite playerAnimation = Resources.Load<Sprite>(playerXml.Attributes["walkAnim"].Value);
		Sprite playerSuccessAnimation = Resources.Load<Sprite>(playerXml.Attributes["successAnim"].Value);
		AudioClip playerSuccessSound = Resources.Load<AudioClip>(playerXml.Attributes["successSound"].Value);

		return new Player(playerAnimation, playerSuccessAnimation, playerSuccessSound);
	}

	private Layer LoadLayer(XmlNode layerXml, string pathPrefix)
	{
		Sprite[] elements = Resources.LoadAll<Sprite>(pathPrefix + layerXml.Attributes["path"].Value);
		return new Layer(elements);
	}
	

}