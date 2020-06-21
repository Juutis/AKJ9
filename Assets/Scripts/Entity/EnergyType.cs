using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyType : MonoBehaviour
{
    [SerializeField]
    private EnergyTypes type;
    public EnergyTypes Type { get { return type; } }

    private MeshRenderer crystalRenderer;
    private Material crystalMaterial;
    private readonly string crystalMeshName = "Cube.002";
    private Light light;

    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform nested in transform)
        {
            foreach (Transform nested2 in nested)
            {
                foreach (Transform t in nested2)
                {
                    if (t.name == crystalMeshName)
                    {
                        crystalRenderer = t.GetComponent<MeshRenderer>();
                    }
                }
            }

            if (nested.name == "Point Light")
            {
                light = nested.GetComponent<Light>();
            }
        }
        EnergyTypeConfig conf = Configs.main.EnergyTypeConfigs[type];
        crystalMaterial = conf.CrystalMaterial;
        crystalRenderer.material = crystalMaterial;
        light.color = conf.EffectColor;
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
    Lightning,

    FireIce,
    FireLightning,
    FireMagic,
    IceLightning,
    IceMagic,
    LightningMagic
}