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

    public GameController gameController;
    void Start()
    {
        gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
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
                BulletEnded();
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, bulletPositions[posIndex], bulletSpeed * Time.deltaTime);
                transform.rotation = Quaternion.Slerp(transform.rotation, bulletRotations[posIndex], bulletRotationSpeed * Time.deltaTime);
            }
        }    
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("something hit");
        if(other.tag =="Target")
        {
            Debug.Log("you win!!");
            gameController.targetHit();
            BulletEnded();
        }
        else
        {
            Debug.Log("you lose!");
            gameController.obstacleHit();
            BulletEnded();
        }
    }
    void BulletEnded()
    {
        gameController.clearDots();
        gameController.virtualBulletController.Dest();
        gameController.canFireVirtBullet = true;
        Destroy(gameObject);
    }
}
