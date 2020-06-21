using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverIndicator : MonoBehaviour
{
    private SpriteRenderer fill;
    private SpriteRenderer border;

    private Color originalFillColor;
    private Color originalBorderColor;

    private float yPos = 0f;

    public void Initialize() {
        fill = this.FindChildObject("Fill").GetComponent<SpriteRenderer>();
        originalFillColor = fill.color;
        border = this.FindChildObject("Border").GetComponent<SpriteRenderer>();
        originalBorderColor = border.color;
        Hide();
    }

    public void SetSize(float size) {
        transform.localScale = new Vector3(size, size, size);
    }

    public void Show(Vector3 pos) {
        pos.y = yPos;
        transform.position = pos;
        fill.enabled = true;
        border.enabled = true;
    }

    public void SetColorTint(Color color) {
        Color fillColor = color;
        fillColor.a = originalFillColor.a;
        fill.color = fillColor;
        Color borderColor = color;
        borderColor.a = originalBorderColor.a;
        border.color = borderColor;
    }

    public void SetColor(Color color) {
        fill.color = color;
        border.color = color;
    }

    public void Hide() {
        fill.enabled = false;
        border.enabled = false;
    }
}
