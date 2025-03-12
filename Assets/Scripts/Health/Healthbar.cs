using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    [SerializeField] private Health playerHealth;
    [SerializeField] private Image totalHealthBar;
    [SerializeField] private Image currentHealthBar;

    private void Awake()
    {
        GameObject player = GameObject.FindWithTag("Player");
        if(player == null)
        {
            gameObject.SetActive(false);
        }
    }

    private void Start()
    {
        totalHealthBar.fillAmount = playerHealth.currentHealth / 10;
    }
    private void Update()
    {
        currentHealthBar.fillAmount = playerHealth.currentHealth / 10;
    }

}
