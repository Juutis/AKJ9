using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathSound : MonoBehaviour
{
    [SerializeField]
    List<AudioClip> deathSounds;
    [SerializeField]
    private float chanceToYell;

    private float dieTime = 1f;
    private float startTime = 0;

    void Start()
    {
        startTime = Time.time;
        if(Random.value > chanceToYell)
        {
            Destroy(gameObject);
        }

        AudioSource src = GetComponent<AudioSource>();
        src.clip = deathSounds[Random.Range(0, deathSounds.Count)];
        src.Play();


    }

    void Update()
    {
        if(Time.time - startTime > dieTime)
        {
            Destroy(gameObject);
        }
    }
}
