using UnityEngine;
using System.Collections;

// PlayerScript requires the GameObject to have a Rigidbody component
[RequireComponent(typeof(BoxCollider2D))]
public class RangedAttack : Attack
{
    BoxCollider2D col;
    Vector2 colliderSize = new Vector2(2f, 1f);

    float attSpeed;
    float direction;

    // Use this for initialization
    protected override void Start()
    {
        base.Start();

        col = GetComponent<BoxCollider2D>() as BoxCollider2D;
        
        direction = 1f;
        col.size = colliderSize;
        col.isTrigger = true;
    }

    public override void AttackNow()
    {
        // Attack Speed stat
        attSpeed = (float)GameplayManager.Instance.attackersDict[gameObject.GetInstanceID()].Stats[(int)StatType.ATTSpd].FinalStat;

        tr.position += tr.right * Time.fixedDeltaTime * direction * attSpeed;
        StartCoroutine(Fade());
    }

    // Triggered when a collision happens.
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        CharacterManager attacker = GameplayManager.Instance.attackersDict[gameObject.GetInstanceID()];
        CharacterManager defender = other.GetComponent<CharacterManager>() as CharacterManager;

		if (other.tag == "wall" || other.tag == "obstacle")
        {
            gameObject.SetActive(false);
        }
		else if ( (attacker.tag == "Player"  && other.tag == "Enemy") || 
			(attacker.tag == "Enemy" && other.tag == "Player") )
        {
            GameplayManager.Instance.ExecuteAttack(attacker, defender);
			gameObject.SetActive(false);
        }
    }
}
