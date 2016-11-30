using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	// Player base stats, they're fixed through the game so we can declare them as constants.
	private const int baseVitality = 3;
	private const int baseAttack = 3;
	private const int baseDefense = 3;
	private const int baseSpeed = 3;

	// Player base hidden stats, they're fixed through the game.
	private const int baseLuck = 3; 
	private const int baseAttackSpeed = 3;
	private const int baseAttackRange = 3;

	// Player equipment slots.
	private const int numberEquipmentSlots = 3;
	private int[] equipmentSlots = new int[numberEquipmentSlots];

	// Player onGame visible stats.
	public int mVitality {get; private set;}
	public int mAttack {get; private set;}
	public int mDefense {get; private set;}
	public int mSpeed {get; private set;}

	// Player onGame hidden stats.
	public int mLuck { get; private set;}
	public int mAttackSpeed { get; private set;}
	public int mAttackRange { get; private set;}

	// Unity objects and variables.
	Transform tr;
//	float mHorizontalAttack = 0f;
//	float mVerticalAttack = 0f;
	Animator mAnimator;

	// Facing variables.
	bool mFacingRight;
	bool mFacingUp;
	//bool mFacingUp;


	// Use this for initialization
	void Start () {
		tr = GetComponent<Transform> () as Transform;
		mAnimator = GetComponent<Animator> () as Animator;

		FillWithBaseStats ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	// Fixed update because the player can
	void FixedUpdate() {
		bool[] facings = PlayerMovement.captureMovement (tr, mSpeed, mFacingRight, mFacingUp);
		mFacingRight = facings [0]; mFacingUp = facings [1];

		PlayerAnimation.Animate (mAnimator);
			
		// Attacking.
		if ( (Input.GetKeyDown (KeyCode.L)) || (Input.GetKeyDown (KeyCode.RightArrow)) ){
			Debug.Log ("Right Attack");
		}

		if ( (Input.GetKeyDown (KeyCode.J)) || (Input.GetKeyDown (KeyCode.LeftArrow)) ){
			Debug.Log ("Left Attack");
		}

		if ( (Input.GetKeyDown (KeyCode.I)) || (Input.GetKeyDown (KeyCode.UpArrow)) ){
			Debug.Log ("Up Attack");
		}

		if ( (Input.GetKeyDown (KeyCode.K)) || (Input.GetKeyDown (KeyCode.DownArrow)) ){
			Debug.Log ("Down Attack");
		}

	}

	// Fills all stats with the base values.
	void FillWithBaseStats(){
		mVitality = baseVitality;
		mAttack = baseAttack;
		mDefense = baseDefense;
		mSpeed = baseSpeed;
		mLuck = baseLuck;
		mAttackSpeed = baseAttackSpeed;
		mAttackRange = baseAttackRange;

		for (int i = 0; i < numberEquipmentSlots; i++) {
			equipmentSlots [i] = 0;
		}

		mFacingRight = true;
		mFacingUp = false;
	}
}
