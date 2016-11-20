using UnityEngine;
using System.Collections;

public class PlayerAnimation {
	public static void Animate (Animator playerAnimator) {
		// Movement animation.
		if (Input.GetKey(KeyCode.D) || Input.GetKey (KeyCode.A) ){
			playerAnimator.SetBool ("PlayerIdle", false);
		} else {
			playerAnimator.SetBool ("PlayerIdle", true);
		}
	}
}
