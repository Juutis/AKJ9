using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Energy : Targetable
{
    private Tower currentTower;
    public Tower CurrentTower { get { return currentTower; } }
    private LineVisualizer line;

    private Squiggle squiggle;

    private void InitializeLine() {
        squiggle = Prefabs.Instantiate<Squiggle>();
        line = Prefabs.Instantiate<LineVisualizer>();
        line.Initialize(HasAnimated);
        line.transform.parent = transform;
        line.SetGradient(Configs.main.UIStyle.ActiveConnectionGradient);
    }

    public void HasAnimated() {
        currentTower.Connect(this);
        squiggle.Initialize(transform.position, currentTower.transform.position);
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
}
