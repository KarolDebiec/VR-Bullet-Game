using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightController : MonoBehaviour
{
    public GameObject pistol;
    public GameObject hand;
    public bool dominantHand = true;

    public void ToHand()
    {
        if(dominantHand)
        {
            pistol.SetActive(false);
            hand.SetActive(true);
        }
        else
        {
            pistol.SetActive(false);
            hand.SetActive(true);
        }
    }
    public void ToPistol()
    {
        if (dominantHand)
        {
            pistol.SetActive(true);
            hand.SetActive(false);
        }
        else
        {
            pistol.SetActive(false);
            hand.SetActive(true);
        }
    }
    public void UpdateController()
    {
        if (pistol.activeSelf)
        {
            ToPistol();
        }
        else
        {
            ToHand();
        }
    }
}
