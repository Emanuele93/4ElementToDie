using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class keyObjectEarth : keyObject
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            other.gameObject.GetComponent<CharacterManager>().Keys[(int)ElementType.Earth]++;
            Destroy(gameObject);
        }
    }
}
