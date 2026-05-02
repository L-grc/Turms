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

    public int currentHealth {  get; private set; }
    public int maxHealth { get; private set; }
    public static Action<int> OnPlayerHealthChanged;
    public static Action<int> OnPlayerRestoreHealth;
    public static Action OnPlayerDie;
    private const string flashRedAnim = "FlashRed";


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        currentHealth = health;
        maxHealth = health;




    }

    public void DamagePlayer(int damageAmount)
    {
        currentHealth -= damageAmount;
        OnPlayerHealthChanged?.Invoke(currentHealth);
        animator.SetTrigger(flashRedAnim);

        if(currentHealth <= 0)
        {
            OnPlayerDie.Invoke();
            Destroy(gameObject);
        }
    }


    private void RestoreHealth(int healthRestored)
    {
        currentHealth += healthRestored;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        OnPlayerHealthChanged?.Invoke(currentHealth);

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
