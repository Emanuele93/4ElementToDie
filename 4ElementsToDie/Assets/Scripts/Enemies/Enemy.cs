﻿using UnityEngine;
using System.Collections;
using POLIMIGameCollective;

public class Enemy : MonoBehaviour {
    
    // Unity objects references
    Transform tr;
    Animator animator;
    CharacterManager charManager;
    GameObject player;

    // Movement/Attack state variables
    bool isFacingRight;
    bool isFacingUp;
    bool isAggressive;
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
        player = GameObject.FindGameObjectWithTag ("Player");
        
        isFacingRight = true;
        isFacingUp = false;
        isInCooldown = false;

    }
	
	// Fixed update because the Enemy can
	void FixedUpdate() {

        isAggressive = EnemyMovement.calculateDistance(tr, player.transform);

        if (isAggressive) {

            // Moving
            Move();

            // Attacking
            if (!isInCooldown)
            {
                // TODO: adjust attack direction to the one of the player
                Attack();
            }
		} 
			
		EnemyAnimation.Animate (animator, isAggressive);
	}

    void Move()
    {
        float movSpeed = (float)charManager.Stats[(int)StatType.SPD].FinalStat;
        bool[] facings = EnemyMovement.Move(tr, movSpeed, isFacingRight, isFacingUp, player.transform);
        isFacingRight = facings[0];
		isFacingUp = facings [1];
    }

	void Attack() {
        GameObject go = null;

		// Calculating the angle between the enemy and the player.
		float deltaY = player.transform.position.y - tr.position.y; 
		float deltaX = player.transform.position.x - tr.position.x;

		// The rotation is the angle in Z to get the player.
		float rotation =  Mathf.Atan2 (deltaY, deltaX) * 180 / Mathf.PI;

        //choose the correct attack type;
        switch (charManager.AttackType)
        {
            case AttackType.Slashing:
                go = ObjectPoolingManager.Instance.GetObject(m_SlashPrefab.name);
                go.transform.position = m_SlashTransform.position;
                break;
			case AttackType.Thrusting:
				go = ObjectPoolingManager.Instance.GetObject (m_ThrustPrefab.name);
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
		go.transform.rotation = Quaternion.Euler (new Vector3 (0, 0, rotation));
        GameplayManager.Instance.attackersDict[go.GetInstanceID()] = charManager;
        StartCoroutine(WaitForCooldown());
    }

    IEnumerator WaitForCooldown()
    {

        isInCooldown = true;
        double attSpeed = charManager.Stats[(int)StatType.ATTSpd].FinalStat;
        double cooldownTime = 1 / attSpeed;

		yield return new WaitForSeconds ((float)cooldownTime);

        isInCooldown = false;
    }
}
