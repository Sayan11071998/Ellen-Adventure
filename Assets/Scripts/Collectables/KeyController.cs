using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyController : MonoBehaviour
{
    [SerializeField] private float keyMoveUpDistance;
    [SerializeField] private float keyMoveUpSpeed;
    [SerializeField] private float keyFadeOutSpeed;
    [SerializeField] private SpriteRenderer keyRenderer;

    private bool isCollected = false;
    private Vector3 targetPosition;
    private Color keyInitialColor;

    private void Awake()
    {
        keyRenderer = gameObject.GetComponent<SpriteRenderer>();
        keyInitialColor = keyRenderer.color;
    }

    private void Update()
    {
        if (isCollected)
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition, keyMoveUpSpeed * Time.deltaTime);

            Color newKeyColor = keyRenderer.color;
            newKeyColor.a -= keyFadeOutSpeed * Time.deltaTime;
            keyRenderer.color = newKeyColor;

            if (newKeyColor.a <= 0)
                Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController playerController = other.gameObject.GetComponent<PlayerController>();

        if (playerController != null)
        {
            isCollected = true;
            targetPosition = transform.position + Vector3.up * keyMoveUpDistance;
            AudioManager.Instance.PlaySFX(AudioTypeList.KeyPickup);
            playerController.PickupKey();
        }
    }
}
