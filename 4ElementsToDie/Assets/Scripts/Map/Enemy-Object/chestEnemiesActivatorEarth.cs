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
            item = enemyObjectCollection.GetComponent<EnemyObjectCollection>().getEarthEquipment(getRarity());
            equipment = true;
        }
        if (equipment)
            numObject = Random.Range(0, 3);
        else
            numObject = Random.Range(2, 4);
        while (numObject > 0)
        {
            numObject--;
            GameObject go = enemyObjectCollection.GetComponent<EnemyObjectCollection>().getEarthObject();
            go.transform.parent = transform;
            go.transform.position = transform.position;
            go.SetActive(false);
            objects.Add(go);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && other.gameObject.GetComponent<CharacterManager>().Keys[(int)ElementType.Earth] > 0)
        {
            player = other.gameObject.GetComponent<CharacterManager>();
            buttom.SetActive(true);
            inChestArea = true;
        }
        else return;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            inChestArea = false;
            buttom.SetActive(false);
        }
    }

    protected override void remouveKey()
    {
        player.Keys[(int)ElementType.Earth]--;

    }

    private int getRarity()
    {
        int rarity, variation;
        rarity = gm.getNoKilledBosses((int)ElementType.Earth) + 2;
        if (rarity > 4) rarity = 4;
        if (Random.Range(0, 2) == 0)
        {
            if (Random.Range(0, 3) == 0) variation = 2;
            else variation = 1;
            if (Random.Range(0, 2) == 0) variation = -variation;
            rarity += variation;
            if (rarity < 1) rarity = 1;
            else if (rarity > 3) rarity = 3;
        }
        return rarity;
    }
}