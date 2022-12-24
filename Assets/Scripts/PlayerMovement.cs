using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    Rigidbody2D playerRB;
    [SerializeField] float horizontalSpeed = 5f;
    [SerializeField] float jumpForce = 10f;
    [SerializeField] float climbSpeed = 5f;
    Vector2 moveInput;
    Animator playerAnimator;
    float defaultPlayerGravity = 0f;
    GameManager gameManager;
    bool isPlayerAlive = true;
    CapsuleCollider2D playerBodyCollider;
    BoxCollider2D playerFeetCollider;

    [SerializeField] GameObject bullet;
    [SerializeField] Transform bulletPosition;

    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        defaultPlayerGravity = playerRB.gravityScale;
        gameManager = FindObjectOfType<GameManager>();
        playerBodyCollider = GetComponent<CapsuleCollider2D>();
        playerFeetCollider = GetComponent<BoxCollider2D>();
    }

    void FixedUpdate()
    {
        if (!isPlayerAlive)
        {
            return;
        }
        Run();
        FlipCharacter();
        ClimbLadder();
        gameManager.updatePlayerTransfrom(transform);
        Death();
    }


    void OnFire(InputValue inputValue)
    {
        if (isPlayerAlive)
        {
            Instantiate(bullet, bulletPosition.position,transform.rotation);
        }
    }

    void OnMove(InputValue inputValue)
    {

        if (isPlayerAlive)
        {
            moveInput = inputValue.Get<Vector2>();
        }

    }

    void OnJump(InputValue inputValue)
    {
        if (isPlayerAlive)
        {
            bool isPlayerFeetOnGround = playerFeetCollider.IsTouchingLayers(LayerMask.GetMask("Platform"));
            if (inputValue.isPressed && isPlayerFeetOnGround)
            {
                playerRB.velocity = new Vector2(0, jumpForce);
            }
        }

    }

    void Run()
    {

        playerRB.velocity = new Vector2(moveInput.x * horizontalSpeed, playerRB.velocity.y);
        bool isPlayerRunning = Mathf.Abs(playerRB.velocity.x) > Mathf.Epsilon;
        playerAnimator.SetBool("isRunning", isPlayerRunning);

    }

    void FlipCharacter()
    {
        if (Mathf.Abs(playerRB.velocity.x) > Mathf.Epsilon)
        {
            transform.localScale = new Vector2(Mathf.Sign(playerRB.velocity.x), 1);
        }
    }

    void ClimbLadder()
    {
        bool isPlayerOnLadder = playerFeetCollider.IsTouchingLayers(LayerMask.GetMask("Climbing"));
        playerAnimator.SetBool("isClimbing", isPlayerOnLadder && (Mathf.Abs(playerRB.velocity.y) > Mathf.Epsilon));
        if (isPlayerOnLadder)
        {
            playerRB.velocity = new Vector2(playerRB.velocity.x, moveInput.y * climbSpeed);
            playerRB.gravityScale = 0f;
        }
        else
        {
            playerRB.gravityScale = defaultPlayerGravity;
        }
    }

    void Death()
    {
        if (playerBodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemy", "Spikes"))
            || playerFeetCollider.IsTouchingLayers(LayerMask.GetMask("Enemy", "Spikes")))
        {
            isPlayerAlive = false;
        }
        if (!isPlayerAlive)
        {
            playerRB.velocity = new Vector2(playerRB.velocity.x, 20f);
            playerAnimator.SetTrigger("isPlayerDead");
        }
        gameManager.isPlayerAlive = isPlayerAlive;
    }
}
