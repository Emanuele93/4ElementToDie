using UnityEngine;
using System.Collections;

public class PlayerMovement {

	public static bool[] captureMovement(Transform playerTr, float playerSpeed, bool playerFacingRight, bool playerFacingUp) {

		// Input capturing.
		float mHorizontalMov = Input.GetAxis ("HorizontalMov");
		float mVerticalMov = Input.GetAxis ("VerticalMov");

		playerTr.position += Time.fixedDeltaTime * playerSpeed * ((playerTr.right * mHorizontalMov + playerTr.up * mVerticalMov).normalized);

		// Flip Player horizontally
		if ( (mHorizontalMov < 0) && (playerFacingRight) ){
			Vector3 ls = playerTr.localScale;

			ls.x = -1f * ls.x;
			playerTr.localScale = ls;
			playerFacingRight = !playerFacingRight;


		} else if ( (mHorizontalMov > 0) && (!playerFacingRight) ) {
			Vector3 ls = playerTr.localScale;

			ls.x = -1f * ls.x;
			playerTr.localScale = ls;
			playerFacingRight = !playerFacingRight;

		}

		// Flip Player vertically
		if ( (mVerticalMov < 0) && (playerFacingUp) ){
			playerFacingUp = !playerFacingUp;



		} else if ( (mVerticalMov > 0) && (!playerFacingUp) ) {
			playerFacingUp = !playerFacingUp;

		}
			
		return new bool[]{playerFacingRight,playerFacingUp};
	}
}
