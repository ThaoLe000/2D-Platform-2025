using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Animator animator;
    private PlayerController playerController;
    private Energy playerEnergy;
    [SerializeField] private float energyUse;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] fireballs;
    [SerializeField] private AudioClip fireballSound;
    [SerializeField] private float attackCooldown;
    private float cooldownTimer = 1000;
    
    [Header("RangeAttack")]
    [SerializeField] private float damage;
    [SerializeField] private BoxCollider2D box;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private float range;
    [SerializeField] private float colliderDistance;
    [SerializeField] private AudioClip swordSound;
    private Health enemyHealth;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        playerController = GetComponent<PlayerController>();
        playerEnergy = GetComponent<Energy>();

    }

    private void Update()
    {
        Attack();
        cooldownTimer += Time.deltaTime;
    }
    private void Attack()
    {
        if (Input.GetKeyDown(KeyCode.E) && cooldownTimer > attackCooldown && playerEnergy.energyCurrent >= energyUse)
        {
            playerEnergy.UseEnergy(energyUse);
            SoundManager.instance.PlaySound(fireballSound);
            cooldownTimer = 0;
            animator.SetTrigger("isCast");
            fireballs[FindFireBall()].transform.position = firePoint.position;
            fireballs[FindFireBall()].GetComponent<FileBall>().SetDirection(Mathf.Sign(transform.localScale.x));
        }
        if(Input.GetKeyDown(KeyCode.F) && cooldownTimer > attackCooldown && playerController.CanAttack())
        {
            SoundManager.instance.PlaySound(swordSound);
            cooldownTimer = 0;
            animator.SetTrigger("isAttack");
        }

    }
    private int FindFireBall()
    {
        for (int i = 0; i < fireballs.Length; i++)
        {
            if (!fireballs[i].activeInHierarchy)
            {
                return i;
            }
        }
        return 0;
    }
    private bool RangeAttack()
    {
        RaycastHit2D hit = Physics2D.BoxCast(box.bounds.center + transform.right * range * transform.localScale.x * colliderDistance
            , new Vector2(box.bounds.size.x * range, box.bounds.size.y), 0, Vector2.left, 0, enemyLayer);

        if(hit.collider != null)
        {
            enemyHealth = hit.collider.GetComponent<Health>();
        }
        return hit.collider != null;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(box.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector2(box.bounds.size.x * range, box.bounds.size.y));
    }
    private void DamageEnemy()
    {
        if (RangeAttack())
        {
            enemyHealth.TakeDamage(damage);
        }
    }
}
