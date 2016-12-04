using UnityEngine;
using System.Collections;

[RequireComponent (typeof (PolygonCollider2D),typeof(Rigidbody2D))]
public class ThrustAttack : MonoBehaviour {
	PolygonCollider2D col;
	Rigidbody2D rb;
	Transform tr;

	const int numberOfPoints = 3;

	float mSpeed;
	float direction;

	float waitTime = 3f;

	void Start () {
		tr = GetComponent<Transform> () as Transform;
		col = GetComponent<PolygonCollider2D> () as PolygonCollider2D;
		rb = GetComponent<Rigidbody2D> () as Rigidbody2D;

		mSpeed = 5f;
		direction = 1f;
		col.SetPath (0,setColliderPoints ());
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

	Vector2[] setColliderPoints() {
		// Thanks to my friend salim :D.

		// Creating the points for the angle.
		Vector2[] points = new Vector2[numberOfPoints];
		int attackAngle = 30;
		float rad; float a; float r;

		// Point.
		r = 2.5f;
		points [0] = new Vector2 (Mathf.Cos (0) * r, Mathf.Sin (0) * r);

		// Down point.
		a = attackAngle;
		r = 0.5f;
		rad = a * Mathf.PI / 180f;
		points [1] = new Vector2 (Mathf.Cos (rad) * r, Mathf.Sin (rad) * r);


		// Upper-Point.
		a = -attackAngle;
		r = 0.5f;
		rad = a * Mathf.PI / 180f;
		points [2] = new Vector2 (Mathf.Cos (rad) * r, Mathf.Sin (rad) * r);

		return points;
	}
}
