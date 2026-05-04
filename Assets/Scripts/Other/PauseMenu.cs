using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;



public class PauseMenu : MonoBehaviour
{

    private bool isPaused;
    public GameObject menuCanvas;
    



  
    void Start()
    {
        
       menuCanvas.SetActive(false);


    }

  
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
        menuCanvas.SetActive(true);
        isPaused = true;
        


    }

    public void ResumeGame()
    {

        Time.timeScale = 1;
        menuCanvas.SetActive(false);
        isPaused = false;


    }



}
