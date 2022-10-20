using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public class MenuController : MonoBehaviour
{
    [SerializeField] public AudioSource audioSource;


    [SerializeField] Image buttonAudioMenu;
    [SerializeField] Sprite audioON;
    [SerializeField] Sprite audioOFF;

    private void Start()
    {
        buttonAudioMenu.sprite = audioON;
    }
    public void MuteAudio()
    {
        if (audioSource.mute == false)
        {
            audioSource.mute = true;
            buttonAudioMenu.sprite = audioOFF;
        }
        else if (audioSource.mute == true)
        {
            audioSource.mute = false;
            buttonAudioMenu.sprite = audioON;
        }
    }
    public void PlayGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Main");
    }
}
