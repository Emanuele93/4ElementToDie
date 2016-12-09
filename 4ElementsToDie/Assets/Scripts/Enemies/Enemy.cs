using UnityEngine;
using System.Collections;
using POLIMIGameCollective;

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
	private const float baseAttackRate = 1f;

	// Enemy onGame visible stats.
	public int mVitality {get; private set;}
	public int mAttack {get; private set;}
	public int mDefense {get; private set;}
	public int mSpeed {get; private set;}

	// Enemy onGame hidden stats.
	public int mLuck { get; private set;}
	public int mAttackSpeed { get; private set;}
	public int mAttackRange { get; private set;}

	// Attack tag.
	string attackTag =  "FromEnemy";

	[Header ("Attack transforms")]
	public Transform m_AreaTransform;
	public Transform m_RangeTransform;
	public Transform m_SlashTransform;
	public Transform m_ThrustTransform;

	[Header ("Attack prefabs")]
	public GameObject m_AreaPrefab;
	public GameObject m_RangePrefab;
	public GameObject m_SlashPrefab;
	public GameObject m_ThrustPrefab;

	// Unity objects and variables.
	Transform tr;
	Animator mAnimator;
	GameObject player;
	CharacterManager charManager;

	// Facing and chasing variables.
	bool mFacingRight;
	bool mFacingUp;
	bool chasePlayer;
	bool isInCooldown;

	WaitForSeconds m_cooldownTime = new WaitForSeconds(baseAttackRate);

	// Use this for initialization
	void Start () {
		tr = GetComponent<Transform> () as Transform;
		mAnimator = GetComponent<Animator> () as Animator;
		charManager = GetComponent<CharacterManager> () as CharacterManager;
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

			StartCoroutine(AttackPlayer ());
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
		isInCooldown = false;
	}

	IEnumerator AttackPlayer() {
		if (!isInCooldown){
			// Here we dispatch the attack based on enemy's type.
			GameObject go = ObjectPoolingManager.Instance.GetObject (m_AreaPrefab.name);
			go.transform.position = m_AreaTransform.position;
			go.transform.rotation = Quaternion.Euler (0f, 0f, 0f);
			go.tag = attackTag;

			 GameplayManager.Instance.attackersDict [go.GetInstanceID ()] = charManager;

			isInCooldown = true;
			yield return m_cooldownTime;
			isInCooldown = false;
		}
	}
}
