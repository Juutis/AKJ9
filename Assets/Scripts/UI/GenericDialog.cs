using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GenericDialog : MonoBehaviour
{
    private RectTransform rectTransform;
    private Text txtMessage;
    private Text txtTitle;
    private GameObject container;
    private RectTransform worldRT;

    private Transform target;

    public void Initialize(string title, string message, Transform target)
    {
        this.target = target;
        transform.SetParent(UIManager.main.UICanvas);
        rectTransform = GetComponent<RectTransform>();
        worldRT = UIManager.main.UICanvas.GetComponent<RectTransform>();
        container = this.FindChildObject("container").gameObject;
        txtMessage = this.FindChildObject("message").GetComponent<Text>();
        txtTitle = this.FindChildObject("title").GetComponent<Text>();
        txtMessage.text = message;
        txtTitle.text = title;
        UpdatePosition();
    }

    public void Initialize(string title, string message, Transform target, Vector2 size)
    {
        Initialize(message, title, target);
        rectTransform.sizeDelta = size;
    }
    
    
    public void UpdatePosition() {
        Vector2 viewportPoint = Camera.main.WorldToViewportPoint(target.position);
        rectTransform.anchorMin = viewportPoint;
        rectTransform.anchorMax = viewportPoint;
    }
    

    public void Show()
    {
        UpdatePosition();
        container.SetActive(true);
    }
    public void Hide()
    {
        container.SetActive(false);
    }
}
