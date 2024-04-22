using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioEffector : MonoBehaviour
{
    [SerializeField] protected AudioSource _audioSource;

    [SerializeField] protected AudioClip _flying;
    [SerializeField] protected AudioClip _shot;

    public void PlayFlying()
    {
        _audioSource.Play();
    }

    public void PlayShot()
    {
        _audioSource.PlayOneShot(_shot);
    }
}
