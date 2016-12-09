using UnityEngine;
using System.Collections;
using POLIMIGameCollective;


public class Player : MonoBehaviour {
	// Player base stats, they're fixed through the game so we can declare them as constants.
	private const int baseVitality = 10;
	private const int baseAttack = 3;
	private const int baseDefense = 3;
	private const int baseSpeed = 3;

	// Player base hidden stats, they're fixed through the game.
	private const int baseLuck = 3; 
	private const int baseAttackSpeed = 1;
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

	private string attackTag = "FromPlayer";

	bool isInCooldown;
	WaitForSeconds m_cooldownTime;

	// Unity objects and variables.
	Transform tr;
	Animator mAnimator;
	CharacterManager charManager;

	// Facing variables.
	bool mFacingRight;
	bool mFacingUp;

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

	// Use this for initialization
	void Start () {
		tr = GetComponent<Transform> () as Transform;
		mAnimator = GetComponent<Animator> () as Animator;
		charManager = GetComponent<CharacterManager> () as CharacterManager;

		FillWithBaseStats ();
		m_cooldownTime = new WaitForSeconds(mAttackSpeed);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	// Fixed update because the player can
	void FixedUpdate() {
//		if (mVitality == 0) {
//			StartCoroutine (playerDead ());
//		}

		bool[] facings = PlayerMovement.captureMovement (tr,  mSpeed , mFacingRight, mFacingUp);
		mFacingRight = facings [0]; mFacingUp = facings [1];

		PlayerAnimation.Move (mAnimator);
			
		// Attacking.
		// Right attack.
		if (!isInCooldown) {
			if ( (Input.GetKeyDown (KeyCode.L)) || (Input.GetKeyDown (KeyCode.RightArrow)) ){
				GameObject go = ObjectPoolingManager.Instance.GetObject (m_ThrustPrefab.name);
				go.transform.position = m_ThrustTransform.position;
				go.transform.rotation = Quaternion.Euler (0f,0f,0f);
				go.tag = attackTag;
				GameplayManager.Instance.attackersDict [go.GetInstanceID ()] = charManager;
				StartCoroutine (WaitForCooldown ());

			}

			// Left attack.
			if ( (Input.GetKeyDown (KeyCode.J)) || (Input.GetKeyDown (KeyCode.LeftArrow)) ){
				GameObject go = ObjectPoolingManager.Instance.GetObject (m_ThrustPrefab.name);
				go.transform.position = m_ThrustTransform.position;
				go.transform.rotation = Quaternion.Euler (0f,0f,180f);
				go.tag = attackTag;
				GameplayManager.Instance.attackersDict [go.GetInstanceID ()] = charManager;
				StartCoroutine (WaitForCooldown ());
			}

			// Up attack.
			if ( (Input.GetKeyDown (KeyCode.I)) || (Input.GetKeyDown (KeyCode.UpArrow)) ){
				GameObject go = ObjectPoolingManager.Instance.GetObject (m_ThrustPrefab.name);
				go.transform.position = m_ThrustTransform.position;
				go.transform.rotation = Quaternion.Euler (0f,0f,90f);
				go.tag = attackTag;
				GameplayManager.Instance.attackersDict [go.GetInstanceID ()] = charManager;
				StartCoroutine (WaitForCooldown ());
			}

			// Down attack.
			if ( (Input.GetKeyDown (KeyCode.K)) || (Input.GetKeyDown (KeyCode.DownArrow)) ){
				GameObject go = ObjectPoolingManager.Instance.GetObject (m_ThrustPrefab.name);
				go.transform.position = m_ThrustTransform.position;
				go.transform.rotation = Quaternion.Euler (0f,0f,270f);
				go.tag = attackTag;
				GameplayManager.Instance.attackersDict [go.GetInstanceID ()] = charManager;
				StartCoroutine (WaitForCooldown ());
			}
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
		isInCooldown = false;
	}

	IEnumerator playerDead() {
		//yield return new WaitForSeconds (.1f);
		PlayerAnimation.Dead(mAnimator, true);
		yield return new WaitForSeconds (2.2f); // Waiting for the animation before disappear
		gameObject.SetActive (false);
	}

	IEnumerator WaitForCooldown() {
		isInCooldown = true;
		yield return m_cooldownTime;
		isInCooldown = false;

	}
}
