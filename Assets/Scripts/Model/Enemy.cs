using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
	public Sprite Anim;
	public Sprite ReconciliationAnim;
	public Sprite[] DeathAnim;

	public Enemy(Sprite anim, Sprite reconciliationAnim, Sprite[] deathAnim)
	{
		Anim = anim;
		ReconciliationAnim = reconciliationAnim;
		DeathAnim = deathAnim;
	}

}
