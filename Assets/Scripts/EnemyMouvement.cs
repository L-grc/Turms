using System.Collections.Generic;
using UnityEngine;

public class EnemyMouvement : MonoBehaviour
{

    [SerializeField]
    private Rigidbody2D rigidBody;
    [SerializeField]
    private SpriteRenderer spriteRenderer;
    [SerializeField]
    private float speed = 3f;
    [SerializeField]
    private int startDirection = 1;

    private int currentDirection;
    private float halfWidth;
    private Vector2 movement;
    private float movementDeLay;




    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        halfWidth = spriteRenderer.bounds.extents.x;
        currentDirection = startDirection;
        spriteRenderer.flipX = startDirection == 1 ? false : true;


    }


    public void knockbackEnemy(Vector2 knockbackForce, int direction, float delay)
    {

        movementDeLay = delay; 
        knockbackForce.x *= direction;
        rigidBody.linearVelocity = Vector2.zero;
        rigidBody.angularVelocity = 0f;
        rigidBody.AddForce(knockbackForce,ForceMode2D.Impulse);
        


    }




    private void FixedUpdate()
    {

        if (movementDeLay > 0f)
        {
            movementDeLay -= Time.fixedDeltaTime;
            return;
        }

        movement.x = speed * currentDirection;
        movement.y = rigidBody.linearVelocity.y;
        rigidBody.linearVelocity = movement;
        SetDirection();

    }


    private void SetDirection()
    {
        if (Physics2D.Raycast(transform.position, Vector2.right, halfWidth + 0.1f, LayerMask.GetMask("Ground")) && rigidBody.linearVelocity.x > 0)
        {
            currentDirection *= -1; 
        }


        else if (Physics2D.Raycast(transform.position, Vector2.left, halfWidth + 01f, LayerMask.GetMask("Ground")) && rigidBody.linearVelocity.x < 0)
        {
            currentDirection *= -1;
        }


            Debug.DrawRay(transform.position, Vector2.right * 100 /*(halfWidth + 0.1f)*/, Color.red);
            Debug.DrawRay(transform.position, Vector2.left * 100 /*(halfWidth + 0.1f)*/, Color.red);


    }



  

}
