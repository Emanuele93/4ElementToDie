using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class enemiesActivator : MonoBehaviour
{
    private List<GameObject> childs = new List<GameObject>();


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            foreach (GameObject child in childs)
            {
                child.SetActive(true);
            }
            Destroy(gameObject);
        }
        else return;
    }

    public void addChild(GameObject child)
    {
        childs.Add(child);
    }
}
