﻿using UnityEngine;
using System.Collections;

public class doorAir : MonoBehaviour
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
            mouvement = new Vector3(0, 6, 0);
            mouvement = transform.rotation * mouvement + transform.position;
            Camera.main.transform.position = new Vector3(mouvement.x, mouvement.y, Camera.main.transform.position.z);
            Camera.main.backgroundColor = new Color(0.035f, 0.471f, 0.925f);

            // Changing the sound.
            GameplayManager.Instance.PlayMusic(Constants.MUSIC_AirArea);

            transform.parent.gameObject.SetActive(false);
            where.SetActive(true);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && other.gameObject.GetComponent<CharacterManager>().Stones[(int)ElementType.Air] > 0)
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
