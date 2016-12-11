using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class chestEnemiesActivator : MonoBehaviour
{
    private List<GameObject> enemies = new List<GameObject>();
    public GameObject buttom;
    protected bool inChestArea;
    public CharacterManager player;
    protected List<GameObject> objects = new List<GameObject>();


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (inChestArea && Input.GetKeyDown(KeyCode.F))
        {
            foreach (GameObject child in enemies)
            {
                child.SetActive(true);
            }
            foreach (GameObject child in objects)
            {
                child.SetActive(true);
                child.transform.parent = transform.parent;
                child.transform.position = new Vector3(child.transform.position.x + Random.Range(-1f, 1f), child.transform.position.y + Random.Range(-1f, 1f), 0);
            }
            Destroy(gameObject);
        }
    }

    public void addChild(GameObject child)
    {
        enemies.Add(child);
    }

    public abstract void addItemOnChest(GameObject enemyObjectCollection);
}
