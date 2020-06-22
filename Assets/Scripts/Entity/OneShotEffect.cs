using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneShotEffect : MonoBehaviour
{
    List<ParticleSystem> systems = new List<ParticleSystem>();
    Pooled pooled;

    // Start is called before the first frame update
    void Start()
    {
        systems.AddRange(GetComponents<ParticleSystem>());
        systems.AddRange(GetComponentsInChildren<ParticleSystem>());
        pooled = GetComponent<Pooled>();
    }

    public void Play()
    {
        foreach (var system in systems)
        {
            system.Play();
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var system in systems)
        {
            if (system.IsAlive())
            {
                break;
            }
            Invoke("Die", 0.0f);
        }
    }

    void Die()
    {
        pooled.GetPool().DeactivateObject(gameObject);
    }
}
