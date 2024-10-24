using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FallDetectionController : MonoBehaviour
{
    [SerializeField] private GameOverUIController gameOverUIController;

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController playerController = other.gameObject.GetComponent<PlayerController>();

        if (playerController != null)
        {
            AudioManager.Instance.PlaySFX(AudioTypeList.PlayerDeath);
            gameOverUIController.PlayerDied();
        }
    }
}
