using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolController : MonoBehaviour
{
    public GameObject redDisplayBar;
    public GameObject greenDisplayBar;
    private GameController gameController;
    // Start is called before the first frame update
    void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(gameController.virtualBulletController != null)
        {
            SetDistanceDisplay(gameController.bulletDistance);
        }
    }
    public void SetDistanceDisplay(float value) //value refers to a number from 0 to 1
    {
        if (value < 1)
        {
            float BottomScale = value * 2;
            redDisplayBar.transform.localScale = new Vector3(2f - BottomScale, 1, 1);
            greenDisplayBar.transform.localScale = new Vector3(BottomScale, 1, 1);
        }
        else if(value <= 0)
        {
            redDisplayBar.transform.localScale = new Vector3(2, 1, 1);
            greenDisplayBar.transform.localScale = new Vector3(0, 1, 1);
        }
        else
        {
            redDisplayBar.transform.localScale = new Vector3(0, 1, 1);
            greenDisplayBar.transform.localScale = new Vector3(2, 1, 1);
        }
    }
}
