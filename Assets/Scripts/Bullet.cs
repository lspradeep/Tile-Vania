using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private PlayerMovement playerMovment;
    private Rigidbody2D rb;
    private float direction;
    private GameManager gameManager;

    
    // Start is called before the first frame update
    void Start()
    {
        playerMovment = FindObjectOfType<PlayerMovement>();
        gameManager = FindObjectOfType<GameManager>();
        rb = GetComponent<Rigidbody2D>();
        direction = playerMovment.transform.localScale.x;
        StartCoroutine(SelfDestroy());
    }

    private IEnumerator SelfDestroy()
    {
        yield return new WaitForSecondsRealtime(1);
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(direction * 7f, 0f);

    }

    void OnTriggerEnter2D(Collider2D collision)
    {   
        if (collision.tag.Equals("Enemy"))
        {
            Destroy(collision.gameObject);
            UpdateKillCount();
        }
        print("OnTriggerEnter2D "+collision.tag);

        Destroy(gameObject);
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.collider.tag.Equals("Enemy"))
    //    {
    //        Destroy(collision.collider);
    //    }
    //    print("OnCollisionEnter2D "+collision.collider.tag);
    //    Destroy(gameObject);
    //}

    private void UpdateKillCount()
    {
        gameManager.UpdateKillCount();
    }
}
