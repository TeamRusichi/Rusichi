using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlayerSoundEmitter : MonoBehaviour
{
    [SerializeField] private List<AudioClip> defaultStepSounds;
    [SerializeField] private List<AudioClip> snowStepSounds;
    [SerializeField] private bool isSnowStepSounds;
    
    private AudioSource _audioSource;
    
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void Step()
    {
        var stepSounds = isSnowStepSounds ? snowStepSounds : defaultStepSounds;
        _audioSource.PlayOneShot(stepSounds[Random.Range(0, stepSounds.Count)]);
    }
}
