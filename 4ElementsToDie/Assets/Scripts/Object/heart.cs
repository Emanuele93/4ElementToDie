using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class heart : usableObject
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            //GameManager heart();
            Destroy(gameObject);
        }
    }
}
