using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class chestEnemiesActivatorEarth : chestEnemiesActivator
{
    public override void addItemOnChest(GameObject enemyObjectCollection)
    {
        bool equipment = false;
        int numObject;
        if (Random.Range(0, 3) == 0)
        {
            GameObject go = enemyObjectCollection.GetComponent<EnemyObjectCollection>().getEarthEquipment(Random.Range(1, 10));
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
            GameObject go = enemyObjectCollection.GetComponent<EnemyObjectCollection>().getEarthObject();
            go.transform.parent = transform;
            go.SetActive(false);
            objects.Add(go);
        }
    }
}