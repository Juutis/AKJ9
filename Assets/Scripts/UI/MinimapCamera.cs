using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapCamera : MonoBehaviour
{
    [SerializeField]
    private Shader minimapShader;
    private Camera minimapCamera;
    public Camera Camera { get { return minimapCamera; } }

    [SerializeField]
    private RectTransform miniMapControl;

    private Vector2 defaultSize = new Vector2(64, 36);

    void Start()
    {
        minimapCamera = GetComponentInChildren<Camera>();
        minimapCamera.SetReplacementShader(minimapShader, "RenderType");
    }

    void Update()
    {
        MoveMiniMapControl();
    }

    void MoveMiniMapControl()
    {
        Vector3 vp = minimapCamera.WorldToViewportPoint(Camera.main.transform.position);
        miniMapControl.sizeDelta = defaultSize * (Camera.main.orthographicSize / 15);
        miniMapControl.anchoredPosition = new Vector2(
            (vp.x * 200) - miniMapControl.sizeDelta.x / 2,
            (vp.y * 200) - miniMapControl.sizeDelta.y / 2
        );
        //        Debug.Log(Camera.main.transform.position);
    }

    public Vector2 MiniMapPositionToCamera()
    {
        return Camera.main.ViewportToWorldPoint(new Vector2(
            (miniMapControl.anchoredPosition.x + miniMapControl.sizeDelta.x / 2) / 200,
            (miniMapControl.anchoredPosition.y + miniMapControl.sizeDelta.y / 2) / 200
        ));
    }

}
