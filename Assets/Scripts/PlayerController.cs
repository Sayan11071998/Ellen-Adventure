using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private BoxCollider2D boxCol;

    //Collider Variables
    private Vector2 boxColInitSize;
    private Vector2 boxColInitOffset;

    private void Start()
    {
        //Fetching initial collider properties
        boxColInitSize = boxCol.size;
        boxColInitOffset = boxCol.offset;
    }

    private void Update()
    {
        float speed = Input.GetAxisRaw("Horizontal");
        float VerticalInput = Input.GetAxis("Vertical");
        
        // Run Animation
        playerAnimator.SetFloat("Speed", Mathf.Abs(speed));
        // Flipping the Player
        Vector3 scale = transform.localScale;
        if (speed < 0) {
            scale.x = -1f * Mathf.Abs(scale.x);
        } else if (speed > 0) {
            scale.x = Mathf.Abs(scale.x);
        }
        transform.localScale = scale;

        // Jump Animation
        PlayJumpAnimation(VerticalInput);
        if (Input.GetKey(KeyCode.LeftControl)) {
            Crouch(true);
        } else {
            Crouch(false);
        }
    }

    public void Crouch (bool crouch) {
        if (crouch == true) {
            float offX = -0.1270343f;
            float offY = 0.5678566f;

            float sizeX = 0.9508577f;
            float sizeY = 1.311901f;

            boxCol.size = new Vector2(sizeX, sizeY);
            boxCol.offset = new Vector2(offX, offY);
        } else {
            //Reset collider to initial values
            boxCol.size = boxColInitSize;
            boxCol.offset = boxColInitOffset;
        }

        //Play Crouch animation
        playerAnimator.SetBool("Crouch", crouch);
    }

    public void PlayJumpAnimation (float vertical) {
        if(vertical > 0) {
            playerAnimator.SetTrigger("Jump");
        }
    }
}
