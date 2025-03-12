using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firetrap : MonoBehaviour
{
    [SerializeField] private float damage;

    [Header("Firetrap Timers")]
    [SerializeField] private float activationDelay;
    [SerializeField] private float activeTime;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private bool triggered;
    private bool active;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (!triggered)
            {
                StartCoroutine(ActivateFiretrap());
            }
            if (active)
            {
                collision.GetComponent<Health>().TakeDamage(damage);
            }
        }
    }
    private IEnumerator ActivateFiretrap()
    {
        triggered = true;
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(activationDelay);

        active = true;
        spriteRenderer.color = Color. white;
        animator.SetBool("isActived",active);
        yield return new WaitForSeconds(activeTime);

        triggered = false;
        active = false;
        animator.SetBool("isActived", active);
    }

}
