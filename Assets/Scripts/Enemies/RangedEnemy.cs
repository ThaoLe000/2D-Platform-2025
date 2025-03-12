using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : MonoBehaviour
{
    [Header("Attack Parameters")]
    [SerializeField] private float attackCooldown;
    [SerializeField] private float range;
    [SerializeField] private int damage;

    [Header("Ranged Attack")]
    [SerializeField] private Transform firepoint;
    [SerializeField] private GameObject[] fireballs;

    [Header("Collider parameter")]
    [SerializeField] private float colliderDistance;
    [SerializeField] private BoxCollider2D box;

    [Header("Player Layer")]
    [SerializeField] private LayerMask playerLayer;
    private float cooldownTimer = Mathf.Infinity;


    private Animator animator;
    private Health playerHealth;
    private EnemyPatrol enemyPatrol;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        enemyPatrol = GetComponent<EnemyPatrol>();
    }
    private void Update()
    {
        cooldownTimer += Time.deltaTime;

        if (PlayerInSight())
        {
            if (cooldownTimer >= attackCooldown)
            {
                //SoundManager.instance.PlaySound(attackSound);
                cooldownTimer = 0;
                animator.SetTrigger("isAttack");

            }
        }

        if (enemyPatrol != null)
        {
            enemyPatrol.enabled = !PlayerInSight();
        }
    }
    private void RangeAttack()
    {
        cooldownTimer = 0;
        fireballs[0].transform.position = firepoint.position;
        
    }
    private bool PlayerInSight()
    {
        RaycastHit2D hit = Physics2D.BoxCast(box.bounds.center + transform.right * range * transform.localScale.x * colliderDistance
            , new Vector2(box.bounds.size.x * range, box.bounds.size.y), 0, Vector2.left, 0, playerLayer);

        if (hit.collider != null)
        {
            playerHealth = hit.collider.GetComponent<Health>();
        }

        return hit.collider != null;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(box.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector2(box.bounds.size.x * range, box.bounds.size.y));
    }
    private void DamagePlayer()
    {
        if (PlayerInSight())
        {
            playerHealth.TakeDamage(damage);
        }
    }
}