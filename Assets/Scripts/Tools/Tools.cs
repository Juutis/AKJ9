using UnityEngine;

public class Tools
{
    public static bool RandomPercent(int percent)
    {
        return percent >= Random.Range(1, 101);
    }

    public static bool MouseCast(out RaycastHit hit, LayerMask targetLayer) {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 1000f, targetLayer)) {
            return true;
        }
        return false;
    }
}