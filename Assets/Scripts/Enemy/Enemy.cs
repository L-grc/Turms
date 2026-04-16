using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Enemy : MonoBehaviour
{

    [SerializeField]
    private Vector2 knockbackToSelf = new Vector2(3f, 5f);
    [SerializeField]
    private Vector2 knockbackToPlayer = new Vector2(3f, 5f);
    [SerializeField]
    private float knockbackDelayToSelf = 1.5f;

    [SerializeField]
    private int damage = 3; 



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Die()
    {
        Destroy(gameObject);
    }

    // Update is called once per frame
    public void HitPlayer(Transform playerTransform)
    {
        int direction = GetDirection(playerTransform);
        Object.FindFirstObjectByType<PlayerMovement>().knockbackPlayer(knockbackToPlayer, direction);
        Object.FindFirstObjectByType<PlayerHealth>().DamagePlayer(damage);
        GetComponent<EnemyMouvement>().knockbackEnemy(knockbackToSelf, -direction, knockbackDelayToSelf);

    }

    private int GetDirection(Transform playerTransform)
    {
        if (transform.position.x > playerTransform.position.x)
        {
            return -1;
        }
        else
        {
            return 1;
        }


    }




}
