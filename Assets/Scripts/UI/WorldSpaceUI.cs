using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldSpaceUI : MonoBehaviour
{

    public static WorldSpaceUI main;
    void Awake() {
        main = this;
        container = this.FindChildObject("WorldSpaceCanvas").transform;
    }
    private Transform container;

    public HitPointBar GetHitPointBar(int hp) {
        HitPointBar hpBar = Prefabs.Instantiate<HitPointBar>();
        hpBar.Initialize(hp);
        hpBar.transform.SetParent(container);
        return hpBar;
    }
}
