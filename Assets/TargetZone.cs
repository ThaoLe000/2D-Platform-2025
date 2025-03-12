using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetZone : MonoBehaviour
{
    [SerializeField] private Collider2D targetZone;
    public Transform targetPlayer;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            targetPlayer = collision.transform;
            targetZone.isTrigger = true;

        }
        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            targetZone.isTrigger = false;
        }
    }
}
