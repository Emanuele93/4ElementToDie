using UnityEngine;
using System.Collections;

[RequireComponent (typeof(CircleCollider2D))]
public class AreaAttack : Attack {
	CircleCollider2D col;
	float colliderRadius = 3f;

	// Use this for initialization
	protected override void Start ()
    {
		base.Start ();

		col = GetComponent<CircleCollider2D> () as CircleCollider2D;

		col.radius = colliderRadius;
		col.isTrigger = true;
	}
    
    public override void AttackNow()
    {
        base.AttackNow();

        col.radius = attRange;
    }
}
