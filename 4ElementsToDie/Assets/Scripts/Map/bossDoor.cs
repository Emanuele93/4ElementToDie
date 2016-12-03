using UnityEngine;
using System.Collections;

public class bossDoor : door
{
    void Update()
    {
        if (inDoorArea && Input.GetKeyDown(KeyCode.F))
        {
            Vector3 mouvement = new Vector3(0, 4, 0);
            player.transform.position = transform.rotation * mouvement + transform.position;
            mouvement = new Vector3(0, 7, 0);
            mouvement = transform.rotation * mouvement + transform.position;
            Camera.main.transform.position = new Vector3(mouvement.x, mouvement.y, Camera.main.transform.position.z);

            where.SetActive(true);
        }
    }
}