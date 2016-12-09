using UnityEngine;
using System.Collections;

public class PlayerAnimation {
	public static void Move (Animator playerAnimator) {
		// Movement animation.
		if (Input.GetKey(KeyCode.D) || Input.GetKey (KeyCode.A) || Input.GetKey(KeyCode.W) || Input.GetKey (KeyCode.S)){
			playerAnimator.SetBool ("PlayerIdle", false);
		} else {
			playerAnimator.SetBool ("PlayerIdle", true);
		}
	}

	public static void Dead (Animator playerAnimator, bool isDead) {
		playerAnimator.SetBool ("PlayerDead", isDead);
	}
}
