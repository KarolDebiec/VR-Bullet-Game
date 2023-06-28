using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolController : MonoBehaviour
{
    public GameObject redDisplayBar;
    public GameObject greenDisplayBar;
    private GameController gameController;
    public Renderer light1;
    public Renderer light2;
    public Renderer light3;
    public float lightChangeSpeed = 1.7f;
    public bool light1Active = true;
    public bool light2Active = true;
    public bool light3Active = true;
    public int bulletsAmount;
    public Renderer distanceIndicator;

    void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        ResetPistol();
    }

    void Update()
    {
        if(gameController.virtualBulletController != null)
        {
            SetDistanceDisplay(gameController.bulletDistance);
        }
        if(!light1Active)
        {
            ChangeBulletLightMaterialsValue(1, light1.material.GetFloat("_ColorMode") - Time.deltaTime * lightChangeSpeed);
            if(light1.material.GetFloat("_ColorMode") < 0)
            {
                ChangeBulletLightMaterialsValue(1, 0);
                light1Active = true;
            }
        }
        if (!light2Active)
        {
            ChangeBulletLightMaterialsValue(2, light2.material.GetFloat("_ColorMode") - Time.deltaTime * lightChangeSpeed);
            if (light2.material.GetFloat("_ColorMode") < 0)
            {
                ChangeBulletLightMaterialsValue(2, 0);
                light2Active = true;
            }
        }
        if (!light3Active)
        {
            ChangeBulletLightMaterialsValue(3, light3.material.GetFloat("_ColorMode") - Time.deltaTime * lightChangeSpeed);
            if (light3.material.GetFloat("_ColorMode") < 0)
            {
                ChangeBulletLightMaterialsValue(3, 0);
                light3Active = true;
            }
        }
    }
    public void ResetPistol()
    {
        if (gameController==null)
        {
            gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        }
        SetDistanceDisplay(1);
        bulletsAmount = gameController.maxBulletsAmount;
        //SetDistanceDisplay(gameController.bulletDistance);
        ChangeBulletLightMaterialsValue(1, 1);
        ChangeBulletLightMaterialsValue(2, 1);
        ChangeBulletLightMaterialsValue(3, 1);
        light1Active = true;
        light2Active = true;
        light3Active = true;
    }
    public void PistolShot()
    {
        if(bulletsAmount == 3)
        {
            bulletsAmount--;
            light1Active = false;
        }
        else if (bulletsAmount == 2)
        {
            bulletsAmount--;
            light2Active = false;
        }
        else if (bulletsAmount == 1)
        {
            bulletsAmount--;
            light3Active = false;
        }
    }
    public void SetDistanceDisplay(float value) //value refers to a number from 0 to 1
    {
        /*if (value < 1)
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
        }*/
        if (value < 1)
        {
            float BottomScale = -((value *2) -1);
            distanceIndicator.material.SetFloat("_Distance_Traveled", BottomScale);
            //redDisplayBar.transform.localScale = new Vector3(2f - BottomScale, 1, 1);
            //greenDisplayBar.transform.localScale = new Vector3(BottomScale, 1, 1);
        }
        else if (value <= 0)
        {
            distanceIndicator.material.SetFloat("_Distance_Traveled", 1);
        }
        else
        {
            distanceIndicator.material.SetFloat("_Distance_Traveled", -1);
        }
    }

    public void ChangeBulletLightMaterialsValue(int lightNumber,float value)
    {
        if(lightNumber == 1)
        {
            light1.material.SetFloat("_ColorMode", value);
        }
        else if (lightNumber == 2)
        {
            light2.material.SetFloat("_ColorMode", value);
        }
        else if (lightNumber == 3)
        {
            light3.material.SetFloat("_ColorMode", value);
        }
        
    }
}
