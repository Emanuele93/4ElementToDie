using UnityEngine;
using System.Collections;

public class door : MonoBehaviour {
    private bool inDoorArea;
    private GameObject player;
    public GameObject buttom;

    void Start () {
        inDoorArea = false;
	}
	
	void Update () {
        if (inDoorArea && Input.GetKeyDown(KeyCode.F))
        {
            Vector3 mouvement = new Vector3(0, 6, 0);
            player.transform.position = transform.rotation * mouvement + transform.position;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            buttom.SetActive(true);
            player = other.gameObject;
            inDoorArea = true;
        }
        else return;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            buttom.SetActive(false);
            inDoorArea = false;
        }
        else return;
    }
}
