using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script para manejar 
public class AudioHandler : MonoBehaviour
{
    private AudioSource _audioSource;

    [SerializeField] public AudioClip[] audios;

    private void Start()
    {
        _audioSource = gameObject.GetComponent<AudioSource>();
    }

    public void PlayClip(int clipIndex)
    {
        _audioSource.PlayOneShot(audios[clipIndex]);
    }
}
