using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineController : MonoBehaviour
{
    private LineRenderer lineRenderer;
    void Start()
    {
        lineRenderer = gameObject.GetComponent<LineRenderer>();
    }

    public void AddPoint(Vector3 point)
    {
        if(lineRenderer==null)
        {
            lineRenderer = gameObject.GetComponent<LineRenderer>();
        }
        lineRenderer.positionCount = lineRenderer.positionCount + 1;
        lineRenderer.SetPosition(lineRenderer.positionCount-1,point);
    }
}
