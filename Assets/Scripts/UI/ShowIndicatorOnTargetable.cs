using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowIndicatorOnTargetable : MonoBehaviour
{
    private HoverIndicator hoverIndicator;
    public void Initialize()
    {
        hoverIndicator = Prefabs.Instantiate<HoverIndicator>();
        hoverIndicator.Initialize();
        hoverIndicator.transform.parent = transform;
    }

    public void ShowAt(Vector3 position) {
        hoverIndicator.Show(position);
    }

    public void Hide() {
        hoverIndicator.Hide();
    }
}
