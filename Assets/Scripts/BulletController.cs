using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float speed;
    public Vector3 dir;
    private Vector3 originalDir;
    public float maxSpeed = 10f;
    public float minSpeed = 1f;

    public float rotationStrenght = 0.4f;

    public float distance=10f;

    public List<Vector3> bulletPositions; // holds a progressive path over time of the bullet
    public List<Quaternion> bulletRotations; // holds a rotaiton over time of the bullet

    private float time = 0f;

    private bool fired = true;
    private bool ended;

    public GameController gameController;

    public GameObject dotsContainer;
    public GameObject dot;

    public GameObject rightController;
    
    // Start is called before the first frame update
    void Start()
    {
        gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
        rightController = gameController.rightController;
        dotsContainer = gameController.dotsContainer;
        gameController.originPos = transform.position;
        originalDir = new Vector3(rightController.transform.localRotation.eulerAngles.x, rightController.transform.localRotation.eulerAngles.y,0);
        // setup origin
        bulletPositions.Add(gameObject.transform.position);
        bulletRotations.Add(gameObject.transform.localRotation);
    }

    // Update is called once per frame
    void Update()
    {
        if(distance > 0 && fired)
        {
            dir = (new Vector3(rightController.transform.localRotation.eulerAngles.x, rightController.transform.localRotation.eulerAngles.y, 0) )* rotationStrenght;
            distance -= speed * Time.deltaTime;
            //transform.Rotate(dir * speed * Time.deltaTime);
            transform.rotation = Quaternion.Euler(rightController.transform.localRotation.eulerAngles.x, rightController.transform.localRotation.eulerAngles.y, 0);
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
            time += Time.deltaTime;
            if (time > 0.12f)
            {
                time = 0;
                bulletPositions.Add(gameObject.transform.position);
                bulletRotations.Add(gameObject.transform.localRotation);
                GameObject dotty = Instantiate(dot, gameObject.transform.position, Quaternion.identity);
                dotty.transform.parent = dotsContainer.transform;
            }
        }
        else if (!ended)
        {
            fired = false;
            ended = true;
            bulletPositions.Add(gameObject.transform.position);
            bulletRotations.Add(gameObject.transform.localRotation);
            gameController.VirtualBulletStopped();
        }
    }

    public void FireVirtualBullet()
    {
        fired = true;
    }
    public void Dest()
    {
        Destroy(gameObject);
    }
}
