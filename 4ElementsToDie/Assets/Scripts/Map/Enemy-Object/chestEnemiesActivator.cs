using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class chestEnemiesActivator : MonoBehaviour
{
    private List<GameObject> childs = new List<GameObject>();
    protected List<GameObject> objects = new List<GameObject>();
    public GameObject buttom;
    protected bool inChestArea;


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (inChestArea && Input.GetKeyDown(KeyCode.F))
        {
            foreach (GameObject child in childs)
            {
                child.SetActive(true);
            }
            foreach (GameObject child in objects)
            {
                child.SetActive(true);
                child.transform.parent = transform.parent;
            }
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            buttom.SetActive(true);
            inChestArea = true;
        }
        else return;
    }

    public void addChild(GameObject child)
    {
        childs.Add(child);
    }

    public abstract void addItemOnChest(GameObject enemyObjectCollection);
}
