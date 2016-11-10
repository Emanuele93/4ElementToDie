using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	// Player base stats, they're fixed through the game so we can declare them as constants.
	private const int baseVitality = 4;
	private const int baseAttack = 4;
	private const int baseDefense = 4;
	private const int baseSpeed = 4;

	// Player base hidden stats, they're fixed through the game.
	private const int baseLuck = 4; 
	private const int baseAttackSpeed = 4;
	private const int baseAttackRange = 4;

	// Player equipment slots.
	private const int numberEquipmentSlots = 3;
	public int[] equipmentSlots = new int[numberEquipmentSlots];

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
	float mHorizontalMov = 0f;
	float mVerticalMov = 0f;

	// Use this for initialization
	void Start () {
		tr = GetComponent<Transform> () as Transform;

		FillWithBaseStats ();
	}
	
	// Update is called once per frame
	void Update () {
		mHorizontalMov = Input.GetAxis ("Horizontal");
		mVerticalMov = Input.GetAxis ("Vertical");

		// Checking for attacking.
		if (Input.GetKeyDown (KeyCode.Space)) {
			//TODO: Handle attack.
		}
	}

	// Fixed update because the player can
	void FixedUpdate() {
		// Moving the player.
		tr.position += 
			tr.right * Time.fixedDeltaTime * mSpeed * mHorizontalMov +
			tr.up * Time.fixedDeltaTime * mSpeed * mVerticalMov;
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

		for (int i = 0;i <  numberEquipmentSlots; i++) {
			equipmentSlots [i] = 0;
		}
	}
}
