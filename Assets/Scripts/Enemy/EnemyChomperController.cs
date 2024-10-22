using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class EnemyChomperController : MonoBehaviour
{
    [SerializeField] private Animator enemyChomperAnimator;
    [SerializeField] private SpriteRenderer enemySpriteRenderer;
    [SerializeField] private Rigidbody2D enemyRigidBody2d;
    [SerializeField] private float enemyPatrolspeed;
    [SerializeField] private Transform groundDetector;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float footstepDelay = 0.5f;  // Time between each footstep sound

    private bool isFacingRIght = true;
    private float nextFootstepTime = 0f;
    private bool hasAttackedPlayer = false;

    private void Awake()
    {
        enemyChomperAnimator = gameObject.GetComponent<Animator>();
        enemySpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        enemyRigidBody2d = gameObject.GetComponent<Rigidbody2D>();
        groundDetector = gameObject.GetComponent<Transform>();
    }

    private void Update()
    {
        Patrol();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        PlayerController playerController = other.gameObject.GetComponent<PlayerController>();

        if (playerController != null)
        {
            playerController.DecreasePlayerHealth(1);
            hasAttackedPlayer = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        PlayerController playerController = other.gameObject.GetComponent<PlayerController>();

        if (playerController != null)
            hasAttackedPlayer = false;

    }

    void Patrol()
    {
        transform.Translate(Vector2.right * enemyPatrolspeed * Time.deltaTime);
        enemyChomperAnimator.SetBool("IsPatrolling", true);
        
        RaycastHit2D hit = Physics2D.Raycast(groundDetector.position, Vector2.down, 1f, groundLayer);

        if (hit.collider == false)
            Flip();

        // Play footstep sound at intervals
        if (Time.time >= nextFootstepTime)
        {
            AudioManager.Instance.PlayEnemyFootestepAudio(AudioTypeList.EnemyFootstep);
            nextFootstepTime = Time.time + footstepDelay;
        }
    }

    void Flip()
    {
        isFacingRIght = !isFacingRIght;

        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
        enemyPatrolspeed = -enemyPatrolspeed;
    }
}
