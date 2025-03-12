using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;
    private BoxCollider2D boxCollider2D;

    [Header ("Movement Parameters")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float JumpForce;

    [Header("Collider")]
    [SerializeField] private BoxCollider2D boxcollider2D;
    private float crouchHeighMultiplier = 0.5f;


    [Header("Multriple Jumps")]
    [SerializeField] private int extraJump;
    private int jumpCounter;

    [Header("Wall Jumping")]
    [SerializeField] private float wallJumpX;
    [SerializeField] private float wallJumpY;

    [Header ("Layer")]
    public LayerMask groundLayer;
    public LayerMask wallLayer;

    [Header("SFX")]
    [SerializeField] private AudioClip jumpSound;

    private float horizontalInput;
    private float wallJumpCooldown;
    private bool isCrouching;
    private Vector2 originalSize;
    private Vector2 originalOffset;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        boxCollider2D = GetComponent<BoxCollider2D>();

        originalSize = boxcollider2D.size;
        originalOffset = boxcollider2D.offset;
    }

    private void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        Flip();
        Crouch();
        CheckAnimation();
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Jump();

        }
        if(Input.GetKeyUp(KeyCode.Space) && rb.velocity.y > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y / 2);
            jumpCounter--;
        }
        if (CheckWall() && !CheckGrounded())
        {

            rb.gravityScale = 1f;
            rb.velocity = Vector2.zero;
            jumpCounter = 1;
        }
        else
        {
            rb.gravityScale = 4f;
            rb.velocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);


            if (CheckGrounded())
            {
                jumpCounter = extraJump;
            }
        }
    }

    private void CheckAnimation()
    {
        animator.SetBool("isWalking", rb.velocity.x != 0);
        animator.SetFloat("yVelocity", rb.velocity.y);
        animator.SetBool("isGround", CheckGrounded());
        animator.SetBool("isCrouch", isCrouching);
    }

    private void Jump()
    {
        if (!CheckWall() && jumpCounter <= 0) return;

        if (CheckWall())
        {
            WallJump();
        }
        else
        {
                rb.velocity = new Vector2(rb.velocity.x, JumpForce);   
        }
        jumpCounter--;

    }
    private void WallJump()
    {
        rb.AddForce(new Vector2(-Mathf.Sign(transform.localScale.x) * wallJumpX, wallJumpY));
        
    }

    private void Flip()
    {
        if (rb.velocity.x > 0.01f)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (rb.velocity.x < -0.01f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    private bool CheckGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider2D.bounds.center, boxCollider2D.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }

    private bool CheckWall()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider2D.bounds.center, boxCollider2D.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.3f, wallLayer);
        return raycastHit.collider != null;
    }

    public bool CanAttack()
    {
        return rb.velocity.x == 0 && CheckGrounded() && !CheckWall();
    }

    private void Crouch()
    {
        if(Input.GetKeyDown(KeyCode.LeftShift) &&CheckGrounded() && !CheckWall())
        {
            isCrouching = true;
            boxcollider2D.size = new Vector2(originalSize.x, originalSize.y * crouchHeighMultiplier);
            boxcollider2D.offset = new Vector2(originalOffset.x, originalOffset.y - (originalSize.y - boxcollider2D.size.y) / 2);
        }else if (Input.GetKeyUp(KeyCode.LeftShift) && CheckGrounded() && !CheckWall())
        {
            isCrouching = false;
            
            boxcollider2D.size = originalSize;
            boxcollider2D.offset = originalOffset;
        }
    }
}
