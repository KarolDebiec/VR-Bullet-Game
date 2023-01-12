using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RealBulletController : MonoBehaviour
{
    public float bulletSpeed;
    public float bulletRotationSpeed;
    public GameObject hitParticleEffectPrefab;

    public List<Vector3> bulletPositions; // holds a progressive path over time of the bullet
    public List<Quaternion> bulletRotations; // holds a rotaiton over time of the bullet
    public bool startReplay;
    public int posIndex = 0;

    public GameController gameController;

    public GameObject track;

    public GameObject redLine;
    private GameObject redLineInstance;
    private LineController redLineController;
    public GameObject greenLine;
    private GameObject greenLineInstance;
    private LineController greenLineController;
    private GameObject bulletTrail;


    public GameObject bullethitSoundEffect;
    void Start()
    {
        gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
        bulletTrail = gameController.bulletTrail;
        gameController.ClearBulletTrail();
        greenLineInstance = Instantiate(greenLine, new Vector3(0, 0, 0), Quaternion.identity);
        greenLineInstance.transform.parent = bulletTrail.transform;
        greenLineController = greenLineInstance.GetComponent<LineController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(startReplay)
        {
            if(gameObject.transform.position == bulletPositions[posIndex])
            {
                greenLineController.AddPoint(bulletPositions[posIndex]);
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
        Debug.Log(other.name + " hit");
        if(other.tag =="Target")
        {
            Debug.Log("you win!!");
            GameObject hitParticleEffect = Instantiate(hitParticleEffectPrefab, this.transform.position, this.transform.rotation);
            Instantiate(bullethitSoundEffect, this.transform.position, this.transform.rotation);
            gameController.targetHit();
            BulletEnded();
        }
        else if (other.tag == "Controller")
        {

        }
        else if (other.tag == "Obstacle")
        {
            GameObject hitParticleEffect = Instantiate(hitParticleEffectPrefab, this.transform.position, this.transform.rotation);
            Instantiate(bullethitSoundEffect, this.transform.position, this.transform.rotation);

            redLineInstance = Instantiate(redLine, new Vector3(0, 0, 0), Quaternion.identity);
            redLineInstance.transform.parent = bulletTrail.transform;
            redLineController = redLineInstance.GetComponent<LineController>();
            int bulletsAmount = bulletPositions.Count;
            redLineController.AddPoint(bulletPositions[posIndex-1]);
            for (int i = posIndex; i < bulletsAmount; i++)
            {
                redLineController.AddPoint(bulletPositions[i]);
            }
            other.GetComponent<ObstacleController>().ObstacleHit();
            gameController.obstacleHit();
            BulletEnded();
        }
        else
        {
            Debug.Log("you lose!");
            Instantiate(bullethitSoundEffect, this.transform.position, this.transform.rotation);
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
