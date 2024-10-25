using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameCompleteUIController : MonoBehaviour
{
    [SerializeField] private GameObject gameCompleteUIController;
    [SerializeField] private Button buttonMainMenu;

    private void Awake()
    {
        buttonMainMenu.onClick.AddListener(ReturnToLobby);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController playerController = other.gameObject.GetComponent<PlayerController>();

        if (playerController != null)
        {
            AudioManager.Instance.PlaySFX(AudioTypeList.LevelComplete);

            playerController.DisablePlayerSprite();
            LevelManager.Instance.MarkCurrentLevelComplete();
            gameCompleteUIController.SetActive(true);
        }
    }

    public void ReturnToLobby()
    {
        AudioManager.Instance.PlaySFX(AudioTypeList.MenuButtonClick_MainMenu_Back);
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }
}
