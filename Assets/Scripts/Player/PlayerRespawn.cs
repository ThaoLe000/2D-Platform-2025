using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    [SerializeField] private AudioClip checkpointSound;
    private Transform currentCheckpoint;
    private Health playerHealth;
    private UIManager uiManager;

    private void Awake()
    {
        playerHealth = GetComponent<Health>();
        uiManager = FindObjectOfType<UIManager>();
    }
    public void CheckRespawn()
    {
        Debug.Log("Player dich chuyen");
        if (currentCheckpoint == null)
        {
            uiManager.GameOver();
            return;
        }
        transform.position = currentCheckpoint.position;
        playerHealth.Respawn();
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "CheckPoint")
        {
            currentCheckpoint = collision.transform;
            SoundManager.instance.PlaySound(checkpointSound);
            collision.GetComponent<Collider2D>().enabled = false;
            collision.GetComponent<Animator>().SetTrigger("Appear");
        }
    }
}
