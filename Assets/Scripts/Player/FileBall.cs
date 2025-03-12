using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FileBall : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float direction;
    [SerializeField] private bool hit;
    private float lifetime;

    private BoxCollider2D boxcollider2D;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        boxcollider2D = GetComponent<BoxCollider2D>();
    }
    private void Update()
    {
        if (hit)
        {
            return;
        }
        float moveSpeed = speed * Time.deltaTime * direction;
        transform.Translate(moveSpeed, 0 ,0);

        TimeLife();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "BackGround") return;
        hit = true;
        boxcollider2D.enabled = false;
        animator.SetTrigger("isExplode");

        if(collision.tag == "Enemy")
        {
            collision.GetComponent<Health>().TakeDamage(1);
        }
    }
    public void SetDirection(float _direction)
    {
        lifetime = 0;
        direction = _direction;
        gameObject.SetActive(true);
        hit = false;
        boxcollider2D.enabled=true;

        float localScaleX = transform.localScale.x;
        if(Mathf.Sign(localScaleX) != _direction)
        {
            localScaleX = -localScaleX;
        }
        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
    }
    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
    private void TimeLife()
    {
        lifetime += Time.deltaTime;
        if (lifetime > 5)
        {
            gameObject.SetActive (false);
        }
    }
}
