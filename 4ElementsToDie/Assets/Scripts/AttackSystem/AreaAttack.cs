using UnityEngine;
using System.Collections;

[RequireComponent (typeof(CircleCollider2D), typeof(SpriteRenderer))]
public class AreaAttack : Attack {
	CircleCollider2D col;
	float colliderRadius = 3f;

	public Sprite attackSprite;

	// Use this for initialization
	protected override void Start ()
    {
		base.Start ();

		col = GetComponent<CircleCollider2D> () as CircleCollider2D;

		col.radius = colliderRadius;
		col.isTrigger = true;

		sr.sprite = attackSprite;
	}
    
    public override void AttackNow()
    {
        base.AttackNow();

        col.radius = attRange;
    }
}
