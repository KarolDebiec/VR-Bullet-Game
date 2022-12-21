using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverController : MonoBehaviour
{
    /*
    obiekt ktory jest pivotem 
    dzwignia ma collider i jak go zlapie reka to liczy odlegosc reki od
    albo look at albo odlgelosc punktu od pivota
    */
    public GameObject target;
    public GameObject targetRightHand;
    public Vector3 targetPosition;
    public Vector3 diffRotation;
    public float LeverBound;
    //public float MaxRotationBound;
    //public float MinRotationBound;
    public bool tracking = false;
    public ChooseBoxController chooseBoxController;

    private void Update()
    {
        if (tracking)
        {
            targetPosition = new Vector3(target.transform.position.x, target.transform.position.y, gameObject.transform.position.z);
            gameObject.transform.LookAt(targetPosition, Vector3.up);
            gameObject.transform.eulerAngles = gameObject.transform.eulerAngles - diffRotation;
            if (gameObject.transform.localRotation.z > LeverBound)
            {
                Debug.Log("lever przekrecilo");
                ResetPosition();
                //gameObject.transform.localRotation = Quaternion.identity;
                chooseBoxController.PulledLever();
                tracking = false;
            }
        }
    }

    public void ResetPosition()
    {
        gameObject.transform.localRotation = Quaternion.identity;
    }
    public void StartTrackHand()
    {
        Debug.Log("zalapo cos lever");
        target = targetRightHand;
        tracking = true;
    }
    public void StopTrackHand()
    {
        Debug.Log("puszczono lever");
        tracking = false;
    }
    
    public void DebugTest()
    {
        Debug.Log("zalapo cos lever");
    }
}
