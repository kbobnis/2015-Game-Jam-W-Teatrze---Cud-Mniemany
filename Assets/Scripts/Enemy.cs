using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
	private Sprite[] Anim;
	private Sprite[] ReconciliationAnim;
	private Sprite[] DeathAnim;

	public Enemy(Sprite[] anim, Sprite[] reconciliationAnim, Sprite[] deathAnim)
	{
		Anim = anim;
		ReconciliationAnim = reconciliationAnim;
		DeathAnim = deathAnim;
	}

}
