using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class GameController : MonoBehaviour
{
    [SerializeField] GameObject backgroundDarkCanvas;
    [SerializeField] PlayerController playerController;
    [SerializeField] TMP_Text textPointPlayer;
    [SerializeField] GameObject panelMenu;
    bool panelMenuState;
    [SerializeField] AudioSource audioMain;


    private void Start()
    {
        backgroundDarkCanvas.SetActive(false);
        panelMenu.SetActive(false);
        panelMenuState = false;
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape) && panelMenuState == false && playerController.isLife == true)
        { 
            panelMenu.SetActive(true);
            Time.timeScale = 0;
            panelMenuState = true;
            audioMain.pitch = 0.95f;
        }
        else if (Input.GetKeyUp(KeyCode.Escape) && panelMenuState == true && playerController.isLife == true)
        {
            panelMenu.SetActive(false);
            Time.timeScale = 1;
            panelMenuState = false;
            audioMain.pitch = 1f;
        }

    }

    public void GameOver()
    {
        Time.timeScale = 0;
        backgroundDarkCanvas.SetActive(true);
        textPointPlayer.text = "POINTS: " + playerController.pointAccumulated.ToString();
    }
    
    public void StarGame()
    {
        Time.timeScale = 1;
        backgroundDarkCanvas.SetActive(false);
    }

    public void AgainStar()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Main");
    }

    public void BackHome()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void ResumeGame()
    {
        panelMenu.SetActive(false);
        Time.timeScale = 1;

        audioMain.pitch = 1f;
    }
}
