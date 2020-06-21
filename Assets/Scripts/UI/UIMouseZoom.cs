using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMouseZoom : MonoBehaviour
{
    private Transform cameraTransform;


    private float scrollSpeed = 30f;
    private float scrollMin = 5f;
    private float scrollMax = 11f;



    private Camera mapCam;
    private LayerMask mask;

    void Start()
    {
        cameraTransform = Camera.main.transform;
        mapCam = cameraTransform.GetComponentInChildren<Camera>();
        mask = LayerMask.GetMask("Water");
    }

    void Update()
    {
        if (Mathf.Abs(Input.mouseScrollDelta.y) > 0.02f)
        {
            Camera.main.orthographicSize = Mathf.Clamp(
                Camera.main.orthographicSize + -Input.mouseScrollDelta.y * scrollSpeed * Time.deltaTime,
                scrollMin,
                scrollMax
            );
        }
    }
}
