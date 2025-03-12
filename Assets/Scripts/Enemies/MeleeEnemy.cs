using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : MonoBehaviour
{
    [Header("Attack Parameter")]
    [SerializeField] private float attackCooldown;
    [SerializeField] private int damage;
    [SerializeField] private float range;

    [Header("Collider parameter")]
    [SerializeField] private BoxCollider2D boxCollider2D;
    [SerializeField] private float colliderDistance;

    [Header("Player Layer")]
    [SerializeField] private LayerMask playerLayer;
    private float cooldownTimer = Mathf.Infinity;


    [SerializeField] private AudioClip attackSound;


    private Animator animator;

    private Health playerHealth;
    private EnemyPatrol enemyPatrol;
    private Boss boss;
    private void Start()
    {
        animator = GetComponent<Animator>();
        enemyPatrol = GetComponent<EnemyPatrol>();
        boss = GetComponent<Boss>();
    }

    private void Update()
    {
        cooldownTimer += Time.deltaTime;

        if (PlayerInSight())
        {
            if (cooldownTimer >= attackCooldown)
            {
                SoundManager.instance.PlaySound(attackSound);
                cooldownTimer = 0;
                animator.SetTrigger("isAttack");
                
            }
        }

        if (enemyPatrol != null)
        {
            enemyPatrol.enabled = !PlayerInSight();
        }else if(boss != null)
        {
            boss.enabled = !PlayerInSight();
        }
    }
    private bool PlayerInSight()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider2D.bounds.center + transform.right *range * transform.localScale.x * colliderDistance  
            ,new Vector2(boxCollider2D.bounds.size.x * range, boxCollider2D.bounds.size.y), 0, Vector2.left,0,playerLayer);

        if(hit.collider != null)
        {
            playerHealth = hit.collider.GetComponent<Health>();
        }

        return hit.collider != null;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider2D.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector2(boxCollider2D.bounds.size.x * range, boxCollider2D.bounds.size.y));
    }
    private void DamagePlayer()
    {
        if (PlayerInSight())
        {
            playerHealth.TakeDamage(damage);
        }
    }

}
