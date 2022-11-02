using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RealBulletController : MonoBehaviour
{
    public float bulletSpeed;
    public float bulletRotationSpeed;

    public List<Vector3> bulletPositions; // holds a progressive path over time of the bullet
    public List<Quaternion> bulletRotations; // holds a rotaiton over time of the bullet
    public bool startReplay;
    public int posIndex = 0;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(startReplay)
        {
            if(gameObject.transform.position == bulletPositions[posIndex])
            {
                posIndex++;
            }
            if(posIndex >= bulletPositions.Count)
            {
                startReplay = false;
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, bulletPositions[posIndex], bulletSpeed * Time.deltaTime);
            }
        }    
    }
}
