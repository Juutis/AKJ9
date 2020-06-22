using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void HasAnimated();
public class LineVisualizer : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private float yPos = 0.1f;

    private bool animate = false;
    private bool isAnimating = false;
    
    private Vector3 targetEndPoint;
    private Vector3 curPos;
    private float speed = 5f;

    private HasAnimated callback;
    public void Initialize()
    {
        lineRenderer = GetComponentInChildren<LineRenderer>();
        SetStartPoint(new Vector3(0f, 0.1f, 0f));
        Hide();
    }

    public void Initialize(HasAnimated callback)
    {
        Initialize();
        this.callback = callback;
        animate = true;
    }

    public void SetEndPoint(Vector3 point)
    {
        point.y = yPos;
        if (animate)
        {
            curPos = lineRenderer.GetPosition(0);
            targetEndPoint = point;
            isAnimating = true;
        }
        else
        {
            lineRenderer.SetPosition(1, point);
        }
    }

    public void SetGradient(Gradient gradient)
    {
        lineRenderer.colorGradient = gradient;
    }

    public void SetStartPoint(Vector3 point)
    {
        point.y = yPos;
        lineRenderer.SetPosition(0, point);
    }

    public void Hide()
    {
        lineRenderer.enabled = false;
        isAnimating = false;
    }
    public void Show()
    {
        lineRenderer.enabled = true;
    }

    void Update()
    {
        if (isAnimating)
        {
            curPos = Vector3.MoveTowards(curPos, targetEndPoint, Time.deltaTime * speed);
            if (Vector3.Distance(curPos, targetEndPoint) <= 0.01f) {
                isAnimating = false;
                lineRenderer.SetPosition(1, targetEndPoint);
                callback();
            } else {
                lineRenderer.SetPosition(1, curPos);
            }
        }
    }
}
