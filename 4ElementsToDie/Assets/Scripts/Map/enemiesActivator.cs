using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class enemiesActivator : MonoBehaviour
{
    private List<Transform> childs = new List<Transform>();


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
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(true);
                childs.Add(child);
            }
            Transform father = transform.parent;
            foreach (Transform child in childs)
                child.parent = father;
            Destroy(gameObject);
        }
        else return;
    }
}
