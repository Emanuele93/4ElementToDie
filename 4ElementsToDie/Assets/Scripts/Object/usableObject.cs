using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class usableObject : MonoBehaviour
{

    public bool shouldMove;
    public Vector3 direction;
    private float m_explosionSpeed = 3f;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (shouldMove)
            transform.position += direction * m_explosionSpeed * Time.fixedDeltaTime;
    }
}
