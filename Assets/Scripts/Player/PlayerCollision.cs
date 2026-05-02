using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.Rendering;

public class PlayerCollision : MonoBehaviour
{

    [SerializeField]
    private SpriteRenderer SpriteRenderer;
    [SerializeField]
    private float bounceForce = 6f;
    [SerializeField]
    private Rigidbody2D rigidBody;
    [SerializeField]
    private float bounceForceMultiplier = 1.3f;

    private float halfHeight; 
   
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        halfHeight = SpriteRenderer.bounds.extents.y;
    }



    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Healing"))
        {
            other.GetComponent<Healing>().Collect();
        }
    }


    // Update is called once per frame
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            CollideWithEnemy(other);
        }
    }

    private void CollideWithEnemy(Collision2D other)
    {

        Enemy enemy = other.gameObject.GetComponent<Enemy>();

        
        if(Physics2D.Raycast(transform.position, Vector2.down, halfHeight + 0.1f, LayerMask.GetMask("Enemy")))
        {
            Vector2 velocity = rigidBody.linearVelocity;
            velocity.y = 0f;
            rigidBody.linearVelocity = velocity;

            float force = bounceForce; 
            if (Input.GetButton("Jump"))
            {
                force *= bounceForceMultiplier;
            }

            rigidBody.AddForce(Vector2.up * force, ForceMode2D.Impulse);
            enemy.Die();
        }
        else
        {
            enemy.HitPlayer(transform);
        }

    }

}
