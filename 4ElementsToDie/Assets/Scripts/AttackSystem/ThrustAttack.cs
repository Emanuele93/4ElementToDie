using UnityEngine;
using System.Collections;

[RequireComponent (typeof (PolygonCollider2D),typeof(Rigidbody2D), typeof(SpriteRenderer))]
public class ThrustAttack : Attack {

	PolygonCollider2D col;

    public Sprite attackSprite;

	protected override void Start ()
	{
		base.Start ();

		col = GetComponent<PolygonCollider2D> () as PolygonCollider2D;

		col.isTrigger = true;

	}

	public override void AttackNow()
	{
		base.AttackNow();
	}

}

