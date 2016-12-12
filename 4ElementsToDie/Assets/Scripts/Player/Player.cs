using UnityEngine;
using System.Collections;
using POLIMIGameCollective;


public class Player : MonoBehaviour {
    
	// Unity objects references
	Transform tr;
	Animator animator;
    CharacterManager charManager;

    // Movement/Attack state variables
    bool isFacingRight;
	bool isFacingUp;
    bool isInCooldown;

    [Header ("Attack transforms")]
	public Transform m_SlashTransform;
	public Transform m_ThrustTransform;
    public Transform m_AreaTransform;
    public Transform m_RangedTransform;

    [Header ("Attack prefabs")]
    public GameObject m_SlashPrefab;
    public GameObject m_ThrustPrefab;
    public GameObject m_AreaPrefab;
    public GameObject m_RangedPrefab;

    // Use this for initialization
    void Start () {
		tr = GetComponent<Transform> () as Transform;
		animator = GetComponent<Animator> () as Animator;
		charManager = GetComponent<CharacterManager> () as CharacterManager;
        
        isFacingRight = true;
        isFacingUp = false;
        isInCooldown = false;
    }
	
	// Fixed update because the player can
	void FixedUpdate() {

        //Moving
        Move();
			
		// Attacking
		if (!isInCooldown) {

            // RIGHT attack
            if ((Input.GetKeyDown(KeyCode.L)) || (Input.GetKeyDown(KeyCode.RightArrow)))
            {
                Attack(Quaternion.Euler(0f, 0f, 0f));
            }

			// LEFT attack
			if ( (Input.GetKeyDown (KeyCode.J)) || (Input.GetKeyDown (KeyCode.LeftArrow)) )
            {
                Attack(Quaternion.Euler(0f, 0f, 180f));
            }

            // UP attack
            if ( (Input.GetKeyDown (KeyCode.I)) || (Input.GetKeyDown (KeyCode.UpArrow)) )
            {
                Attack(Quaternion.Euler(0f, 0f, 90f));
            }

			// Down attack.
			if ( (Input.GetKeyDown (KeyCode.K)) || (Input.GetKeyDown (KeyCode.DownArrow)))
            {
                Attack(Quaternion.Euler(0f, 0f, 270f));
            }
        }

        // Picking up drops
        if (Input.GetKeyDown(KeyCode.P))
        {
            Drop drop = GetClosestDropInRange();
            if (drop != null)
            {
                GameplayManager.Instance.PickUpDrop(drop);
            }
        }
    }

    void Move()
    {
        float movSpeed = (float)charManager.Stats[(int)StatType.SPD].FinalStat;
        bool[] facings = PlayerMovement.captureMovement(tr, movSpeed, isFacingRight, isFacingUp);
        isFacingRight = facings[0]; isFacingUp = facings[1];

        PlayerAnimation.Move(animator);
    }

    void Attack(Quaternion attackDirection)
    {
        GameObject go = null;

        //choose the correct attack type;
        switch (charManager.AttackType)
        {
            case AttackType.Slashing:
                go = ObjectPoolingManager.Instance.GetObject(m_SlashPrefab.name);
                go.transform.position = m_SlashTransform.position;
                break;
            case AttackType.Thrusting:
                go = ObjectPoolingManager.Instance.GetObject(m_ThrustPrefab.name);
                go.transform.position = m_ThrustTransform.position;
                break;
            case AttackType.Area:
                go = ObjectPoolingManager.Instance.GetObject(m_AreaPrefab.name);
                go.transform.position = m_AreaTransform.position;
                break;
            case AttackType.Ranged:
                go = ObjectPoolingManager.Instance.GetObject(m_RangedPrefab.name);
                go.transform.position = m_RangedTransform.position;
                break;
        }
        go.transform.rotation = attackDirection;
        GameplayManager.Instance.attackersDict[go.GetInstanceID()] = charManager;
        StartCoroutine(WaitForCooldown());
    }
    
    Drop GetClosestDropInRange()
    {
        // TODO: optimize (look only in range and then calculate the closest, not viceversa)
        Drop[] drops = (Drop[])GameObject.FindObjectsOfType(typeof(Drop));

        Drop closestDrop = null;
        double closestDistanceSqr = System.Double.PositiveInfinity;
        Vector3 currentPos = transform.position;

        foreach (Drop drop in drops)
        {
            float sqrDistance = (drop.transform.position - currentPos).sqrMagnitude;
            if (sqrDistance < closestDistanceSqr)
            {
                closestDrop = drop;
                closestDistanceSqr = sqrDistance;
            }
        }
        if (closestDistanceSqr <= Constants.MAX_PickupDropRange)
        {
            return closestDrop;
        }
        return null;
    }

	IEnumerator WaitForCooldown() {

		isInCooldown = true;
        double attSpeed = charManager.Stats[(int)StatType.ATTSpd].FinalStat;
        double cooldownTime = 1 / attSpeed;

		yield return new WaitForSeconds ((float) cooldownTime);

		isInCooldown = false;
	}
}
