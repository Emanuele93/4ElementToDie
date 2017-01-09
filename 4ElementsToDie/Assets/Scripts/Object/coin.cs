using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coin : usableObject
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            other.gameObject.GetComponent<CharacterManager>().Money++;
            GameplayManager.Instance.UpdateCoinBar();
            Destroy(gameObject);
        }
    }
}
