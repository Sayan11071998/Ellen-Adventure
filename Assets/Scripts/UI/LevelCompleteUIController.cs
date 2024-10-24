using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelCompleteUIController : MonoBehaviour
{
    [SerializeField] private Button playNextLevel;
    [SerializeField] private Button buttonMainMenu;

    private void Awake()
    {
        playNextLevel.onClick.AddListener(PlayNextLevel);
        buttonMainMenu.onClick.AddListener(ReturnToLobby);
    }

    public void PlayNextLevel()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

        if (LevelManager.Instance.CheckIfValidLevel(nextSceneIndex))
        {
            AudioManager.Instance.PlaySFX(AudioTypeList.MenuButtonClick_NextLevel_Restart);
            SceneManager.LoadScene(nextSceneIndex);
        }
    }

    public void ReturnToLobby()
    {
        AudioManager.Instance.PlaySFX(AudioTypeList.MenuButtonClick_MainMenu_Back);
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }
}
