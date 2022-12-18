using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuLevelController : MonoBehaviour
{
    public GameController gameController;
    public GameObject chosenLevel;
    public GameObject lever;
    public void SetupMenuLevel()// call this every time menu level is set active
    {
        //
        lever.GetComponent<LeverController>().ResetPosition();
        chosenLevel = null;
        //

        Debug.Log("menu level has beem setup");
    }

    public void ActivatedCommissionBox()
    {
        if(chosenLevel != null)
        {
            gameController.LoadLevel(chosenLevel);
        }
    }
}
