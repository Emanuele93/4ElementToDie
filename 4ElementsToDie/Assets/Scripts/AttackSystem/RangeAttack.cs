using UnityEngine;
using System.Collections;

// PlayerScript requires the GameObject to have a Rigidbody component
[RequireComponent (typeof (BoxCollider2D))]
[RequireComponent (typeof (Rigidbody2D))]
public class RangeAttack : MonoBehaviour {
	BoxCollider2D col;
	Rigidbody2D rb;
	Transform tr;


	float mSpeed;
	float direction;

	Vector2 colliderSize = new Vector2(2f,1f);

	// Use this for initialization
	void Start () {
		tr = GetComponent<Transform> () as Transform;
		col = GetComponent<BoxCollider2D> () as BoxCollider2D;
		rb = GetComponent<Rigidbody2D> () as Rigidbody2D;

		mSpeed = 5f;
		direction = 1f;
		col.size = colliderSize;
	}
	
	// the attack method is called when the user presses the attack button.
	void Update () {
		//Debug.Log ("Collider size: " +  (col.size + ( Vector2.up / 100)).ToString ());
		//col.size = new Vector2(col.size.x, col.size.y +1);
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
