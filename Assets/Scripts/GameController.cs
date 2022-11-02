using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public BulletController virtualBullet;
    public RealBulletController realBullet;
    public List<Vector3> bulletPositions; // holds a progressive path over time of the bullet
    public List<Quaternion> bulletRotations; // holds a rotaiton over time of the bullet

    public bool testBool;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(testBool)
        {
            GetTrackToRealBullet();
            testBool = false;
        }
        
    }

    void GetTrackToRealBullet()
    {
        realBullet.bulletPositions = virtualBullet.bulletPositions;
        realBullet.bulletRotations = virtualBullet.bulletRotations;
    }
}
