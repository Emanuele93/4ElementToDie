using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Rigidbody2D))]
public  class Attack : MonoBehaviour {
	
	protected Rigidbody2D rb;
	protected Transform tr;

    protected float attRange;

	protected float waitTime = 1f;

	protected virtual void Start() {
		tr = GetComponent<Transform> () as Transform;
		rb = GetComponent<Rigidbody2D> () as Rigidbody2D;
	}

    void FixedUpdate()
    {
        AttackNow();
    }

    // the attack method is called when the user presses the attack button.
    public virtual void AttackNow()
    {
        // Attack Range stat
        attRange = (float)GameplayManager.Instance.attackersDict[gameObject.GetInstanceID()].Stats[(int)StatType.ATTRng].FinalStat;

        StartCoroutine(Fade());
    }

	protected IEnumerator Fade () {
		yield return new WaitForSeconds (waitTime);
		gameObject.SetActive (false);
	}

	// virtual in order to be overriden if necessary.
	protected virtual void OnTriggerEnter2D (Collider2D other) {
        CharacterManager attacker = GameplayManager.Instance.attackersDict [gameObject.GetInstanceID ()];
        CharacterManager defender = other.GetComponent<CharacterManager>() as CharacterManager;
        
        if ( attacker.tag != other.tag &&
            (attacker.tag == "Player" || other.tag == "Player") )
        {
            GameplayManager.Instance.ExecuteAttack(attacker, defender);
        }
    }
}
