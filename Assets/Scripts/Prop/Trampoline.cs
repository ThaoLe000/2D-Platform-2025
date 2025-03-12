using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampoline : MonoBehaviour
{

    [SerializeField] private float AddForce;
    private Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();

            if(rb != null) 
            {
                animator.SetTrigger("isActived");
                rb.velocity = new Vector2(rb.velocity.x, AddForce);
            }
            
        }
    }
}
