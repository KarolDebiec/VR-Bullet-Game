using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public BulletController virtualBullet;
    public RealBulletController realBullet;

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

    public void GetTrackToRealBullet()
    {
        realBullet.bulletPositions = virtualBullet.bulletPositions;
        realBullet.bulletRotations = virtualBullet.bulletRotations;
    }
}
