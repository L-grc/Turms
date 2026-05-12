using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Healing : MonoBehaviour, IInteractable
{



    [SerializeField] 
    private int healthRecovered = 2;

    public static Action<int> OnFruitCollected;


   

    public void Interact()
    {
        OnFruitCollected?.Invoke(healthRecovered);
        Destroy(gameObject); 
    }

    public bool CanInteract()
    {
        return true;
    }
}
