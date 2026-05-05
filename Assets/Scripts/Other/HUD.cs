using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;
using UnityEngine.Rendering;
public class HUD : MonoBehaviour
{

    [SerializeField]
    private Slider healthbar;

    private float maxHealth;

    private void SetupHealthbar(GameObject player)
    {
        
        maxHealth = player.GetComponent<PlayerHealth>().MaxHealth;
        healthbar.maxValue = maxHealth;
        healthbar.value = maxHealth;
    }

    private void OnEnable()
    {
        GameController.OnPlayerSpawned += SetupHealthbar;   
        PlayerHealth.OnPlayerHealthChanged += UpdateHealthbar;
    }

    private void UpdateHealthbar(float currentHealth)
    {
        healthbar.value = currentHealth;
      
    }


    private void OnDisable()
    {
        GameController.OnPlayerSpawned -= SetupHealthbar;
        PlayerHealth.OnPlayerHealthChanged -= UpdateHealthbar;
    }


}
