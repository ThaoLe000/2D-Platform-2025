using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fan : MonoBehaviour
{
    [SerializeField] private float rayDistance;
    [SerializeField] private float addForce;
    [SerializeField] private LayerMask playerLayer;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void Start()
    {
        animator.SetBool("isActived", true);
    }
    private void FixedUpdate()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.up, rayDistance, playerLayer);

        if(hit.collider != null && hit.collider.tag == "Player")
        {
            Rigidbody2D rb = hit.collider.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.AddForce(Vector2.up * addForce, ForceMode2D.Force);
            }
        }
        Debug.DrawRay(transform.position, Vector2.up * rayDistance, Color.green);
    }

    
}
