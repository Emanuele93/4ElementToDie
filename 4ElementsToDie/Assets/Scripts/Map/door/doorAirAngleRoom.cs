using UnityEngine;
using System.Collections;

public class doorAirAngleRoom : MonoBehaviour
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

            transform.parent.gameObject.SetActive(false);
            where.SetActive(true);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            CharacterManager player = other.gameObject.GetComponent<CharacterManager>();
            if ((player.Element != ElementType.Fire && player.Stones[(int)ElementType.Fire] > 0) || (player.Element == ElementType.Fire && player.Stones[(int)ElementType.Fire] > 1))
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
