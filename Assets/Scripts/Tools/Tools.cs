using UnityEngine;

public class Tools
{
    public static bool RandomPercent(int percent)
    {
        return percent >= Random.Range(1, 101);
    }
}