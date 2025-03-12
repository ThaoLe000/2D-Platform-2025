using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rockhead : EnemyDamage
{
    private Vector3 destination;
    private Vector3 initialPosition; 
    [SerializeField] private float speed;
    [SerializeField] private float range;
    [SerializeField] private float checkDelay;
    public LayerMask playerLayer;
    private float checkTimer;
    private bool attack;
    private bool returning; 

    private Vector3[] directions = new Vector3[4];

    private void Start()
    {
        initialPosition = transform.position; 
    }

    private void OnEnable()
    {
        Stop();
    }

    private void Update()
    {
        if (attack)
        {
            transform.Translate(destination * Time.deltaTime * speed);

            if (Vector3.Distance(transform.position, destination) < 0.1f)
            {
                attack = false;
                returning = true;
            }
        }
        else if (returning)
        {
            transform.position = Vector3.MoveTowards(transform.position, initialPosition, speed * Time.deltaTime);

            if (Vector3.Distance(transform.position, initialPosition) < 0.1f)
            {
                returning = false;
                Stop();
            }
        }
        else
        {
            checkTimer += Time.deltaTime;
            if (checkTimer > checkDelay)
            {
                CheckForPlayer();
            }
        }
    }

    private void CheckForPlayer()
    {
        CalculateDirections();
        for (int i = 0; i < directions.Length; i++)
        {
            Debug.DrawRay(transform.position, directions[i], Color.red);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, directions[i], range, playerLayer);

            if (hit.collider != null && !attack)
            {
                attack = true;
                destination = directions[i];
                checkTimer = 0;
            }
        }
    }

    private void CalculateDirections()
    {
        directions[0] = transform.right * speed;
        directions[1] = -transform.right * speed;
        directions[2] = transform.up * speed;
        directions[3] = -transform.up * speed;
    }

    private void Stop()
    {
        destination = transform.position;
        attack = false;
        returning = false;
    }

    private new void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        attack = false;
        returning = true; 
    }
}
