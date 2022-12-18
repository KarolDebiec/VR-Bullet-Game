using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    public Vector3 targetShape; // animation is based on x therefore targertShape x needs to always be less than originalShape x
    private Vector3 originalShape;
    public float animationSpeed;
    public bool isJiggle;
    private int jigglePhase;
    public Vector3 stepScale;
    public void Start()
    {
        originalShape = gameObject.transform.localScale;
        stepScale = (originalShape - targetShape) * animationSpeed;
    }
    public void Update()
    {
        if(isJiggle)
        {
            if(jigglePhase == 0)
            {
                gameObject.transform.localScale -= stepScale;
                /*if(gameObject.transform.localScale.sqrMagnitude <= targetShape.sqrMagnitude)
                {
                    jigglePhase = 1;
                }*/
                if (gameObject.transform.localScale.x <= targetShape.x)
                {
                    jigglePhase = 1;
                }
            }
            else if (jigglePhase == 1)
            {
                gameObject.transform.localScale += stepScale;
                if (gameObject.transform.localScale.x >= originalShape.x)
                {
                    isJiggle = false;
                }
            }
        }
    }
    public void JiggleAnimation()
    {
        originalShape = gameObject.transform.localScale;
        jigglePhase = 0;
        isJiggle = true;
        Debug.Log("jiggle wiggle wiggle");
    }

   public void ObstacleHit()
    {
        JiggleAnimation();
    }
}
