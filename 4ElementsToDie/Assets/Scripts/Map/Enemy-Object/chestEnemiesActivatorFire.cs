using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class chestEnemiesActivatorFire : chestEnemiesActivator
{
    public override void addItemOnChest(GameObject enemyObjectCollection)
    {
        bool equipment = false;
        int numObject;
        if (Random.Range(0, 3) == 0)
        {
            GameObject go = enemyObjectCollection.GetComponent<EnemyObjectCollection>().getFireEquipment(Random.Range(1, 10));
            go.transform.parent = transform;
            go.SetActive(false);
            objects.Add(go);
            equipment = true;
        }
        if (equipment)
            numObject = Random.Range(0, 3);
        else
            numObject = Random.Range(2, 5);
        while (numObject > 0)
        {
            numObject--;
            GameObject go = enemyObjectCollection.GetComponent<EnemyObjectCollection>().getFireObject();
            go.transform.parent = transform;
            go.SetActive(false);
            objects.Add(go);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && player.Keys[(int)ElementType.Fire] > 0)
        {
            player.Keys[(int)ElementType.Fire]--;
            buttom.SetActive(true);
            inChestArea = true;
        }
        else return;
    }

}