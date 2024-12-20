using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelCompleteController : MonoBehaviour
{
    [SerializeField] private GameObject levelCompleteUIController;

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController playerController = other.gameObject.GetComponent<PlayerController>();

        if (playerController != null)
        {
            AudioManager.Instance.PlaySFX(AudioTypeList.LevelComplete);

            playerController.DisablePlayerSprite();
            LevelManager.Instance.MarkCurrentLevelComplete();
            levelCompleteUIController.SetActive(true);
        }
    }
}
