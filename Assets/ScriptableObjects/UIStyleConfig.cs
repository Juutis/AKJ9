
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "UIStyleConfig", menuName = "New UIStyleConfig")]
public class UIStyleConfig : ScriptableObject
{
    public Gradient ActiveConnectionGradient;
    public Gradient HoveringLineGradient;
    public Gradient HoveringErrorGradient;
    public Gradient HPGradient;

    [SerializeField]
    private Sprite magicMissileIcon;
    [SerializeField]
    private Sprite FireIcon;
    [SerializeField]
    private Sprite LightningIcon;
    [SerializeField]
    private Sprite IceIcon;

    public Sprite GetEnergyTypeIcon(EnergyTypes energyType) {
        if (energyType == EnergyTypes.MagicMissile) {
            return magicMissileIcon;
        }
        if (energyType == EnergyTypes.Lightning) {
            return LightningIcon;
        }
        if (energyType == EnergyTypes.Fire) {
            return FireIcon;
        }
        if (energyType == EnergyTypes.Ice) {
            return IceIcon;
        }
        return null;
    }

    public string GetEnergyTypeTitle(EnergyTypes energyType) {
        if (energyType == EnergyTypes.MagicMissile) {
            return "Magic missile";
        }
        if (energyType == EnergyTypes.Lightning) {
            return "Lightning";
        }
        if (energyType == EnergyTypes.Fire) {
            return "Fire";
        }
        if (energyType == EnergyTypes.Ice) {
            return "Ice";
        }
        return "";
    }

    public string GetEnergyTypeMessage(EnergyTypes energyType) {
        if (energyType == EnergyTypes.MagicMissile) {
            return "Does damage.";
        }
        if (energyType == EnergyTypes.Lightning) {
            return "Bounces from target to target.";
        }
        if (energyType == EnergyTypes.Fire) {
            return "Does damage on an area.";
        }
        if (energyType == EnergyTypes.Ice) {
            return "Slows down targets.";
        }
        return "";
    }
}

