using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public RightController rightController;
    public LeftController leftController;
    public int mode = 0; // 0 - right handed, 1 - left handed

    public void SwitchHands()
    {
        if(mode == 0)
        {
            rightController.dominantHand = false;
            leftController.dominantHand = true;
            rightController.UpdateController(); 
            leftController.UpdateController();
            mode = 1;
        }
        else
        {
            rightController.dominantHand = true;
            leftController.dominantHand = false;
            rightController.UpdateController();
            leftController.UpdateController();
            mode = 0;
        }
    }
    public void ControllersToHands()
    {
        rightController.ToHand();
        leftController.ToHand();
    }
    public void ControllersToPistol()
    {
        rightController.ToPistol();
        leftController.ToPistol();
    }
}
