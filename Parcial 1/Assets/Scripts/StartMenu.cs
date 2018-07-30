using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenu : MonoBehaviour {

    public GameObject startCanvas;


    private void Start()
    {
        Time.timeScale = 0;
        startCanvas.SetActive(true);
    }
    public void PlayGame()
    {        
        startCanvas.SetActive(false);
        Time.timeScale = 1;
    }
}
