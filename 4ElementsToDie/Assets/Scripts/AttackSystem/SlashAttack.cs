using UnityEngine;
using System.Collections;

[RequireComponent (typeof (PolygonCollider2D),typeof(Rigidbody2D), typeof(SpriteRenderer))]
public class SlashAttack : Attack {
	PolygonCollider2D col;
	const int numberOfPoints = 16;

	public Sprite attackSprite;

	protected override void Start () {
		base.Start ();

		col = GetComponent<PolygonCollider2D> () as PolygonCollider2D;

		//col.SetPath (0,setColliderPoints ());
		col.isTrigger = true;

		//sr.sprite = attackSprite;

	}

	public override void AttackNow()
	{
		
		base.AttackNow();
		transform.localScale = new Vector3 (attRange, attRange, 0);
		//col.SetPath (0, setColliderPoints ());
	}
		
	Vector2[] setColliderPoints() {
        // Thanks to my friend salim :D.

        // Creating the points for the angle.
        Vector2[] points = new Vector2[numberOfPoints];
		int attackAngle = 30;
		float step = (attackAngle * 2) / ((float)numberOfPoints / 2f - 1);
		float rad; float a; float r;

		// Outer circle creation. 
		a = attackAngle;
        r = attRange;
		for (int i = 0; i < numberOfPoints / 2; i++) {
			rad = a * Mathf.PI / 180f;
			points [i] = new Vector2 (Mathf.Cos (rad) * r, Mathf.Sin (rad) * r);
			a -= step;
		}

		// Inner-Circle creation.
		a = -attackAngle;
		r = 1f;
		for (int i = numberOfPoints/2; i < numberOfPoints; i++) {
			rad = a * Mathf.PI / 180f;
			points [i] = new Vector2 (Mathf.Cos (rad) * r, Mathf.Sin (rad) * r);
			a += step;
		}

		return points;
	}
}
