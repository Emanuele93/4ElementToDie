using UnityEngine;
using System.Collections;

public class doorWaterAngleRoom : MonoBehaviour
{
    protected bool inDoorArea;
    protected GameObject player;
    public GameObject buttom;
    public GameObject where;

    void Start()
    {
        inDoorArea = false;
    }

    void Update()
    {
        if (inDoorArea && Input.GetKeyDown(KeyCode.F))
        {
            Vector3 mouvement = new Vector3(0, 3.5f, 0);
            player.transform.position = transform.rotation * mouvement + transform.position;
            Camera.main.transform.position = new Vector3(where.transform.position.x, where.transform.position.y, Camera.main.transform.position.z);
            
            inDoorArea = false;
            where.SetActive(true);
            transform.parent.gameObject.SetActive(false);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            CharacterManager player = other.gameObject.GetComponent<CharacterManager>();
            if ((player.Element != ElementType.Air && player.Stones[(int)ElementType.Air] > 0) || (player.Element == ElementType.Air && player.Stones[(int)ElementType.Air] > 1))
            {
                buttom.SetActive(true);
                this.player = other.gameObject;
                inDoorArea = true;
            }
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
