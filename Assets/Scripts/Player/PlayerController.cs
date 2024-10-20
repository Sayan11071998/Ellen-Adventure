using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private BoxCollider2D playerBoxCollider2d;
    [SerializeField] private Rigidbody2D playerRigidBody2d;
    [SerializeField] private GameUIController gameUIController;
    [SerializeField] private GameOverUIController gameOverUIController;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float playerHorizontalSpeed;
    [SerializeField] private float playerVerticalJumpHeight;
    [SerializeField] private int playerMaxNumberOfHealths;

    private int currentHealth;
    private Vector2 boxColInitSize;
    private Vector2 boxColInitOffset;
    private bool isGrounded;
    private bool isCrouch = false;
    private bool isHurt = false;
    private bool isDead = false;

    private void Awake()
    {
        playerAnimator = gameObject.GetComponent<Animator>();
        playerBoxCollider2d = gameObject.GetComponent<BoxCollider2D>();
        playerRigidBody2d = gameObject.GetComponent<Rigidbody2D>();
        gameUIController = FindObjectOfType<GameUIController>();
        // gameOverUIController = FindObjectOfType<GameOverUIController>();
    }

    private void Start()
    {
        boxColInitSize = playerBoxCollider2d.size;
        boxColInitOffset = playerBoxCollider2d.offset;
        currentHealth = playerMaxNumberOfHealths;
    }

    private void Update()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");

        if (!isDead)
        {
            PlayerMovement(horizontalInput);
            PlayMovementAnimation(horizontalInput);
            PlayerCrouch();
        }
    }

    private void FixedUpdate()
    {
        if (Input.GetButton("Jump") && isGrounded)
        {
            PlayerJump();
        }
    }

    public void PlayerMovement(float horizontalInput)
    {
        if (!isCrouch)
        {
            playerRigidBody2d.velocity = new Vector2(horizontalInput * playerHorizontalSpeed, playerRigidBody2d.velocity.y);
        }
    }

    public void PlayMovementAnimation(float horizontalInput)
    {
        playerAnimator.SetFloat("Speed", Mathf.Abs(horizontalInput));
        Vector2 localScale = transform.localScale;
        if (horizontalInput < 0)
        {
            localScale.x = -1f * Mathf.Abs(localScale.x);
        }
        else if (horizontalInput > 0)
        {
            localScale.x = Mathf.Abs(localScale.x);
        }
        transform.localScale = localScale;
    }

    public void PlayerJump()
    {
        playerRigidBody2d.velocity = new Vector2(playerRigidBody2d.velocity.x, playerVerticalJumpHeight);
        isGrounded = false;
        playerAnimator.SetBool("Jump", true);
    }

    public void PlayerGroundedCheck()
    {
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, 0.1f, groundLayer);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            playerAnimator.SetBool("Jump", false);
        }
    }

    public void PlayerCrouch()
    {
        if (isGrounded && Input.GetKey(KeyCode.LeftControl))
            isCrouch = true;
        else
            isCrouch = false;
        PlayCrouchAnimation(isCrouch);
    }

    public void PlayCrouchAnimation(bool crouchValue)
    {
        if (crouchValue == true)
        {
            float sizeValueX = 0.9508577f;
            float sizeValueY = 1.311901f;

            float offsetValueX = -0.1270343f;
            float offsetValueY = 0.5678566f;

            playerBoxCollider2d.size = new Vector2(sizeValueX, sizeValueY);
            playerBoxCollider2d.offset = new Vector2(offsetValueX, offsetValueY);
        }
        else
        {
            playerBoxCollider2d.size = boxColInitSize;
            playerBoxCollider2d.offset = boxColInitOffset;
        }
        playerAnimator.SetBool("Crouch", crouchValue);
    }

    public void PickupKey()
    {
        Debug.Log("Player picked up the Key!!");
        gameUIController.IncreaseScore(10);
    }

    public void DecreasePlayerHealth(int damageValue)
    {
        if (isHurt || isDead) return;

        playerMaxNumberOfHealths -= damageValue;
        PlayHurtAnimation();
        CheckPlayerDeathCondition();
    }

    public void PlayHurtAnimation()
    {
        isHurt = true;
        playerAnimator.SetBool("isHurt", true);
        StartCoroutine(DisableMovementForHurt());
        StartCoroutine(ResetHurtStatusAfterAnimation());
    }

    private IEnumerator DisableMovementForHurt()
    {
        playerRigidBody2d.velocity = Vector2.zero;
        playerRigidBody2d.isKinematic = true;
        yield return new WaitForSeconds(1f);
        playerRigidBody2d.isKinematic = false;
    }

    private IEnumerator ResetHurtStatusAfterAnimation()
    {
        yield return new WaitForSeconds(0.3f);
        isHurt = false;
        playerAnimator.SetBool("isHurt", false);
    }

    public void CheckPlayerDeathCondition()
    {
        if (playerMaxNumberOfHealths < 1)
        {
            KillPlayer();
        }
    }

    public void KillPlayer()
    {
        if (isDead) return;

        isDead = true;
        Debug.Log("Player Killed by Enemy!!");
        PlayDeathAnimation();
        playerRigidBody2d.velocity = Vector2.zero;
        playerRigidBody2d.isKinematic = true;
        StartCoroutine(WaitForDeathAnimation());
    }

    public void PlayDeathAnimation()
    {
        Debug.Log("Play Death Animation");
        playerAnimator.SetBool("isDead", true);
    }

    private IEnumerator WaitForDeathAnimation()
    {
        Debug.Log("Inside Wait for Death Animation");

        float deathAnimationDuration = playerAnimator.GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(deathAnimationDuration + 2);
        // ReloadLevel();
        gameOverUIController.PlayerDied();
    }

    public int getPlayerLives()
    {
        return playerMaxNumberOfHealths;
    }
}