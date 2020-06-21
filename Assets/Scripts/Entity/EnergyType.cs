﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyType : MonoBehaviour
{
    [SerializeField]
    EnergyTypes type;
    [SerializeField]
    MeshRenderer crystalRenderer;
    Material crystalMaterial;

    // Start is called before the first frame update
    void Start()
    {
        EnergyTypeConfig conf = Configs.main.EnergyTypeConfigs[type];
        crystalMaterial = conf.CrystalMaterial;
        crystalRenderer.material = crystalMaterial;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

public enum EnergyTypes
{
    MagicMissile,
    Fire,
    Ice,
    Boulder,
    Lightning
}