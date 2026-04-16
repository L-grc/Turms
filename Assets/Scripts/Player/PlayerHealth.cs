using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;


public class PlayerHealth : MonoBehaviour
{

    [SerializeField]
    private int health = 10;

    public int currentHealth {  get; private set; }
    public int maxHealth { get; private set; }
    public static Action<int> OnPlayerTakeDamage; 
    public static Action OnPlayerDie;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        currentHealth = health;
        maxHealth = health;




    }

    public void DamagePlayer(int damageAmount)
    {
        currentHealth -= damageAmount;
        OnPlayerTakeDamage?.Invoke(currentHealth);

        if(currentHealth <= 0)
        {
            OnPlayerDie.Invoke();
            Destroy(gameObject);
        }
    }



}
