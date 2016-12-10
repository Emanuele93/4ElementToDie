using UnityEngine;
using System.Collections;

[RequireComponent (typeof (PolygonCollider2D),typeof(Rigidbody2D))]
public class ThrustAttack : Attack {
	PolygonCollider2D col;
	const int numberOfPoints = 3;

	protected override void Start ()
    {
		base.Start ();

		col = GetComponent<PolygonCollider2D> () as PolygonCollider2D;

		col.SetPath (0,setColliderPoints ());
		col.isTrigger = true;
	}

    Vector2[] setColliderPoints() {
        // Thanks to my friend salim :D.
        
        // Creating the points for the angle.
        Vector2[] points = new Vector2[numberOfPoints];
		int attackAngle = 30;
		float rad; float a; float r;

		// Point.
		r = 2.5f * attRange;
		points [0] = new Vector2 (Mathf.Cos (0) * r, Mathf.Sin (0) * r);

		// Down point.
		a = attackAngle;
		r = 0.5f / attRange;
		rad = a * Mathf.PI / 180f;
		points [1] = new Vector2 (Mathf.Cos (rad) * r, Mathf.Sin (rad) * r);


		// Upper-Point.
		a = -attackAngle;
		r = 0.5f / attRange;
		rad = a * Mathf.PI / 180f;
		points [2] = new Vector2 (Mathf.Cos (rad) * r, Mathf.Sin (rad) * r);

		return points;
	}
}

