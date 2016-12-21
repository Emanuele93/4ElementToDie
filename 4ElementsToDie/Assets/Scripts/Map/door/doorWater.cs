using UnityEngine;
using System.Collections;

public class doorWater : MonoBehaviour
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
        if (inDoorArea && Input.GetKeyDown(KeyCode.F) && player.GetComponent<CharacterManager>().Stones[(int)ElementType.Water] > 0)
        {
            Vector3 mouvement = new Vector3(0, 3.5f, 0);
            player.transform.position = transform.rotation * mouvement + transform.position;
            mouvement = new Vector3(0, 6, 0);
            mouvement = transform.rotation * mouvement + transform.position;
            Camera.main.transform.position = new Vector3(mouvement.x, mouvement.y, Camera.main.transform.position.z);

			// Changing the sound.
			GameplayManager.Instance.PlayMusic(Constants.MUSIC_WaterArea);

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
