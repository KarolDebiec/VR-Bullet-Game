using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BullethitController : MonoBehaviour
{
    public float livingTime = 1f;
   
    void Update()
    {
        livingTime -= Time.deltaTime;
        if (livingTime < 0)
        {
            Destroy(gameObject);
        }
    }
}
