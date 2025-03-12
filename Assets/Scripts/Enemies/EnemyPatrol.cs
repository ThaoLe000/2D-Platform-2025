using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [Header("Patrol Point")]
    [SerializeField] private float distance;


    [Header("Enemy")]
    [SerializeField] private float speed;
    private Vector2 initScale;
    private Vector2 initPosition;
    private bool movingLeft;
    [SerializeField] private float idleDuration;
    private float idleTimer;

    [SerializeField] private Animator animator;
    private void Awake()
    {
        initScale = transform.localScale;
        initPosition = transform.position;
    }

    private void Update()
    {
        if (movingLeft)
        {
            if (transform.position.x >= initPosition.x - distance)
            {
                MoveInDirection(-1);
            }
            else
            {
                DirectionChange();
            }
        }
        else
        {
            if (transform.position.x <= initPosition.x + distance)
            {
                MoveInDirection(1);
            }
            else
            {
                DirectionChange();
            }
        }
    }
    private void DirectionChange()
    {
        animator.SetBool("isWalking", false);
        idleTimer += Time.deltaTime;
        if (idleTimer > idleDuration)
        {
            movingLeft = !movingLeft;
        }
    }
    private void OnDisable()
    {
        animator.SetBool("isWalking", false);
    }
    private void MoveInDirection(int _direction)
    {
        animator.SetBool("isWalking", true);
        idleTimer = 0;

        transform.localScale = new Vector2 (Mathf.Abs(initScale.x) * _direction, initScale.y);
        transform.position = new Vector2(transform.position.x + Time.deltaTime * _direction * speed, transform.position.y);
    }
}
