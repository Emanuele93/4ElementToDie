using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent (typeof(Rigidbody2D))]
public  class Attack : MonoBehaviour {
	
	protected Rigidbody2D rb;
	protected Transform tr;

	protected float waitTime = 3f;

	protected CharacterManager attacker;

	protected virtual void Start() {
		tr = GetComponent<Transform> () as Transform;
		rb = GetComponent<Rigidbody2D> () as Rigidbody2D;
	}

	void FixedUpdate() {
		AttackNow ();
	}

	// the attack method is called when the user presses the attack button.
	public virtual void AttackNow() {
		StartCoroutine( ExplosionTime ());
	}

	protected IEnumerator ExplosionTime () {
		yield return new WaitForSeconds (waitTime);
		gameObject.SetActive (false);
	}

	// virtual in order to be overriden if necessary.
	protected virtual void OnTriggerEnter2D (Collider2D other) {
		attacker = GameplayManager.Instance.attackersDict [gameObject.GetInstanceID ()];
		Debug.Log (gameObject.tag + " - " + attacker.ToString ());
		if (gameObject.tag == "FromPlayer" && other.tag == "Enemy") {
			
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
		} else if (gameObject.tag == "FromEnemy" && other.tag == "Player"){ 
			Debug.LogError ("Player");

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
		} 
	}
}
