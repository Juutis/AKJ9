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
        }
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