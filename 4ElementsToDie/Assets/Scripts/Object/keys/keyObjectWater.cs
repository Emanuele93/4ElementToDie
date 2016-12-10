using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class keyObjectWater : keyObject
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            //GameManager addWaterKey();
            Debug.Log("WaterKey");
            Destroy(gameObject);
        }
    }
}
