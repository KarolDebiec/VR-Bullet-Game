using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseBoxController : MonoBehaviour
{
    public MenuLevelController menuLevelController;
    private CommissionScript commissionScript;

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Commission")
        {
            Debug.Log("nowa komisja");
            commissionScript = other.GetComponent<CommissionScript>();
            menuLevelController.chosenLevel = commissionScript.commissionLevel;
           
        }
    }
    public void PulledLever()
    {
        menuLevelController.ActivatedCommissionBox();
    }


}
