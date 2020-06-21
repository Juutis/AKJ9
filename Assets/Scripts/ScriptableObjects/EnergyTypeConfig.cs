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
    private int damage;
    public int Damage { get { return damage; } }

    [SerializeField]
    private float speed = 50f;
    public float Speed { get { return speed; } }

    [SerializeField]
    private int explodeRange;
    public int ExplodeRange { get { return explodeRange; } }

    [SerializeField]
    private int bounces = 0;
    public int Bounces { get { return bounces; } }

    [SerializeField]
    private int bounceDistance = 0;
    public int BounceDistance { get { return bounceDistance; } }

    [SerializeField]
    private float freezeTime;
    public float FreezeTime { get { return freezeTime; } }

    [SerializeField]
    private float freezeMultiplier;
    public float FreezeMultiplier { get { return freezeMultiplier; } }

    private Color effectColor;
    public Color EffectColor { get { return effectColor; } }
}
