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
    [SerializeField] Vector2 deathKick = new Vector2(10f, 10f);
    [SerializeField] GameObject bullet;
    [SerializeField] Transform gun;
    LayerMask ground;
    LayerMask stairs;
    LayerMask enemy;
    BoxCollider2D bodyCollider;
    CapsuleCollider2D feetCollider;
    float gravityScaleAtStart;
    bool isAlive = true;
    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        ground = LayerMask.GetMask("Ground");
        stairs = LayerMask.GetMask("Stairs");
        enemy = LayerMask.GetMask("Enemy", "Hazard");
        bodyCollider = GetComponent<BoxCollider2D>();
        feetCollider = GetComponent<CapsuleCollider2D>();
        gravityScaleAtStart = rb.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        if(!isAlive) { return; }
        Run();
        FlipSprite();
        Climb();
        Die();
    }

    void OnFire(InputValue value)
    {
        if (!isAlive) { return; }
        Instantiate(bullet, gun.position, transform.rotation);
    }

    void OnMove(InputValue value)
    {
        if (!isAlive) { return; }
        moveInput = value.Get<Vector2>();
        Debug.Log(moveInput);        
    }

    void OnJump(InputValue value)
    {
        if (!isAlive) { return; }
        if (value.isPressed && feetCollider.IsTouchingLayers(ground))
        {
            rb.velocity += new Vector2(0f, jumpForce);
            playerAnimator.SetBool("isRunning", true);
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
        Debug.Log($"flip sprite {transform.localScale.x}");
    }

    void Climb()
    {
        if (!bodyCollider.IsTouchingLayers(stairs))
        {
            rb.gravityScale = gravityScaleAtStart;
            playerAnimator.SetBool("isClimbing", false);
            return;
        }

        Vector2 climbVelocity = new Vector2(rb.velocity.x, moveInput.y * climbingSpeed);        
        rb.velocity = climbVelocity;
        rb.gravityScale = 0f;        
        playerAnimator.SetBool("isClimbing", true);

    }

    void Die()
    {
        if (bodyCollider.IsTouchingLayers(enemy))
        {
            isAlive = false;
            playerAnimator.SetTrigger("Dying");
            rb.velocity = deathKick;
        }
    }

}
