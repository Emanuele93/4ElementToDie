using UnityEngine;
using System.Collections;

// PlayerScript requires the GameObject to have a Rigidbody component
[RequireComponent (typeof (BoxCollider2D), typeof (Rigidbody2D))]
public class RangeAttack : Attack {
	BoxCollider2D col;
	Vector2 colliderSize = new Vector2(2f,1f);

	float mSpeed;
	float direction;

	// Use this for initialization
	protected override void Start () {
		base.Start ();

		col = GetComponent<BoxCollider2D> () as BoxCollider2D;

		mSpeed = 5f;
		direction = 1f;
		col.size = colliderSize;
		col.isTrigger = true;
	}

	// the attack method is called when the user presses the attack button.
	public override void AttackNow() {
		tr.position += tr.right * Time.fixedDeltaTime * direction * mSpeed;
		StartCoroutine( ExplosionTime ());
	}
		
	// Triggered when a collision happens.
	protected override void OnTriggerEnter2D (Collider2D other) {
		if (other.tag == "Enemy") {
			Debug.LogError ("Enemy");
			//CharacterManager defender = other.GetComponent<CharacterManager> () as CharacterManager;
			//GameplayManager.Instance.SuccessfulAttack (attacker, defender);
			//
			//			
			//
			//			double damage = GameLogicManager.CalculateDamage (attacker, defender);
			//			defender.ApplyDamage(damage);
			//			if (GameLogicManager.IsDead(defender))
			//			{
			//				GameplayManager.Instance.Kill(defender);
			//			}

		} else if (other.tag == "Wall") {
			Debug.LogError ("Wall");
			gameObject.SetActive (false);
		} 
	}
}
