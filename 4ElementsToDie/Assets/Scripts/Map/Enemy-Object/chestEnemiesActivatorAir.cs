using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class chestEnemiesActivatorAir : chestEnemiesActivator
{
    public override void addItemOnChest(GameObject enemyObjectCollection)
    {
        bool equipment = false;
        int numObject;
        if (Random.Range(0, 3) == 0)
        {
            Item it = enemyObjectCollection.GetComponent<EnemyObjectCollection>().getAirEquipment(Random.Range(1, 5));
            objects.Add(it);
            equipment = true;
        }
        if (equipment)
            numObject = Random.Range(0, 3);
        else
            numObject = Random.Range(2, 4);
        while (numObject > 0)
        {
            numObject--;
            Item it = enemyObjectCollection.GetComponent<EnemyObjectCollection>().getAirObject();
            objects.Add(it);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && other.gameObject.GetComponent<CharacterManager>().Keys[(int)ElementType.Air] > 0)
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
        player.Keys[(int)ElementType.Air]--;

    }
}
