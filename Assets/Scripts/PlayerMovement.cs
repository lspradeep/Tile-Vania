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

    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        defaultPlayerGravity = playerRB.gravityScale;
        gameManager = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        Run();
        FlipCharacter();
        ClimbLadder();
        gameManager.updatePlayerTransfrom(transform);
    }

    void OnMove(InputValue inputValue)
    {
        
            moveInput = inputValue.Get<Vector2>();
        
    }

    void OnJump(InputValue inputValue)
    {
        bool isPlayerOnGround = playerRB.IsTouchingLayers(LayerMask.GetMask("Platform"));
        if (inputValue.isPressed && isPlayerOnGround)
        {
            playerRB.velocity = new Vector2(0, jumpForce);
        }
    }

    void Run()
    {

        playerRB.velocity = new Vector2(moveInput.x * horizontalSpeed, playerRB.velocity.y);
        bool isPlayerRunning = Mathf.Abs(playerRB.velocity.x)  > Mathf.Epsilon;
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
        bool isPlayerOnLadder = playerRB.IsTouchingLayers(LayerMask.GetMask("Climbing"));
        playerAnimator.SetBool("isClimbing", isPlayerOnLadder && (Mathf.Abs(playerRB.velocity.y)>Mathf.Epsilon));
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
}
