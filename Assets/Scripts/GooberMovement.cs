using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GooberMovement : MonoBehaviour
{
    GameManager gameManager;
    Transform playerTransfrom;
    Rigidbody2D gooberRB;
    [SerializeField] float gooberSpeed = 4f;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        gooberRB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        playerTransfrom = gameManager.getPlayerTransfrom();
    }

    private void FixedUpdate()
    {
        FollowPlayer();
    }

    void FollowPlayer()
    {
        
        if (playerTransfrom != null)
        {
            gooberRB.position = Vector2.MoveTowards(transform.position, playerTransfrom.position, gooberSpeed * Time.deltaTime);

            float direction= (playerTransfrom.position - transform.position).normalized.x;
            //print((playerTransfrom.position - transform.position).normalized.x);
            //gooberRB.velocity = new Vector2(direction * gooberSpeed, gooberRB.velocity.y);
            if (direction > Mathf.Epsilon)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
            else
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
        }
    }
}
