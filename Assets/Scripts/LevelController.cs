using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public float changeValue;
    public float changeSpeed;
    private bool disappear;
    private bool show;
    public float minValue;
    public float maxValue;
    public List<MeshRenderer> objectsWithShader;
    void Start()
    {
        changeValue = maxValue;
        ChangeMaterialsValue(changeValue);
        StartShowing();
    }
    private void Update()
    {
        if(disappear)
        {
            changeValue += changeSpeed * Time.deltaTime;
            if(changeValue > maxValue)
            {
                disappear = false;
            }
            ChangeMaterialsValue(changeValue);
        }
        if (show)
        {
            changeValue -= changeSpeed * Time.deltaTime;
            if (changeValue < minValue)
            {
                changeValue = minValue;
                show = false;
            }
            ChangeMaterialsValue(changeValue);
        }
    }
    public void ChangeMaterialsValue(float value)
    {
        foreach(MeshRenderer renderer in objectsWithShader)
        {
            renderer.material.SetFloat("_distance_parameter", value);
        }
    }
    public void StartDisappearing()
    {
        changeValue = minValue;
        disappear = true;
        show = false;
    }
    public void StartShowing()
    {
        changeValue = maxValue;
        show = true;
        disappear = false;
    }

}
