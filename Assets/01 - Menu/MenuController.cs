using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public class MenuController : MonoBehaviour
{
    [SerializeField] public AudioSource audioSource;

    private bool isMute;

    [SerializeField] Image buttonAudioMenu;
    [SerializeField] Sprite audioON;
    [SerializeField] Sprite audioOFF;

    [SerializeField] GameObject panelExit;

    private void Start()
    {
        isMute = PlayerPrefs.GetInt("MUTED") == 1;
        audioSource.mute = isMute;
        if (!isMute) buttonAudioMenu.sprite = audioON;
        else buttonAudioMenu.sprite = audioOFF;
        panelExit.SetActive(false);
    }

    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.Escape)) panelExit.SetActive(true);       
    }
    public void MuteAudio()
    {
        if (isMute == false)
        {
            isMute = !isMute;
            audioSource.mute = true;
            buttonAudioMenu.sprite = audioOFF;
            PlayerPrefs.SetInt("MUTED", isMute ? 1 : 0);
        }
        else if (isMute == true)
        {
            isMute = !isMute;
            audioSource.mute = false;
            buttonAudioMenu.sprite = audioON;
            PlayerPrefs.SetInt("MUTED", isMute ? 1 : 0);
        }
    }
    public void PlayGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Main");
    }
    public void ResumeGame()
    {
        panelExit.SetActive(false);
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
