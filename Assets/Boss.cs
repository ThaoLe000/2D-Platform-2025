using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float speed;

    [Header("TargetZone")]
    [SerializeField] private TargetZone zone;

    [Header("RangeAttack")]
    [SerializeField] private float rangeAttack;
    private Transform player;
    private Animator animator;
    private Rigidbody2D rb;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        IsPlayerInTargetZone();
        if (player != null)
        {
            MoveToTarget();
            Debug.Log("Da nhin thay");
        }
        Flip();
    }

    private bool IsPlayerInTargetZone()
    {
        if(zone.targetPlayer != null)
        {
            player = zone.targetPlayer;
            return true;
        }

        player = null;
        return false;
    }
    private void Flip()
    {
        if(player == null)
        {
            return;
        }

        if(rb.position.x < player.position.x)
        {
            rb.transform.localScale = new Vector3(-1, 1, 1);
        }else if( rb.position.x > player.position.x)
        {
            rb.transform.localScale = new Vector3(1, 1, 1);
        }
    }
    private void MoveToTarget()
    {
        Vector2 target = new Vector2(player.position.x, rb.position.y);
        Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
        if (Vector2.Distance(player.position, rb.position) >= rangeAttack)
        {
            animator.SetBool("isWalking", true);
        }
        rb.MovePosition(newPos);

        if (Vector2.Distance(player.position, rb.position) <= rangeAttack)
        {
            animator.SetBool("isWalking", false);

            
        }
    }
    
}
