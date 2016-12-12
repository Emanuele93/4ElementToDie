using UnityEngine;
using System.Collections;

public class EnemyMovement {

	public static bool[] Move(Transform enemyTr, float enemySpeed, bool enemyFacingRight, bool enemyFacingUp, Transform playerTr) {
		Vector3 chasingDirection = 
			(playerTr.position - enemyTr.position).normalized;

		// Moving the enemy.
		enemyTr.position += chasingDirection * enemySpeed * Time.fixedDeltaTime;
		
		// Flip enemy horizontally
		if ( (chasingDirection.x < 0) && (enemyFacingRight) ){
			Vector3 ls = enemyTr.localScale;

			ls.x = -1f * ls.x;
			enemyTr.localScale = ls;
			enemyFacingRight = !enemyFacingRight;


		} else if ( (chasingDirection.x > 0) && (!enemyFacingRight) ) {
			Vector3 ls = enemyTr.localScale;

			ls.x = -1f * ls.x;
			enemyTr.localScale = ls;
			enemyFacingRight = !enemyFacingRight;

		}

		// Flip enemy vertically
		if ( (chasingDirection.y < 0) && (enemyFacingUp) ){
			enemyFacingUp = !enemyFacingUp;



		} else if ( (chasingDirection.y > 0) && (!enemyFacingUp) ) {
			enemyFacingUp = !enemyFacingUp;

		}

		return new bool[]{enemyFacingRight,enemyFacingUp};
	}

	// This functions calculates if the user is inside the enemy vision radius. 
	// A circle is drawed with center enemy position and radio r.
	// The point to check is player position.
	public static bool calculateDistance(Transform enemyTr, Transform playerTr) {
		double center_x = enemyTr.position.x; double center_y = enemyTr.position.y; 
		double x = playerTr.position.x; double y = playerTr.position.y;
		double radius = Constants.MAX_EnemyAggroRange;
		return (System.Math.Pow(x - center_x,2) + System.Math.Pow(y - center_y,2) ) <= System.Math.Pow(radius,2);
	}
}
