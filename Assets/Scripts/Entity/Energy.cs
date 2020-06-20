using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Energy : Targetable
{
    private Tower currentTower;
    public Tower CurrentTower { get { return currentTower; } }
    private LineVisualizer line;

    private void InitializeLine() {
        line = Prefabs.Instantiate<LineVisualizer>();
        line.Initialize();
        line.transform.parent = transform;
        line.SetGradient(Configs.main.UIStyle.ActiveConnectionGradient);
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
        tower.Connect(this);
    }
}
