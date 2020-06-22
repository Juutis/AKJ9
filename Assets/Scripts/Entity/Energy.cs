using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Energy : Targetable
{
    private Tower currentTower;
    public Tower CurrentTower { get { return currentTower; } }
    private LineVisualizer line;

    private Squiggle squiggle;
    private EnergyType energyType;
    public EnergyType EnergyType { get { return energyType; } }

    private UIStyleConfig config;

    public Sprite Icon {get {return config.GetEnergyTypeIcon(energyType.Type);}}
    public string Title {get {return config.GetEnergyTypeTitle(energyType.Type);}}
    public string Message {get {return config.GetEnergyTypeMessage(energyType.Type);}}

    [SerializeField]
    public KeyCode shortcut;

    private void Start()
    {
        config = Configs.main.UIStyle;
        energyType = GetComponent<EnergyType>();
    }

    private void InitializeLine()
    {
        squiggle = Prefabs.Instantiate<Squiggle>();
        line = Prefabs.Instantiate<LineVisualizer>();
        line.Initialize(HasAnimated);
        line.transform.parent = transform;
        line.SetGradient(config.ActiveConnectionGradient);
    }

    public void HasAnimated() {
        currentTower.Connect(this);
        
        var config = Configs.main.EnergyTypeConfigs[energyType.Type];

        squiggle.Initialize(transform.position, currentTower.transform.position, config.EffectColor);
    }

    public void Connect(Tower tower)
    {
        if (currentTower != null)
        {
            currentTower.Disconnect(this);
        }
        if (line == null)
        {
            InitializeLine();
        }

        line.SetStartPoint(this.transform.position);
        line.SetEndPoint(tower.transform.position);
        line.Show();
        currentTower = tower;
    }

    public void Disconnect()
    {
        if (currentTower != null)
        {
            currentTower.Disconnect(this);
        }
        if (line == null)
        {
            InitializeLine();
        }
        line.Hide();
        squiggle.Hide();
        currentTower = null;
    }
}
