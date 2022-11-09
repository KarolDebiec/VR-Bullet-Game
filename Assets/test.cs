using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    public float X;
    public float Y;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(X, Y, 0);
        /*transform.Rotate(X,Y,0);
        Quaternion target = Quaternion.Euler(X, Y, 0);

        transform.rotation = Quaternion.Slerp(transform.rotation, target,  Time.deltaTime);*/
    }
}
