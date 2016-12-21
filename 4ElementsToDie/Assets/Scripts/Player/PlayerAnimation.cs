using UnityEngine;
using System.Collections;

public class PlayerAnimation {
	public static void Move (Animator playerAnimator) {
		// Movement animation.
		if (Input.GetKey(KeyCode.D) || Input.GetKey (KeyCode.A) ){
			playerAnimator.SetBool ("PlayerIdle", false);
		} else {
			playerAnimator.SetBool ("PlayerIdle", true);
		}

		if (Input.GetKey (KeyCode.W)) {
			playerAnimator.SetBool ("PlayerUp", true);
		} else if (Input.GetKey (KeyCode.S)) {
			playerAnimator.SetBool ("PlayerUp", false);
		} else {
			playerAnimator.SetBool ("PlayerUp", false);
		}
	}

	public static void Dead (Animator playerAnimator, bool isDead) {
		playerAnimator.SetBool ("PlayerDead", isDead);
	}
}
