using UnityEngine;
using System.Collections;

public class miniMapSetter : MonoBehaviour
{
    public GameObject controller;
    private bool entered = false;
    //private bool voidRoom = false;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {/*
        if(!voidRoom && transform.childCount == 0)
        {
            controller.GetComponent<miniMapContoller>().finishPosition(transform.position.x, transform.position.y);
            voidRoom = true;
        }*/
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (!entered)
            {
                bool chest = false;
                foreach(Transform t in transform)
                {
                    if (t.tag == "Chest")
                        chest = true;
                }
                if(chest)
                    controller.GetComponent<miniMapContoller>().newPosition(transform.position.x, transform.position.y, true);
                else
                    controller.GetComponent<miniMapContoller>().newPosition(transform.position.x, transform.position.y, false);
                entered = true;
            }
            else
            {
                controller.GetComponent<miniMapContoller>().movePlayer(transform.position.x, transform.position.y);
            }
        }
    }

    public void removeChest()
    {
        controller.GetComponent<miniMapContoller>().newPosition(transform.position.x, transform.position.y, false);
    }
}
