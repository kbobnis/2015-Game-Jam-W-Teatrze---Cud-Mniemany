using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	private Sprite Animation;
	private Sprite SuccessAnimation;
	private AudioClip SuccessSound;

	public Player(Sprite playerAnimation, Sprite playerSuccessAnimation, AudioClip playerSuccessSound) {
		Animation = playerAnimation;
		SuccessAnimation = playerSuccessAnimation;
		SuccessSound = playerSuccessSound;
	}

}
