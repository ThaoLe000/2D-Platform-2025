using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shrine : MonoBehaviour
{
    private void Awake()
    {
        PlayerPrefs.DeleteKey("Shrine");
    }
    private void Start()
    {
        if (PlayerPrefs.GetInt("Shrine")== 1)
        {
            this.enabled = false;
        }
        else
        {
            this.enabled = true;
            if(PlayerPrefs.HasKey("Shrine")== false)
            {
                PlayerPrefs.SetInt("Shrine", 0);
            }
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            collision.GetComponent<Energy>().AddTotalEnergy(1);
            PlayerPrefs.SetInt("Shrine", 1);
            this.enabled= false;
            Debug.Log("Da nhan");
        }
    }
}
