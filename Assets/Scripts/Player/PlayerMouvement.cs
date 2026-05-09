using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering;

public class PlayerMovement : MonoBehaviour
{

    public Rigidbody2D playerRb;
    public float speed;
    public float input;
    public SpriteRenderer spriteRenderer;
    public float jumpForce;

    public LayerMask groundLayer;
    private bool isGrounded;
    public Transform feetPosition;
    public float groundCheckCircle;
    Animator animator;



    private void Start()
    {

        animator = GetComponent<Animator>();






    }



    void Update()
    {

        if (PauseController.IsGamePause)
        {
            playerRb.linearVelocity = Vector2.zero;

        }
        //Direction
        input = Input.GetAxisRaw("Horizontal");
        if (input < 0)
        {
            spriteRenderer.flipX = true;
        }

        else if(input > 0)
        {
            spriteRenderer.flipX = false;
        }


        //Jump

        isGrounded = Physics2D.OverlapCircle(feetPosition.position, groundCheckCircle, groundLayer);


        if (isGrounded == true && Input.GetButton("Jump"))
        {
            playerRb.linearVelocity = Vector2.up * jumpForce;
        }



    }

    void FixedUpdate()
    {
        playerRb.linearVelocity = new Vector2 (input * speed, playerRb.linearVelocity.y);
        animator.SetFloat("Speed", Mathf.Abs(input));
        animator.SetBool("isGrounded", isGrounded);

    }



    public void knockbackPlayer (Vector2 knockbackForce, int direction)
    {
        knockbackForce.x *= direction;
        playerRb.linearVelocity = Vector2.zero;
        playerRb.angularVelocity = 0f;
        playerRb.AddForce(knockbackForce, ForceMode2D.Impulse);
    }




    /* private float horizontal;
     private float speed = 8f;
     private float jumpingPower = 16f;
     private bool isFacingRight = true;

     private Collider2D platformCollider;

     [SerializeField] private Rigidbody2D rb;
     [SerializeField] private Transform groundCheck;
     [SerializeField] private LayerMask groundLayer;
     [SerializeField] private Animator animator;
     [SerializeField] private string IsWalking = "IsWalking";

     void Update()
     {
         if (Input.GetAxisRaw("Horizontal") != 0)
         {
             animator.SetTrigger("IsWalking");
         }
         else
         {
             animator.SetTrigger("StopWalking");
         }

             horizontal = Input.GetAxisRaw("Horizontal");

         if (Input.GetButtonDown("Jump") && IsGrounded())
         {
             rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpingPower);
         }

         if (Input.GetButtonUp("Jump") && rb.linearVelocity.y > 0f)
         {
             rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * 0.5f);
         }

         Flip();
     }

     private void FixedUpdate()
     {
         rb.linearVelocity = new Vector2(horizontal * speed, rb.linearVelocity.y);
     }

     private bool IsGrounded()
     {
         return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
     }

     private void Flip()
     {
         if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
         {
             isFacingRight = !isFacingRight;
             Vector3 localScale = transform.localScale;
             localScale.x *= -1f;
             transform.localScale = localScale;
         }
     }*/
}