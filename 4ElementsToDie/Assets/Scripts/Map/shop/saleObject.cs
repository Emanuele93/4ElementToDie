using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class saleObject : MonoBehaviour
{

    public GameObject buttom;
    private bool onArea;
    public Text value;
    private GameObject player;
    private Item item;

    // Use this for initialization
    void Start()
    {
        value.text = item.price + " ";
        onArea = false;
        //gameObject.transform.localScale = new Vector3(1.5f, 1.5f, 1f);
    }

    void Update()
    {
        if (onArea && Input.GetKeyDown(KeyCode.F))
        {
            if (player.GetComponent<CharacterManager>().Money >= item.price && player.GetComponent<CharacterManager>().AddItem(item))
            {
                player.GetComponent<CharacterManager>().Money -= item.price;
                GameplayManager.Instance.UpdateCoinBar();
                Destroy(gameObject);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            buttom.SetActive(true);
            player = other.gameObject;
            onArea = true;
        }
        else return;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            buttom.SetActive(false);
            onArea = false;
        }
        else return;
    }

    public Item saleItem
    {
        get { return item; }
        set { item = value; }
    }
}
