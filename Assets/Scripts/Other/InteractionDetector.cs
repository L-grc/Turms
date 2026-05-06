using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;
public class InteractionDetector : MonoBehaviour
{
    private IInteractable interactableInRange = null;
    public GameObject interactionIcon;
    public InputActionReference interactionAction;


    void Start()
    {
       interactionIcon.SetActive(false);
        interactionAction.action.performed += OnInteracte;
        
    }


    public void OnInteracte(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            interactableInRange?.Interact();
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out IInteractable interactable) && interactable.CanInteract())
        {
            interactableInRange = interactable;
            interactionIcon.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IInteractable interactable) && interactable == interactableInRange)
        {
            interactableInRange = null;
            interactionIcon.SetActive(false);
        }
    }


    private void OnDisable()
    {
        interactionAction.action.started -= OnInteracte;
        interactionAction.action.canceled -= OnInteracte;
    }
}
