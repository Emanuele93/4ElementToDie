using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shopAir : MonoBehaviour {
    
    public EnemyObjectCollection enemyObjectCollection;
    public GameObject saleObject;

    // Use this for initialization
    void Start ()
    {
        Item airItem, earthItem, fireItem, waterItem;
        GameObject go;

        airItem = enemyObjectCollection.getAirEquipment(4);
        earthItem = enemyObjectCollection.getEarthEquipment(2);
        fireItem = enemyObjectCollection.getFireEquipment(3);
        waterItem = enemyObjectCollection.getWaterEquipment(1);

        go = Instantiate(saleObject, new Vector3(2 + transform.parent.position.x, 3 + transform.parent.position.y, 0), Quaternion.Euler(0, 0, 0), transform.parent);
        go.GetComponent<SpriteRenderer>().sprite = airItem.sprite;
        go.GetComponent<saleObject>().saleItem = airItem;
        go = Instantiate(saleObject, new Vector3(2 + transform.parent.position.x, 1 + transform.parent.position.y, 0), Quaternion.Euler(0, 0, 0), transform.parent);
        go.GetComponent<SpriteRenderer>().sprite = earthItem.sprite;
        go.GetComponent<saleObject>().saleItem = earthItem;
        go = Instantiate(saleObject, new Vector3(2 + transform.parent.position.x, -1 + transform.parent.position.y, 0), Quaternion.Euler(0, 0, 0), transform.parent);
        go.GetComponent<SpriteRenderer>().sprite = fireItem.sprite;
        go.GetComponent<saleObject>().saleItem = fireItem;
        go = Instantiate(saleObject, new Vector3(2 + transform.parent.position.x, -3 + transform.parent.position.y, 0), Quaternion.Euler(0, 0, 0), transform.parent);
        go.GetComponent<SpriteRenderer>().sprite = waterItem.sprite;
        go.GetComponent<saleObject>().saleItem = waterItem;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
