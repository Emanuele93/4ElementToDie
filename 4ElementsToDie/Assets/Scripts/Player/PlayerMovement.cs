using UnityEngine;
using System.Collections;

public class PlayerMovement {

	public static void captureMovement(Transform playerTr, int playerSpeed) {

		// Input capturing.
		float mHorizontalMov = Input.GetAxis ("HorizontalMov");
		float mVerticalMov = Input.GetAxis ("VerticalMov");

		// Moving the player.
		playerTr.position += 
			playerTr.right * Time.fixedDeltaTime * playerSpeed * mHorizontalMov +
			playerTr.up * Time.fixedDeltaTime * playerSpeed * mVerticalMov;
	
//		// Flip Player
//		if ( (Mathf.Abs (mHorizontalMov) < 0) && (playerFacingRight) ){
//			flipPlayer ();
//			playerFacingRight = !playerFacingRight;
//		} else if ( ( (Mathf.Abs (mHorizontalMov) > 0) && (!playerFacingRight) ) ) {
//			flipPlayer ();
//			playerFacingRight = !playerFacingRight;
//		}
//
//		return playerFacingRight;
	}

//	public static void flipPlayer() {
//		
//	}
}
