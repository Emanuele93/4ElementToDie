using UnityEngine;
using System.Collections;

[RequireComponent (typeof (CircleCollider2D))]
[RequireComponent (typeof (Rigidbody2D))]
public class AreaAttack : MonoBehaviour {
	CircleCollider2D col;
	Rigidbody2D rb;
	Transform tr;

	float mSpeed;
	float direction;

	float colliderRadius = 3f;
	float waitTime = 3f;

	// Use this for initialization
	void Start () {
		tr = GetComponent<Transform> () as Transform;
		col = GetComponent<CircleCollider2D> () as CircleCollider2D;
		rb = GetComponent<Rigidbody2D> () as Rigidbody2D;

		mSpeed = 5f;
		direction = 1f;
		col.radius = colliderRadius;
		col.isTrigger = true;
	}

	// the attack method is called when the user presses the attack button.
	void Update () {
	}

	void FixedUpdate () {
		Attack ();
	}

	// the attack method is called when the user presses the attack button.
	public void Attack() {
		StartCoroutine( ExplosionTime ());
	}

	IEnumerator ExplosionTime () {
		yield return new WaitForSeconds (waitTime);
		gameObject.SetActive (false);
	}
		
	// Triggered when a collision happens.
	void OnTriggerEnter2D (Collider2D other) {
		if (other.tag == "Enemy") {
			Debug.LogError ("Enemy");
			Destroy (other.gameObject);

		} else if (other.tag == "Wall") {
			Debug.LogError ("Wall");
		} 
		else if (other.tag == "Player") { 
			Debug.Log ("Collided with Player");
		}

		else {
			Debug.LogError ("Didnt touch.");
		}
	}
}
