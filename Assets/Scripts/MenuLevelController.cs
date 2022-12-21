using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuLevelController : MonoBehaviour
{
    public GameController gameController;
    public GameObject chosenLevel;
    public GameObject lever;
    public GameObject commissionHolder;
    public List<GameObject> commisions;
    public List<Vector3> commisionsOriginalPos;
    public void Start()
    {
        foreach (Transform comision in commissionHolder.transform)
        {
            commisions.Add(comision.gameObject);
            commisionsOriginalPos.Add(comision.transform.position);
        }
    }
    public void SetupMenuLevel()// call this every time menu level is set active
    {
        //
        lever.GetComponent<LeverController>().ResetPosition();
        //
        int i = 0;
        foreach (GameObject comision in commisions)
        {
            comision.transform.position = commisionsOriginalPos[i];
            i++;
        }
        //
        chosenLevel = null;
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
