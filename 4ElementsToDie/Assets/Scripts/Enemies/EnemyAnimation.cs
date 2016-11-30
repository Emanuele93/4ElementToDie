using UnityEngine;
using System.Collections;

public class EnemyAnimation {
	public static void Animate (Animator enemyAnimator, bool chasePlayer) {
		// Movement animation.
		if (chasePlayer) {
			enemyAnimator.SetFloat ("Speed", 1);
		} else {
			enemyAnimator.SetFloat ("Speed", 0);
		}
	}
}
