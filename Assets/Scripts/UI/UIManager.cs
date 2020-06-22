﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{

    public static UIManager main;
    void Awake()
    {
        main = this;
        worldSpaceCanvas = this.FindChildObject("WorldSpaceCanvas").transform;
        uiCanvas = this.FindChildObject("UICanvas").transform;
        hudScore = GetComponentInChildren<HUDScore>();
    }
    private Transform uiCanvas;
    private Transform worldSpaceCanvas;
    private HUDScore hudScore;
    public Transform WorldSpaceCanvas { get { return worldSpaceCanvas; } }

    public Transform UICanvas {get {return uiCanvas;}}

    public HitPointBar GetHitPointBar(float hp)
    {
        HitPointBar hpBar = Prefabs.Instantiate<HitPointBar>();
        hpBar.Initialize(hp);
        hpBar.transform.SetParent(worldSpaceCanvas);
        return hpBar;
    }

    public void UpdateMultiplier (float multiplier) {
        hudScore.UpdateMultiplier(multiplier);
    }
    public void UpdateScore (int addition, int score) {
        hudScore.UpdateScore(addition, score);
    }
}
