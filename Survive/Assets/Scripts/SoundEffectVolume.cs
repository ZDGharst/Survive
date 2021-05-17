using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectVolume : MonoBehaviour
{
    private AudioSource soundEffect;

    void Start()
    {
        soundEffect = GetComponent<AudioSource>();
        soundEffect.volume = PlayerPrefs.GetFloat("EffectsVolume", 0.75f);
    }

    void Update()
    {
    }
}
