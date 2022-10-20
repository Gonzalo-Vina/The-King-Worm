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



    private void Start()
    {
        backgroundDarkCanvas.SetActive(false);
    }


    public void PauseGame()
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
}
