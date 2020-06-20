using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMousePan : MonoBehaviour
{
    private Transform cameraTransform;

    private float speed = 15f;

    private float scrollSpeed = 25f;
    private float scrollMin = 6f;
    private float scrollMax = 10f;
    private Vector3 curVel;
    private float curScrollVel;
    Rect screenRect = new Rect(0,0, Screen.width, Screen.height);

    void Start() {
        cameraTransform = Camera.main.transform;
    }

    void Update()
    {
        Vector2 mous = Input.mousePosition - new Vector3(Screen.width / 2, Screen.height / 2, 0f);
        Vector3 pos = Camera.main.ScreenToViewportPoint(mous);
        if (pos.magnitude > 0.4f) {
            if (Mathf.Abs(pos.x) > 0.5f) {
                pos.x *= 3f;
            }
            if (Mathf.Abs(pos.y) > 0.5f) {
                pos.y *= 3f;
            }
            cameraTransform.Translate(pos * Time.deltaTime * speed, Space.Self);
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
