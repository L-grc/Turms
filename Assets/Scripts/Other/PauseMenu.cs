using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PauseMenu : MonoBehaviour
{

    private bool isPaused;
    public GameObject pausePanel;
    



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
       


    }

    // Update is called once per frame
    void Update()
    {
        
        if(Input.GetButtonDown("Pause"))
        {
            if(isPaused)
                ResumeGame();
            else
                PauseGame();
                
        }



    }


    public void PauseGame()
    {
        Time.timeScale = 0;
        pausePanel.SetActive(true);
        isPaused = true;
        


    }

    public void ResumeGame()
    {

        Time.timeScale = 1;
        pausePanel.SetActive(false);
        isPaused = false;


    }



}
