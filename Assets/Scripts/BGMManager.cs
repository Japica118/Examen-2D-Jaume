using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMManager : MonoBehaviour
{
    private AudioSource _audioSource;
   
    void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }
 
    void Start()
    {
        _audioSource.Play();
    }

    public void StopBGM()
    {
        _audioSource.Stop();
    }
}
