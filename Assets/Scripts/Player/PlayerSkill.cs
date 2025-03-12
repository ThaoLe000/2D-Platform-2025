using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill : MonoBehaviour
{
    private Energy energyPlayer;
    private Rigidbody2D rb;
    [SerializeField] private float energyUse;
    [SerializeField] private float dashSpeed;
    [SerializeField] private float dashDuration;
    [SerializeField] private GameObject dashEffectObj;


    private void Awake()
    {
        energyPlayer = GetComponent<Energy>();
        rb = GetComponent<Rigidbody2D>();
    }
   

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftShift) && energyPlayer.energyCurrent >= energyUse)
        {
            energyPlayer.UseEnergy(energyUse);
            StartCoroutine(Dash());
        }
    }

    private IEnumerator Dash()
    {
        float elapsedTime = 0f;
        while (elapsedTime < dashDuration)
        {
            rb.velocity = new Vector2(dashSpeed * transform.localScale.x, rb.velocity.y);
            elapsedTime += Time.deltaTime;
            dashEffectObj.SetActive(true);
            yield return null;
        }
        rb.velocity = new Vector2(0, rb.velocity.y);
        dashEffectObj.SetActive(false);


        
    }
    
}
