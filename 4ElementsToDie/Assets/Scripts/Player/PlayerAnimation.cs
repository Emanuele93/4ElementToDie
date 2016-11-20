using UnityEngine;
using System.Collections;

public class PlayerAnimation {
	public static void Animate (Animator playerAnimator) {
		// Movement animation.
		if (Input.GetKeyDown (KeyCode.W) ){
			playerAnimator.SetInteger ("DirectionVertical",1);
		}

		if (Input.GetKeyDown (KeyCode.S) ){
			playerAnimator.SetInteger ("DirectionVertical",2);
		}

		if (Input.GetKeyDown (KeyCode.D) ){
			playerAnimator.SetInteger ("DirectionHorizontal",3);
		}

		if (Input.GetKeyDown (KeyCode.A) ){
			playerAnimator.SetInteger ("DirectionHorizontal",4);
		}
	}
}
