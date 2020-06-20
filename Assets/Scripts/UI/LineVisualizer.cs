using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineVisualizer : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private float yPos = 0.1f;
    public void Initialize()
    {
        lineRenderer = GetComponentInChildren<LineRenderer>();
        SetStartPoint(new Vector3(0f, 0.1f, 0f));
        Hide();
    }

    public void SetEndPoint(Vector3 point) {
        point.y = yPos;
        lineRenderer.SetPosition(1, point);
    }

    public void SetGradient(Gradient gradient) {
        lineRenderer.colorGradient = gradient;
    }

    public void SetStartPoint(Vector3 point) {
        point.y = yPos;
        lineRenderer.SetPosition(0, point);
    }

    public void Hide() {
        lineRenderer.enabled = false;
    }
    public void Show() {
        lineRenderer.enabled = true;
    }
}
