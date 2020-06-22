using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Squiggle : MonoBehaviour
{
    private float speed = 1f;
    private Material squiggleMaterial;
    private Vector2 originalOffset = Vector2.zero;
    private Vector2 currentOffset;
    private LineRenderer lineRenderer;
    private bool isActive = false;
    public void Initialize(Vector3 start, Vector3 end, Color color)
    {
        lineRenderer = GetComponentInChildren<LineRenderer>();
        squiggleMaterial = lineRenderer.material;
        start.y = 0.3f;
        end.y = 0.3f;
        lineRenderer.SetPosition(0, start);
        lineRenderer.SetPosition(1, end);
        currentOffset = originalOffset;
        lineRenderer.enabled = true;
        isActive = true;

        color.a = 0.5f;
        squiggleMaterial.color = color;
        squiggleMaterial.mainTextureScale = new Vector2(Vector3.Distance(start, end), 1);
    }

    public void Hide() {
        isActive = false;
        lineRenderer.enabled = false;
    }

    void Update()
    {
        if (isActive) {
            currentOffset.x -= speed * Time.deltaTime;
            squiggleMaterial.SetTextureOffset("_MainTex", currentOffset);
        }
    }
}
