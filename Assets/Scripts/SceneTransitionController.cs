using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneTransitionController : MonoBehaviour
{
    public GameObject currentLevel;
    public GameObject nextLevel;
    public float trasitionSpeed = 2f;

    private Image theImage;
    private bool shouldReveal = true;

    // Start is called before the first frame update
    void Start()
    {
        theImage = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
    //    if(Input.GetKeyDown(KeyCode.S))
    //    {
    //        if (shouldReveal) shouldReveal = false;
    //        else if (!shouldReveal) shouldReveal = true;
    //    }
    //    if (shouldReveal)
    //    {
    //        theImage.material.SetFloat("_Cutoff", Mathf.MoveTowards(theImage.material.GetFloat("_Cutoff"), 1.1f, trasitionSpeed * Time.deltaTime));
    //    }
    //    if (!shouldReveal)
    //    {
    //        theImage.material.SetFloat("_Cutoff", Mathf.MoveTowards(theImage.material.GetFloat("_Cutoff"), -0.1f - theImage.material.GetFloat("_EdgeSmoothing"), trasitionSpeed * Time.deltaTime));

    //        if(theImage.material.GetFloat("_Cutoff") == -0.1f - theImage.material.GetFloat("_EdgeSmoothing"))
    //        {
    //            currentLevel.SetActive(false);
    //            Instantiate(nextLevel);
    //            //nextLevel.SetActive(true);
    //        }
    //    }
    }
}
