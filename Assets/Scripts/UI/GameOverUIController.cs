using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverUIController : MonoBehaviour
{
    [SerializeField] private Button buttonRestart;
    [SerializeField] private Button buttonMainMenu;

    private void Awake()
    {
        buttonRestart.onClick.AddListener(ReloadLevel);
        buttonMainMenu.onClick.AddListener(ReturnToLobby);
    }

    public void PlayerDied()
    {
        this.gameObject.SetActive(true);
    }

    public void ReloadLevel()
    {
        AudioManager.Instance.PlaySFX(AudioTypeList.MenuButtonClick_NextLevel_Restart);
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    public void ReturnToLobby()
    {
        AudioManager.Instance.PlaySFX(AudioTypeList.MenuButtonClick_MainMenu_Back);
        SceneManager.LoadScene(0);
    }
}
