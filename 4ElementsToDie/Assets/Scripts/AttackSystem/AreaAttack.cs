using UnityEngine;
using System.Collections;

[RequireComponent (typeof(CircleCollider2D), typeof(SpriteRenderer))]
public class AreaAttack : Attack {

	CircleCollider2D col;

	float baseRadius = 2.5f;

	public Sprite attackSprite;

	// Use this for initialization
	protected override void Start ()
    {
		base.Start ();

		col = GetComponent<CircleCollider2D> () as CircleCollider2D;

		col.radius = baseRadius;
		col.isTrigger = true;

		sr.sprite = attackSprite;
	}
    
    public override void AttackNow()
    {
        base.AttackNow();

        col.radius = baseRadius * attRange;
    }
}
