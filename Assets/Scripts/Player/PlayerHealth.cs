using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using JetBrains.Annotations;


public class PlayerHealth : MonoBehaviour
{

    [SerializeField]
    private int health = 10;
    [SerializeField]
    private Animator animator; 

    public float CurrentHealth {  get; private set; }
    public float MaxHealth { get; private set; }
    public static Action<float> OnPlayerHealthChanged;
    public static Action<float> OnPlayerRestoreHealth;
    public static Action OnPlayerDie;
    private const string flashRedAnim = "FlashRed";


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        CurrentHealth = health;
        MaxHealth = health;




    }

    public void DamagePlayer(float damageAmount)
    {
        CurrentHealth -= damageAmount;
        OnPlayerHealthChanged?.Invoke(CurrentHealth);
        animator.SetTrigger(flashRedAnim);

        if(CurrentHealth <= 0)
        {
            OnPlayerDie.Invoke();
            Destroy(gameObject);
        }
    }


    private void RestoreHealth(float healthRestored)
    {
        CurrentHealth += healthRestored;
        CurrentHealth = Mathf.Clamp(CurrentHealth, 0, MaxHealth);
        OnPlayerHealthChanged?.Invoke(CurrentHealth);

    }


    private void OnEnable()
    {
        Healing.OnFruitCollected += RestoreHealth;
    }


    private void OnDisable()
    {
        Healing.OnFruitCollected -= RestoreHealth;
    }
}
