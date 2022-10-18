using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundMaganer : MonoBehaviour
{
    public static SoundMaganer Instance;

    private AudioSource audioSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayAudio(AudioClip sound)
    {
        audioSource.PlayOneShot(sound);
    }
}
