using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleCarMovement : MonoBehaviour
{
    public float speed;
    public float maxBound;
    public float minBound;
    public float movementOnTurn;
    void Update()
    {
        if(transform.position.x <= minBound)
        {
            transform.position = new Vector3(minBound+0.2f, transform.position.y, transform.position.z);
            transform.RotateAround(transform.position, Vector3.up, 180);
            transform.Translate(Vector3.right * movementOnTurn);
        }
        else if(transform.position.x >= maxBound)
        {
            transform.position = new Vector3(maxBound - 0.2f, transform.position.y, transform.position.z);
            transform.RotateAround(transform.position, Vector3.up, 180);
            transform.Translate(Vector3.right * movementOnTurn);
        }
        transform.Translate(-Vector3.up * speed * Time.deltaTime);
    }
}
