using System.Collections;
using System.Collections.Generic;
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
    private Color effectColor;
    public Color EffectColor { get { return effectColor; } }


}
