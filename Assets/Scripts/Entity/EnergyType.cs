using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyType : MonoBehaviour
{
    [SerializeField]
    EnergyTypes type;
    Material crystalMaterial;

    // Start is called before the first frame update
    void Start()
    {
        
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