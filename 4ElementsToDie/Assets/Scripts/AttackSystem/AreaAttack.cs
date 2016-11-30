using UnityEngine;
using System.Collections;

// PlayerScript requires the GameObject to have a Rigidbody component
[RequireComponent (typeof (BoxCollider2D))]
[RequireComponent (typeof (Rigidbody2D))]
public class AreaAttack : MonoBehaviour {
	BoxCollider2D collider;
	Rigidbody2D rb;
	Transform tr;


	float mSpeed;
	float direction;

	// Use this for initialization
	void Start () {
		tr = GetComponent<Transform> () as Transform;
		collider = GetComponent<BoxCollider2D> () as BoxCollider2D;
		rb = GetComponent<Rigidbody2D> () as Rigidbody2D;

		mSpeed = 5f;
		direction = 1f;
	}
	
	// the attack method is called when the user presses the attack button.
	void Update () {
		
	}

	void FixedUpdate () {
		Attack ();
	}

	// the attack method is called when the user presses the attack button.
	public void Attack() {
		tr.position += tr.right * Time.fixedDeltaTime * direction * mSpeed;
	}

	// Triggered when a collision happens.
	void OnTriggerEnter2D (Collider2D other) {
		if (other.tag == "Enemy") {
			Debug.LogError ("Enemy");
			Destroy (other.gameObject);
			Destroy (gameObject);

		} else if (other.tag == "Wall") {
			Debug.LogError ("Wall");
			Destroy (gameObject);
		} 
		else if (other.tag == "Player") { 
			Debug.Log ("Collided with Player");
		}

		else {
			Debug.LogError ("Didnt touch.");
		}
	}
}
