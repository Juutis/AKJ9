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
        border = this.FindChildObject("Border").GetComponent<SpriteRenderer>();
        Hide();
    }

    public void Show(Vector3 pos) {
        pos.y = yPos;
        transform.position = pos;
        fill.enabled = true;
        border.enabled = true;
    }

    public void ChangeTint(Color color) {
        Color fillColor = color;
        fillColor.a = originalFillColor.a;
        fill.color = fillColor;
        Color borderColor = color;
        borderColor.a = originalBorderColor.a;
        border.color = borderColor;
    }

    public void Hide() {
        fill.enabled = false;
        border.enabled = false;
    }
}
