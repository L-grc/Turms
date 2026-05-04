using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;


public class StartMenuController : MonoBehaviour
{
    


    public void OnStartClick()
    {
        SceneManager.LoadScene("StartingVillage");
    }

    public void OnSettingsClick()
    {
        
    }


    public void OnExitClick()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }


}
