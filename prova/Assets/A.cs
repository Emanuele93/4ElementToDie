using UnityEngine;
using System.Collections;

public abstract class A : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        cacca();
        pippo();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void cacca()
    {
        Debug.Log("cacca");
    }
    protected abstract void pippo();
}
