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
    {
        if(transform.childCount == 0 && entered)
        {
            controller.GetComponent<miniMapContoller>().finishPosition(transform.position.x, transform.position.y);
            Destroy(gameObject);
        }
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            controller.GetComponent<miniMapContoller>().newPosition(transform.position.x, transform.position.y);
            entered = true;
        }
    }
}
