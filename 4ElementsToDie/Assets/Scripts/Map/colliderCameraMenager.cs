using UnityEngine;
using System.Collections;

public class colliderCameraMenager : MonoBehaviour
{
    private CameraController cam;
    public bool vertical;
    public bool horizontal;

    void Start()
    {
        cam = Camera.main.GetComponent<CameraController>();
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