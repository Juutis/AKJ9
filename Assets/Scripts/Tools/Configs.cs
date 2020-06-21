using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Configs : MonoBehaviour
{
    public static Configs main;

    void Awake() {
        main = this;
    }

    [SerializeField]
    private UIStyleConfig uiStyleConfig;
    public UIStyleConfig UIStyle { get { return uiStyleConfig; } }

    [SerializeField]
    private List<EnergyTypeConfig> energyTypeConfigs;
    public Dictionary<EnergyTypes, EnergyTypeConfig> EnergyTypeConfigs { 
        get {
            return energyTypeConfigs.ToDictionary(x => x.Type);
        } 
    }

}
