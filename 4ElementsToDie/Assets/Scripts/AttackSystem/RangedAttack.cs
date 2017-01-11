using UnityEngine;
using System.Collections;

// PlayerScript requires the GameObject to have a Rigidbody component
[RequireComponent(typeof(BoxCollider2D), typeof(SpriteRenderer))]
public class RangedAttack : Attack
{
    BoxCollider2D col;
    Vector2 colliderSize = new Vector2(2f, 1f);

    float baseBulletRange = 1f;
    float baseBulletSpeed = 5f;
    float attSpeed;

	public Sprite attackSprite;

    // Use this for initialization
    protected override void Start()
    {
        base.Start();

        col = GetComponent<BoxCollider2D>() as BoxCollider2D;
        
        col.size = colliderSize;
        col.isTrigger = true;

		sr.sprite = attackSprite;
    }

    public override void AttackNow()
    {
        // Attack Speed stat
        attSpeed = (float)GameplayManager.Instance.attackersDict[gameObject.GetInstanceID()].Stats[(int)StatType.AttSPD].FinalStat;
        // Attack Range stat
        attRange = (float)GameplayManager.Instance.attackersDict[gameObject.GetInstanceID()].Stats[(int)StatType.AttRNG].FinalStat;

        tr.position += tr.right * Time.fixedDeltaTime * baseBulletSpeed * attSpeed;
        StartCoroutine(Fade());
    }

    protected override IEnumerator Fade()
    {
        waitTime = baseBulletRange * attRange;
        yield return new WaitForSeconds(waitTime);
        gameObject.SetActive(false);
    }

    // Triggered when a collision happens.
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        CharacterManager attacker = GameplayManager.Instance.attackersDict[gameObject.GetInstanceID()];
        CharacterManager defender = other.GetComponent<CharacterManager>() as CharacterManager;

		if (other.tag == "wall" || other.tag == "door")
        {
            gameObject.SetActive(false);
        }
		else if ( 
			(attacker.tag == "Player"  && other.tag == "Enemy") || 
			(attacker.tag == "Enemy" && other.tag == "Player")  || 
			(attacker.tag == "Player" && other.tag == "Boss")   ||
			(attacker.tag == "Boss" && other.tag == "Player") 
		    )
        {
            GameplayManager.Instance.ExecuteAttack(attacker, defender);
			gameObject.SetActive(false);
        }
    }
}
