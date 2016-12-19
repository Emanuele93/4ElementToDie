using UnityEngine;
using System.Collections;

public class miniMapSetter : MonoBehaviour
{
    public GameObject controller;
    private bool entered;

    // Use this for initialization
    void Start()
    {
        entered = false;
    }

    // Update is called once per frame
    void Update()
    {/*
        Lo deve fare l'enemies?? <------------------------------------- TODO

        if(transform.childCount == 0 && entered)
        {
            controller.GetComponent<miniMapContoller>().finishPosition(transform.position.x, transform.position.y);
        }*/
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (!entered)
            {
                controller.GetComponent<miniMapContoller>().newPosition(transform.position.x, transform.position.y);
                entered = true;
            }
            else
            {
                controller.GetComponent<miniMapContoller>().movePlayer(transform.position.x, transform.position.y);
            }
        }
    }
}
