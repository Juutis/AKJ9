using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MinimapDraggable :
    MonoBehaviour,
    IPointerClickHandler,
    IPointerDownHandler,
    IPointerUpHandler,
    IDragHandler,
    IEndDragHandler
{

    [SerializeField]
    private RectTransform miniMapControl;
    private Transform cameraTransform;

    private float speed = 25f;
    private LayerMask mask;

    private Vector2 prevClickPos;

    private bool pointerDown = false;
    private float border = 20f;


    void Start()
    {
        cameraTransform = Camera.main.transform;
        mask = LayerMask.GetMask("Water");
    }

    void Update()
    {
        Vector2 mousePanDir = GetMousePanDir();
        Vector2 mouseDragDir = GetDir(GetMouseonMinimap(prevClickPos));
        if (mousePanDir.magnitude > 0.01f)
        {
            cameraTransform.Translate(mousePanDir * Time.deltaTime * speed, Space.Self);
        }
        else
        {
            if (mouseDragDir.magnitude > 0.01f && pointerDown)
            {
                cameraTransform.Translate(mouseDragDir * 1000f * Time.deltaTime, Space.Self);
            }
        }
        if (mouseDragDir.magnitude < 0.01f && pointerDown)
        {
            pointerDown = false;
        }
    }

    private Vector2 GetMousePanDir()
    {
        Vector2 dir = Vector2.zero;
        Vector2 mous = Input.mousePosition;
        Vector2 minimapControlPos = new Vector2(
            (miniMapControl.anchoredPosition.x + miniMapControl.sizeDelta.x / 2) / 200f,
            (miniMapControl.anchoredPosition.y + miniMapControl.sizeDelta.y / 2) / 200f
        );
        
        if (mous.x > Screen.width / 2 && minimapControlPos.x <= 1)
        {
            if (mous.x > Screen.width || Mathf.Abs(Screen.width - mous.x) <= border)
            {
                dir.x = 1f;
            }
        }
        else if (mous.x <= border && minimapControlPos.x >= 0)
        {
            dir.x = -1f;
        }
        if (mous.y > Screen.height / 2 && minimapControlPos.y <= 1)
        {
            if (mous.y > Screen.height || Mathf.Abs(Screen.height - mous.y) <= border)
            {
                dir.y = 1f;
            }
        }
        else if (mous.y <= border && minimapControlPos.y >= 0)
        {
            dir.y = -1f;
        }
        return dir;
    }
    private Vector2 GetMouseonMinimap(Vector2 mousePos)
    {
        Vector2 mouseOnMiniMap = new Vector2(
            (mousePos.x - 20) / 200f,
            (mousePos.y - 20) / 200f
        );
        return mouseOnMiniMap;
    }

    private Vector2 GetDir(Vector2 mouseOnMiniMap) {
        Vector2 minimapControlPos = new Vector2(
            (miniMapControl.anchoredPosition.x + miniMapControl.sizeDelta.x / 2) / 200f,
            (miniMapControl.anchoredPosition.y + miniMapControl.sizeDelta.y / 2) / 200f
        );
        mouseOnMiniMap = new Vector2(
            Mathf.Clamp(mouseOnMiniMap.x, 0f, 1f),
            Mathf.Clamp(mouseOnMiniMap.y, 0f, 1f)
        );
        minimapControlPos = new Vector2(
            Mathf.Clamp(minimapControlPos.x, 0f, 1f),
            Mathf.Clamp(minimapControlPos.y, 0f, 1f)
        );
        Vector2 ret = (mouseOnMiniMap - minimapControlPos);
        return ret;
    }

    public void OnDrag(PointerEventData data)
    {
        pointerDown = true;
        prevClickPos = data.position;
    }

    public void OnEndDrag(PointerEventData data)
    {
        pointerDown = false;
        prevClickPos = data.position;
    }

    public void OnPointerUp(PointerEventData pointerEventData)
    {
        pointerDown = false;
    }
    public void OnPointerDown(PointerEventData pointerEventData)
    {
        pointerDown = true;
        prevClickPos = pointerEventData.position;
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        pointerDown = true;
        prevClickPos = pointerEventData.position;
    }
}
