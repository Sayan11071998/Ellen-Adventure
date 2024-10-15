using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private BoxCollider2D playerBoxCollider2d;
    [SerializeField] private Rigidbody2D playerRigidBody2d;
    [SerializeField] private float playerHorizontalSpeed;
    [SerializeField] private float playerVerticalJumpHeight;

    private Vector2 boxColInitSize;
    private Vector2 boxColInitOffset;

    // private bool isGrounded = false;

    private void Awake()
    {
        playerRigidBody2d = gameObject.GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        boxColInitSize = playerBoxCollider2d.size;
        boxColInitOffset = playerBoxCollider2d.offset;
    }

    private void Update()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Jump");

        MoveCharacterHorizontally(horizontalInput, verticalInput);
        PlayMovementAnimation(horizontalInput, verticalInput);

        if (Input.GetKey(KeyCode.LeftControl))
        {
            PlayCrouchAnimation(true);
        }
        else
        {
            PlayCrouchAnimation(false);
        }
    }

    public void MoveCharacterHorizontally(float horizontalInput, float verticalInput)
    {
        // Move Character Horizontally
        Vector3 position = transform.position;
        position.x += horizontalInput * playerHorizontalSpeed * Time.deltaTime;
        transform.position = position;

        // Move Character Vertically
        if (verticalInput > 0)
        {
            playerRigidBody2d.AddForce(new Vector2(0f, playerVerticalJumpHeight), ForceMode2D.Force);
        }
    }

    public void PlayMovementAnimation(float horizontalInput, float verticalInput)
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

        // Jump
        if (verticalInput > 0)
        {
            playerAnimator.SetBool("Jump", true);
        }
        else
        {
            playerAnimator.SetBool("Jump", false);
        }
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
}