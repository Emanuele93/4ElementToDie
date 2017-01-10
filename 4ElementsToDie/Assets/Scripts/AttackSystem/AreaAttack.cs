using UnityEngine;
using System.Collections;

[RequireComponent (typeof(CircleCollider2D), typeof(SpriteRenderer))]
public class AreaAttack : Attack {
	CircleCollider2D col;

	public Sprite attackSprite;

	Vector3 initialScale = new Vector3 (1f,1f,1f);
	Vector3 updateScale = new Vector3 (0f,0f,0f);

	// Use this for initialization
	protected override void Start ()
	{
		base.Start ();

		col = GetComponent<CircleCollider2D> () as CircleCollider2D;
		col.isTrigger = true;
	}

	protected override IEnumerator Fade() {

		yield return new WaitForSeconds(waitTime);
		sr.transform.localScale = initialScale;
		updateScale.x = 0f;
		updateScale.y = 0f;
		gameObject.SetActive(false);
	}

	public override void AttackNow()
	{
		attRange = (float)GameplayManager.Instance.attackersDict[gameObject.GetInstanceID()].Stats[(int)StatType.AttRNG].FinalStat;
		updateScale.x += (Time.fixedDeltaTime * attRange) / 5;
		updateScale.y += (Time.fixedDeltaTime * attRange) / 5;
		sr.transform.localScale += updateScale;
		StartCoroutine (Fade ());

	}
}