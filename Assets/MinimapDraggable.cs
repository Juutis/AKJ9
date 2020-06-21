using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MinimapDraggable : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler, IDragHandler, IEndDragHandler
{

    [SerializeField]
    private RectTransform miniMapControl;
    private Transform cameraTransform;

    private LayerMask mask;

    private Vector2 prevClickPos;

    private bool pointerDown = false;

    void Start()
    {
        cameraTransform = Camera.main.transform;
        mask = LayerMask.GetMask("Water");
    }

    void Update() {
        Vector2 dir = GetDir(prevClickPos);
        if (dir.magnitude > 0.01f && pointerDown) {
            cameraTransform.Translate(dir * 100, Space.Self);
        }
    }

    private Vector2 GetDir(Vector2 mousePos) {
        Vector2 mouseOnMiniMap = new Vector2(
            (mousePos.x - 20) / 200f,
            (mousePos.y - 20) / 200f
        );
        Vector2 minimapControlPos = new Vector2(
            (miniMapControl.anchoredPosition.x + miniMapControl.sizeDelta.x / 2) / 200f,
            (miniMapControl.anchoredPosition.y + miniMapControl.sizeDelta.y / 2) / 200f
        );
        return mouseOnMiniMap - minimapControlPos;
    }

    public void OnDrag(PointerEventData data) {
        pointerDown = true;
        prevClickPos = data.position;
    }

   public void OnEndDrag(PointerEventData data) {
        pointerDown = false;
        prevClickPos = data.position;
    }

    public void OnPointerUp(PointerEventData pointerEventData) {
        pointerDown = false;
    }
    public void OnPointerDown(PointerEventData pointerEventData) {
        pointerDown = true;
        prevClickPos = pointerEventData.position;
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        pointerDown = true;
        prevClickPos = pointerEventData.position;
    }
}
