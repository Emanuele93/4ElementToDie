using UnityEngine;
using System.Collections;

public class EnemyAnimation {
	public static void Animate (Animator enemyAnimator, bool chasePlayer, bool enemyIsFacingRight, bool enemyIsFacingUp, bool lookingHorizontal ) {
		if (chasePlayer) { // TODO: Remove this IF when we change the enemy animators in all the enemy assets.
			//TODO: Refactor.
			if (!chasePlayer) { // Idle state
				enemyAnimator.SetBool ("EnemyIdle", true);
				enemyAnimator.SetBool ("EnemyUp", false);
			}  else if (chasePlayer && enemyIsFacingUp && !lookingHorizontal) { // Chasing the player looking up.
				enemyAnimator.SetBool ("EnemyIdle", false);
				enemyAnimator.SetBool ("EnemyUp", true);
			} else if (chasePlayer && !enemyIsFacingUp && !lookingHorizontal) { // Chasing the player looking down.
				enemyAnimator.SetBool ("EnemyIdle", true); // TODO: Refactor this.
				enemyAnimator.SetBool ("EnemyUp", false); 
			} else { // Moving only horizontally
				enemyAnimator.SetBool ("EnemyIdle", false);
				enemyAnimator.SetBool ("EnemyUp",false);
			}
		}
	}
}
