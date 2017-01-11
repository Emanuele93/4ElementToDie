using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class chestEnemiesActivator : MonoBehaviour
{
    public GameplayManager gm;
    private List<GameObject> enemies = new List<GameObject>();
    public GameObject buttom;
    protected bool inChestArea;
    protected CharacterManager player;
    public List<GameObject> objects = new List<GameObject>();
    public Item item = null;


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (inChestArea && Input.GetKeyDown(KeyCode.F))
        {
            remouveKey();
            GameplayManager.Instance.UpdateKeyBar();
            foreach (GameObject go in enemies)
                go.SetActive(true);
            gm.openChest(gameObject);
        }
    }

    public void addChild(GameObject child)
    {
        enemies.Add(child);
    }

    public abstract void addItemOnChest(GameObject enemyObjectCollection);

    protected abstract void remouveKey();
}
