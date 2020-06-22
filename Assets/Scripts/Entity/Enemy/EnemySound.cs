using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySound : MonoBehaviour
{
    [SerializeField]
    private List<AudioClip> idleSounds;
    [SerializeField]
    [Range(0, 1)]
    private float idleVolume;
    [SerializeField]
    [Range(0, 1)]
    private float footStepVolume;
    [SerializeField]
    private AudioClip footStepSound;

    private Queue<float> secondsToNextIdleSound = new Queue<float>();
    private AudioSource audioSource;
    private float lastSound = 0;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        randomizeSounds();
    }

    private void randomizeSounds()
    {
        for (int i = 0; i < 100; i++)
        {
            secondsToNextIdleSound.Enqueue(Random.Range(7f, 25f));
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(secondsToNextIdleSound.Count == 0)
        {
            randomizeSounds();
        }

        if(Time.time - lastSound > secondsToNextIdleSound.Peek())
        {
            secondsToNextIdleSound.Dequeue();
            audioSource.clip = idleSounds[Random.Range(0, idleSounds.Capacity - 1)];
            audioSource.volume = footStepVolume;
            audioSource.Play();
            lastSound = Time.time;
        }
        
        if(!audioSource.isPlaying)
        {
            audioSource.clip = footStepSound;
            audioSource.volume = footStepVolume;
            audioSource.Play();
        }
    }
}
