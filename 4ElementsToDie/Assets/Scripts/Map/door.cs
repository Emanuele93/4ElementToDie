using UnityEngine;
using System.Collections;

public class door : MonoBehaviour {
    private bool inDoorArea;
    private GameObject player;
    public GameObject buttom;
    public GameObject where;

    void Start () {
        inDoorArea = false;
	}
	
	void Update () {
        if (inDoorArea && Input.GetKeyDown(KeyCode.F))
        {
            Vector3 mouvement = new Vector3(0, 4, 0);
            player.transform.position = transform.rotation * mouvement + transform.position;
            Camera.main.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, Camera.main.transform.position.z);

            transform.parent.gameObject.SetActive(false);
            where.SetActive(true);
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
