﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "EnergyTypeConfig", menuName = "New EnergyTypeConfig")]
public class EnergyTypeConfig : ScriptableObject
{
    [SerializeField]
    private EnergyTypes type;
    public EnergyTypes Type { get { return type; } }

    [SerializeField]
    private Material crystalMaterial;
    public Material CrystalMaterial { get { return crystalMaterial; } }

    [SerializeField]
    private GameObject projectile;
    public GameObject Projectile { get { return projectile; } }

    [SerializeField]
    private float damage;
    public float Damage { get { return damage; } }

    [SerializeField]
    private float speed = 50f;
    public float Speed { get { return speed; } }

    [SerializeField]
    private float explodeRange;
    public float ExplodeRange { get { return explodeRange; } }

    [SerializeField]
    private int bounces = 0;
    public int Bounces { get { return bounces; } }

    [SerializeField]
    private float bounceDistance = 0;
    public float BounceDistance { get { return bounceDistance; } }

    [SerializeField]
    private float freezeTime;
    public float FreezeTime { get { return freezeTime; } }

    [SerializeField]
    private float freezeMultiplier;
    public float FreezeMultiplier { get { return freezeMultiplier; } }

    [SerializeField]
    private Color effectColor;
    public Color EffectColor { get { return effectColor; } }

    [SerializeField]
    private List<EnergyTypePair> combos;
    public List<EnergyTypePair> Combos { get { return combos; } }

    public EnergyTypeConfig GetCombo(EnergyTypes type)
    {
        EnergyTypePair pair = combos.Where(x => x.type == type).FirstOrDefault();
        if (pair == null) return null;
        return pair.config;
    }
}

[Serializable]
public class EnergyTypePair
{
    public EnergyTypes type;
    public EnergyTypeConfig config;
}