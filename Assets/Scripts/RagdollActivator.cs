using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollActivator : MonoBehaviour
{
    public List<Rigidbody> allRigidbodies;
    public void ActivateRagdoll()
    {
        gameObject.GetComponent<Animator>().enabled = false;
        foreach(Rigidbody rb in allRigidbodies)
        {
            rb.useGravity = true;
        }
    }
}
