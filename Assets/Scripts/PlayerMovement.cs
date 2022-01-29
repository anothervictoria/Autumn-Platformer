using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    Vector2 moveInput;
    Rigidbody2D rb;
    Animator playerAnimator;
    [SerializeField] float runSpeed = 10f;
    [SerializeField] float jumpForce = 5f;
    [SerializeField] float climbingSpeed = 3f;
    LayerMask ground;
    LayerMask stairs;
    BoxCollider2D playerCollider;
    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        ground = LayerMask.GetMask("Ground");
        stairs = LayerMask.GetMask("Stairs");
        playerCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Run();
        FlipSprite();
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
        Debug.Log(moveInput);        
    }

    void OnJump(InputValue value)
    {      
        if (value.isPressed && playerCollider.IsTouchingLayers(ground))
        {
            rb.velocity += new Vector2(0f, jumpForce);
        }
    }

    void Run()
    {
        Vector2 playerVelocity = new Vector2(moveInput.x * runSpeed, rb.velocity.y);
        rb.velocity = playerVelocity;

        bool playerHasHorizontalSpeed = Mathf.Abs(rb.velocity.x) > Mathf.Epsilon;
        playerAnimator.SetBool("isRunning", playerHasHorizontalSpeed);
    }

    void FlipSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(rb.velocity.x) > Mathf.Epsilon;

        if (playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(rb.velocity.x), 1f);
        }      
    }

    void OnClimb(InputValue value)
    {
        if(value.isPressed && playerCollider.IsTouchingLayers(stairs))
        {
            Debug.Log(playerCollider.IsTouchingLayers(stairs));
            rb.velocity += new Vector2(0f, climbingSpeed);
            playerAnimator.SetBool("isClimbing", true);
        }
    }

}
