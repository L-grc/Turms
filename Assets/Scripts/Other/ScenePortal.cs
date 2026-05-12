using UnityEngine;
using UnityEngine.SceneManagement;


public class ScenePortal : MonoBehaviour, IInteractable
{

    [SerializeField] private string sceneToLoad;

    public bool CanInteract()
    {
       return true;
    }

    public void Interact()
    {
        SceneManager.LoadScene(sceneToLoad);
    }

 
}
