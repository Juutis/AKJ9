using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneShotEffect : MonoBehaviour
{
    List<ParticleSystem> systems = new List<ParticleSystem>();

    // Start is called before the first frame update
    void Start()
    {
        systems.AddRange(GetComponents<ParticleSystem>());
        systems.AddRange(GetComponentsInChildren<ParticleSystem>());
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var system in systems)
        {
            if (system.IsAlive())
            {
                continue;
            }
            Invoke("Die", 1.0f);
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
