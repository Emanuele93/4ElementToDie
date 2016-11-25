using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	// Enemy base stats, they're fixed through the game so we can declare them as constants.
	private const int baseVitality = 2;
	private const int baseAttack = 2;
	private const int baseDefense = 2;
	private const int baseSpeed = 2;

	// Enemy base hidden stats, they're fixed through the game.
	private const int baseLuck = 2; 
	private const int baseAttackSpeed = 2;
	private const int baseAttackRange = 2;

	// Enemy onGame visible stats.
	public int mVitality {get; private set;}
	public int mAttack {get; private set;}
	public int mDefense {get; private set;}
	public int mSpeed {get; private set;}

	// Enemy onGame hidden stats.
	public int mLuck { get; private set;}
	public int mAttackSpeed { get; private set;}
	public int mAttackRange { get; private set;}

	// Unity objects and variables.
	Transform tr;
	float mHorizontalAttack = 0f;
	float mVerticalAttack = 0f;
	Animator mAnimator;
	GameObject player;

	// Facing and chasing variables.
	bool mFacingRight;
	bool mFacingUp;
	bool chasePlayer;


	// Use this for initialization
	void Start () {
		tr = GetComponent<Transform> () as Transform;
		mAnimator = GetComponent<Animator> () as Animator;
		player  = GameObject.FindGameObjectWithTag ("Player");
		FillWithBaseStats ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	// Fixed update because the Enemy can
	void FixedUpdate() {
		chasePlayer = false;

		if (EnemyMovement.calculateDistance (tr, player.transform)) {
			bool[] facings = EnemyMovement.Move (tr, mSpeed, mFacingRight, mFacingUp, player.transform);
			mFacingRight = facings [0];
			mFacingUp = facings [1];
			chasePlayer = true;
		} 
			
		EnemyAnimation.Animate (mAnimator, chasePlayer);
			
		// Attacking.
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

		mFacingRight = true;
		mFacingUp = false;
	}
}
