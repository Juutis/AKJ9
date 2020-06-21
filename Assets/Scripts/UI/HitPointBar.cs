using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HitPointBar : MonoBehaviour
{
    private Text txtHp;
    private Image imgBackground;
    private Image imgForeground;
    private float maxHp;
    Transform camTransform;
    Quaternion originalRotation;

    RectTransform imgForegroundRT;
    RectTransform mainRT;
    float originalForeGroundWidth;
    Gradient hpGradient;

    public void Initialize(float initialHp)
    {
        mainRT = GetComponent<RectTransform>();
        hpGradient = Configs.main.UIStyle.HPGradient;
        maxHp = initialHp;
        txtHp = GetComponentInChildren<Text>();

        originalRotation = transform.rotation;
        camTransform = Camera.main.transform;

        imgBackground = this.FindChildObject("background").GetComponent<Image>();
        imgForeground = this.FindChildObject("foreground").GetComponent<Image>();
        imgForegroundRT = imgForeground.GetComponent<RectTransform>();
        originalForeGroundWidth = imgForegroundRT.sizeDelta.x;
        UpdateHp(initialHp);
    }

    public void SetSize(Vector2 size)
    {
        mainRT.sizeDelta = size;
        imgForegroundRT.sizeDelta = size;
        originalForeGroundWidth = imgForegroundRT.sizeDelta.x;
    }

    public void UpdatePosition(Vector3 newPos)
    {
        transform.position = newPos;
    }

    public void Die() {
        Destroy(gameObject);
    }

    public void UpdateHp(float newHp)
    {
        if (newHp <= 0)
        {
            newHp = 0;
        }
        if (txtHp.enabled) {
            txtHp.text = "{0}/{1}".Format(newHp, maxHp);
        }
        float percentage = (newHp / maxHp);

        imgForeground.color = hpGradient.Evaluate(percentage);
        imgForegroundRT.sizeDelta = new Vector2(
            percentage * originalForeGroundWidth,
            imgForegroundRT.sizeDelta.y
        );
    }

    void Update()
    {
        transform.rotation = originalRotation * camTransform.rotation;
    }
}
