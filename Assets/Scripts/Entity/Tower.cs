using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : Targetable
{
    public void Connect(Energy energy) {
        Debug.Log("<color=green><b>Connected:</b></color> [{0}] <b>=></b> [{1}]".Format(this, energy));
    }
    
    public void Disconnect(Energy energy) {
        Debug.Log("<color=red><b>Disconnected:</b></color> [{0}] <b>=></b> [{1}]".Format(this, energy));
    }

}
