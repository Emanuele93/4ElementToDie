﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class keyObjectFire : keyObject
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            //GameManager addFireKey()
            Destroy(gameObject);
        }
    }
}
