using UnityEngine;
using System.Collections;

public class colliderCameraMenager : MonoBehaviour
{
    public CameraController cam;
    public bool vertical;
    public bool horizontal;

    void Start()
    {

    }

    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            cam.verticalMouvement = vertical;
            cam.horizontalMouvement = horizontal;
            cam.fixedX = transform.parent.position.x;
            cam.fixedY = transform.parent.position.y;
        }
    }
}