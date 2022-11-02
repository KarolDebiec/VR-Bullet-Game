using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float speed;
    public Vector3 dir;
    public float maxSpeed = 10f;
    public float minSpeed = 1f;

    public float distance=10f;

    public List<Vector3> bulletPositions; // holds a progressive path over time of the bullet
    public List<Quaternion> bulletRotations; // holds a rotaiton over time of the bullet

    private float time = 0f;

    private bool fired = true;
    private bool ended;

    public GameController gameController;

    public GameObject dotsContainer;
    public GameObject dot;
    // Start is called before the first frame update
    void Start()
    {
        // setup origin
        bulletPositions.Add(gameObject.transform.position);
        bulletRotations.Add(gameObject.transform.localRotation);
    }

    // Update is called once per frame
    void Update()
    {
        if(distance > 0 && fired)
        {
            distance -= speed * Time.deltaTime;
            transform.Rotate(dir * speed * Time.deltaTime);
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
            time += Time.deltaTime;
            if (time > 0.3f)
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
            gameController.GetTrackToRealBullet();
        }
    }

    public void FireVirtualBullet()
    {
        fired = true;
    }
}
