using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineFollowsMouse : MonoBehaviour
{
    private LineVisualizer line;

    private void Init() {
        line.transform.parent = transform;
        line.SetGradient(Configs.main.UIStyle.HoveringLineGradient);
        Hide();
    }

    public void Initialize() {
        line = Prefabs.Instantiate<LineVisualizer>();
        line.Initialize();
        Init();
    }

    public void Initialize(HasAnimated callback) {
        line = Prefabs.Instantiate<LineVisualizer>();
        line.Initialize(callback);
        Init();
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

    public void ShowError()
    {
        line.SetGradient(Configs.main.UIStyle.HoveringErrorGradient);
    }
    public void ShowNormal()
    {
        line.SetGradient(Configs.main.UIStyle.HoveringLineGradient);
    }
}
