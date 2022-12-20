using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTargetController : MonoBehaviour
{
    public float boundaryX;
    public float boundaryY;
    public Vector3 dir;
    public float speed = 5f;
    // Start is called before the first frame update
    void Start()
    {
        //transform.position = new Vector3(boundaryX, 1, boundaryY);
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x <= -boundaryX)
        {
            dir = Vector3.right;
        }
        if (transform.position.x >= boundaryX)
        {
            dir = Vector3.left;
        }

       /* if (transform.position.z >= boundaryY && transform.position.x >= boundaryX)
        {
            dir = Vector3.right;
        }
        if (transform.position.z >= boundaryY && transform.position.x <= -boundaryX)
        {
            dir = Vector3.forward;
        }
        if (transform.position.z <= -boundaryY && transform.position.x >= boundaryX)
        {
            dir = Vector3.back;
        }
        if (transform.position.z <= -boundaryY && transform.position.x <= -boundaryX)
        {
            dir = Vector3.left;
        }*/


        transform.Translate(dir * speed * Time.deltaTime);
    }
}
