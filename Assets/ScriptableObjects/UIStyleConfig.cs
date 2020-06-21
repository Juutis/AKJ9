
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "UIStyleConfig", menuName = "New UIStyleConfig")]
public class UIStyleConfig : ScriptableObject
{
    public Gradient ActiveConnectionGradient;
    public Gradient HoveringLineGradient;
    public Gradient HPGradient;
}

