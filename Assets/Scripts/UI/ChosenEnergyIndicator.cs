using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChosenEnergyIndicator : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private EnergyTypes energyType;

    public EnergyTypes Type { get { return energyType; } }

    [SerializeField]
    private Color activeColor;
    private Color originalImgColor;
    private Color originalTxtColor;
    private Image imgHighlight;
    private Text txtHighlight;

    void Start()
    {
        imgHighlight = this.FindChildObject("source").GetComponent<Image>();
        txtHighlight = GetComponentInChildren<Text>();
        originalImgColor = imgHighlight.color;
        originalTxtColor = txtHighlight.color;
    }
    public void Activate()
    {
        imgHighlight.color = activeColor;
        txtHighlight.color = activeColor;
    }

    public void Deactivate()
    {
        imgHighlight.color = originalImgColor;
        txtHighlight.color = originalTxtColor;
    }
}
