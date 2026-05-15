using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMelee : MonoBehaviour
{
    public Transform attackOrigin;
    public float attackRadius = 1f;
    public LayerMask Enemy;

    public float cooldownTime = .5f;
    private float cooldownTimer = 0f;

    public int damage = 25;

    public Animator animator;

    private void Update()
    {
        if (cooldownTimer <= 0)
        {
            if (Input.GetKeyDown(KeyCode.K))
            {
              
                // Example of playing attack animation
                // animator.SetTrigger("Melee");

                Collider2D[] enemiesInRange = Physics2D.OverlapCircleAll(attackOrigin.position, attackRadius, Enemy);
                
                foreach (var Enemy in enemiesInRange)
                {
                   
                    Enemy.GetComponent<Enemy>().TakeDamage(damage);
                }

                cooldownTimer = cooldownTime;
            }
        }
        else
        {
            cooldownTimer -= Time.deltaTime;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackOrigin.position, attackRadius);
    }
}