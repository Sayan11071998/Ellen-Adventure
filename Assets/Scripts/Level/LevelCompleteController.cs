using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelCompleteController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController playerController = other.gameObject.GetComponent<PlayerController>();

        if (playerController != null)
        {
            Debug.Log("Level Completed!!");
            playerController.DisablePlayerSprite();
            LevelManager.Instance.MarkCurrentLevelComplete();
        }
    }
}
