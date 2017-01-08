using UnityEngine;
using System.Collections;

[RequireComponent (typeof(CircleCollider2D), typeof(SpriteRenderer))]
public class AreaAttack : Attack {
	CircleCollider2D col;
	float colliderRadius = 0f;

	public Sprite attackSprite;

	Vector3 initialScale = new Vector3 (0f,0f,0f);
	Vector3 updateScale = new Vector3 (0f,0f,0f);

	// Use this for initialization
	protected override void Start ()
    {
		base.Start ();
		sr.sprite = attackSprite;

		col = GetComponent<CircleCollider2D> () as CircleCollider2D;
		col.isTrigger = true;
	}

	void OnEnable() {
		sr.sprite = attackSprite;
		sr.transform.localScale = initialScale;
		updateScale.x = 0f;
		updateScale.y = 0f;
	}
    
    public override void AttackNow()
    {
        base.AttackNow();
		updateScale.x += (Time.fixedDeltaTime * attRange) / 50;
		updateScale.y += (Time.fixedDeltaTime * attRange) / 50;
		sr.transform.localScale += updateScale;

    }
}
