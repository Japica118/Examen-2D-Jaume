using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public AudioClip coinSFX;
    public AudioClip starSFX;
    public AudioClip jumpSFX;
   

    private AudioSource _audioSource;

    void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void CoinSound()
    {
        _audioSource.PlayOneShot(coinSFX);
    }

    public void StarSound()
    {
        _audioSource.PlayOneShot(starSFX);
    }

    public void JumpSound()
    {
        _audioSource.PlayOneShot(jumpSFX);
    }


}
