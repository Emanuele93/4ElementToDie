using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    public GameObject player;
    public bool verticalMouvement;
    public bool horizontalMouvement;
    public float fixedX;
    public float fixedY;
    public float speed;

    private Vector3 from;
    private Vector3 to;

    void Start()
    {
        verticalMouvement = horizontalMouvement = true;
    }

    void FixedUpdate()
    {
        from = transform.position;
        if (verticalMouvement && horizontalMouvement)
        {
            to = player.transform.position;
            to.z = transform.position.z;
        }
        else if (verticalMouvement)
        {
            to = new Vector3(fixedX, player.transform.position.y, transform.position.z);
        }
        else if (horizontalMouvement)
        {
            to = new Vector3(player.transform.position.x, fixedY,transform.position.z);
        }
        else
        {
            to = new Vector3(fixedX, fixedY, transform.position.z);
        }
        transform.position = Vector3.MoveTowards(from, to, speed * Time.deltaTime);

        speed = player.GetComponent<Player>().getPleyerSpeed() * 1.1f;
    }
}