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
		Sprite landscape = Resources.Load<Sprite>(pathPrefix + sceneXml.Attributes["landscapePath"].Value);

		Dictionary<LayerType, Layer> layers = new Dictionary<LayerType, Layer>();
		List<Word> words = null;
		foreach (XmlNode sceneNodeXml in sceneXml.ChildNodes)
		{
			switch (sceneNodeXml.Name) {
				case "layers":
					foreach (XmlNode layerXml in sceneNodeXml.ChildNodes)
					{
						string name = layerXml.Attributes["id"].Value;
						LayerType lt = LayerType.BackElements;
						switch (name)
						{
							case "wsporniki":
								lt = LayerType.Wsporniki;
								break;
							case "backLayer":
								lt = LayerType.BackElements;
								break;
							case "wspornikiPrzod":
								lt = LayerType.WspornikiPrzod;
								break;
							default:
								throw new System.Exception("THere is no layer tape mapping for " + name);
						}

						layers.Add(lt, LoadLayer(layerXml, pathPrefix));
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
		return new Scene(time, enemiesCount, landscape, layers, words);
	}

	private Word LoadWord(XmlNode wordXml)
	{
		return new Word(wordXml.Attributes["text"].Value);
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
		string name = layerXml.Attributes["id"].Value;
		switch (name)
		{
			case Layer.LayerBackLayer:
			case Layer.LayerWsporniki:
				break;
			case Layer.LayerWspornikiPrzod:
				break;
			default:
				throw new System.Exception("There can be layers named wsporniki, frontLayers, backLayer. Found: " + name);
		}
		List<Sprite> images = new List<Sprite>();
		foreach (XmlNode imageXml in layerXml.ChildNodes)
		{
			images.Add(Resources.Load<Sprite>(pathPrefix + imageXml.Attributes["path"].Value));
		}

		return new Layer(name, images);
	}
	

}