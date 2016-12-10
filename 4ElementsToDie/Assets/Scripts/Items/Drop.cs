using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(CircleCollider2D))]
public class Drop : MonoBehaviour
{
    Rigidbody2D rb;
    CircleCollider2D col;
    private float m_explosionSpeed = 3f;
    public Vector3 direction;

    public Item item;
    public bool shouldMove = false;

    Transform tr;
    // Use this for initialization
    void Start()
    {
        tr = GetComponent<Transform>() as Transform;

        // The item isn't affected by gravity
        rb = GetComponent<Rigidbody2D>() as Rigidbody2D;
        rb.gravityScale = 0;

        // Nothing is triggered on collision.
        col = GetComponent<CircleCollider2D>() as CircleCollider2D;
        col.isTrigger = false;
    }
    
    // Update is called once per frame
    void Update()
    {
        if (shouldMove)
        {
            tr.position += direction * m_explosionSpeed * Time.fixedDeltaTime;
            /*
            tr.Translate(
                m_explosionSpeed * Time.fixedDeltaTime,
                m_explosionSpeed * Time.fixedDeltaTime,
                0
            );
            */
        }

        // rotation around Y axis
        //tr.RotateAround(tr.position, tr.up, Time.deltaTime * m_rotationSpeed);
    }
}
