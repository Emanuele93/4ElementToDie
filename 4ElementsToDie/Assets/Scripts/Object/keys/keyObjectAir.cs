using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class keyObjectAir : keyObject
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            //GameManager addAirKey();
            Debug.Log("AirKey");
            Destroy(gameObject);
        }
    }
}
