using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;


public class PlayerHealth : MonoBehaviour
{

    [SerializeField]
    private int health = 10;
    [SerializeField]
    private Animator animator; 

    public int currentHealth {  get; private set; }
    public int maxHealth { get; private set; }
    public static Action<int> OnPlayerTakeDamage; 
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
        OnPlayerTakeDamage?.Invoke(currentHealth);
        animator.SetTrigger(flashRedAnim);

        if(currentHealth <= 0)
        {
            OnPlayerDie.Invoke();
            Destroy(gameObject);
        }
    }



}
