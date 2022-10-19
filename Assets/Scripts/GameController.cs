using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] GameObject backgroundDarkCanvas;



    private void Start()
    {
        backgroundDarkCanvas.SetActive(false);
    }


    public void PauseGame()
    {
        Time.timeScale = 0;
        backgroundDarkCanvas.SetActive(true);
    }
    
    public void StarGame()
    {
        Time.timeScale = 1;
        backgroundDarkCanvas.SetActive(false);
    }
}
