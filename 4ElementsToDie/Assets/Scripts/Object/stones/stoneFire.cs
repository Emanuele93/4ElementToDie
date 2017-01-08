using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stoneFire : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            other.gameObject.GetComponent<CharacterManager>().Stones[(int)ElementType.Fire]++;
            other.transform.position = new Vector3(0, 0, 0);
            Camera.main.transform.position = new Vector3(0, 0, Camera.main.transform.position.z);
            Camera.main.backgroundColor = new Color(0, 0, 0);
            transform.parent.parent.GetComponent<superMap>().centralRoomObject.SetActive(true);
            transform.parent.parent.gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }
}
