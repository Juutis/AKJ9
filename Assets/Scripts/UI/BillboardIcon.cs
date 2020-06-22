using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BillboardIcon :
    MonoBehaviour,
    IPointerEnterHandler,
    IPointerExitHandler
{

    private Transform camTransform;
    private Quaternion originalRotation;

    private Image iconImg;
    private Image background;
    private RectTransform rt;
    public RectTransform RT { get { return rt == null ? GetComponent<RectTransform>() : rt; } }

    private float iconMargin = 10f;

    private GenericDialog infoDialog;

    private string title;
    private string message;

    void Start()
    {
        rt = GetComponent<RectTransform>();
        originalRotation = transform.rotation;
        camTransform = Camera.main.transform;
    }

    public void Initialize(Sprite sprite, Vector3 pos, string infoTitle, string infoMessage)
    {
        title = infoTitle;
        message = infoMessage;
        transform.SetParent(UIManager.main.WorldSpaceCanvas);
        transform.position = pos;
        iconImg = this.FindChildObject("icon").GetComponent<Image>();
        iconImg.sprite = sprite;
        background = this.FindChildObject("background").GetComponent<Image>();
    }

    private void InitDialog()
    {
        infoDialog = Prefabs.Instantiate<GenericDialog>();
        infoDialog.Initialize(title, message, transform);
    }

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        if (infoDialog == null)
        {
            InitDialog();
        }
        infoDialog.Show();
    }
    public void OnPointerExit(PointerEventData pointerEventData)
    {
        infoDialog.Hide();
    }

    public void Die()
    {
        Destroy(gameObject);
        if (infoDialog != null) {
            Destroy(infoDialog.gameObject);
        }
    }

    void Update()
    {
        transform.rotation = originalRotation * camTransform.rotation;
    }

}
