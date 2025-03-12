using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private float startingHealth;
    private Animator animator;
    public float currentHealth {  get; private set; }
    private bool isDead;

    [Header ("iFrames")]
    [SerializeField] private float iFramesDuration;
    [SerializeField] private int numberOfFlashes;
    private SpriteRenderer spriteRenderer;

    [Header("Components")]
    [SerializeField] private Behaviour[] components;
    [SerializeField] private AudioClip deathSound;
    private void Awake()
    {
        currentHealth = startingHealth;
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public void TakeDamage(float _damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);

        if (currentHealth > 0)
        {
            animator.SetTrigger("isHurt");
            StartCoroutine(Invunerability());
        }
        else
        {
            if (!isDead)
            {
                animator.SetBool("isGround", true);
                foreach (Behaviour item in components)
                {
                    item.enabled = false;
                }

                animator.SetTrigger("isDie");


                isDead = true;
                SoundManager.instance.PlaySound(deathSound);
            }
        }
    }
    public void AddHealth(float value)
    {
        currentHealth = Mathf.Clamp(currentHealth + value, 0, startingHealth);
    }
    private IEnumerator Invunerability()
    {
        Physics2D.IgnoreLayerCollision(10, 11, true);
        for (int i = 0; i < numberOfFlashes; i++)
        {
            spriteRenderer.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(iFramesDuration /(numberOfFlashes *2));
            spriteRenderer.color = Color.white;
            yield return new WaitForSeconds(iFramesDuration/(numberOfFlashes * 2));
        }

        Physics2D.IgnoreLayerCollision(10, 11, false);
    }

    private void Deativate()
    {
        gameObject.SetActive(false);
    }
    public void Respawn()
    {
        isDead = false;
        AddHealth(startingHealth);
        animator.ResetTrigger("isDie");
        animator.Play("Idle");

        foreach (Behaviour item in components)
        {
            item.enabled = true;
        }
    }
}
