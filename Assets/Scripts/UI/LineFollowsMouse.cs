using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineFollowsMouse : MonoBehaviour
{
    private LineVisualizer line;

    public void Initialize() {
        line = Prefabs.Instantiate<LineVisualizer>();
        line.Initialize();
        line.transform.parent = transform;
        line.SetGradient(Configs.main.UIStyle.HoveringLineGradient);
        Hide();
    }

    public void SetEnd(Vector3 position) {
        line.SetEndPoint(position);
        line.Show();
    }

    public void SetStart(Vector3 position) {
        line.SetStartPoint(position);
    }

    public void Hide() {
        line.Hide();
    }
    public void Show() {
        line.Show();
    }
}
