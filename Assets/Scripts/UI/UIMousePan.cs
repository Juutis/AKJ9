using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMousePan : MonoBehaviour
{
    private Transform cameraTransform;

    private float speed = 15f;
    private float scrollSpeed = 30f;
    private float scrollMin = 5f;
    private float scrollMax = 11f;

    private float border = 20f;

    void Start() {
        cameraTransform = Camera.main.transform;
    }

    void Update()
    {
        Vector2 dir = Vector2.zero;
        Vector2 mous = Input.mousePosition;
        if (mous.x > Screen.width / 2) {
            if (mous.x > Screen.width || Mathf.Abs(Screen.width - mous.x) <= border) {
                dir.x = 1f;
            }
        } else if (mous.x <= border) {
            dir.x = -1f;
        }
        if (mous.y > Screen.height / 2) {
            if (mous.y > Screen.height || Mathf.Abs(Screen.height - mous.y) <= border) {
                dir.y = 1f;
            }
        } else if (mous.y <= border) {
            dir.y = -1f;
        }
        if (dir.magnitude > 0.1f) {
            cameraTransform.Translate(dir * Time.deltaTime * speed, Space.Self);
        }
        if (Mathf.Abs(Input.mouseScrollDelta.y) > 0.02f) {
            Camera.main.orthographicSize = Mathf.Clamp(
                Camera.main.orthographicSize + -Input.mouseScrollDelta.y * scrollSpeed * Time.deltaTime,
                scrollMin,
                scrollMax
            );
        }
    }
}
