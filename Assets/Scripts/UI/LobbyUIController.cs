using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LobbyUIController : MonoBehaviour
{
    [SerializeField] private Button buttonPlay;
    [SerializeField] private Button buttonQuit;
    [SerializeField] private GameObject LevelSelection;

    private void Awake()
    {
        buttonPlay.onClick.AddListener(PlayGame);
        buttonQuit.onClick.AddListener(QuitGame);
    }

    public void PlayGame()
    {
        AudioManager.Instance.PlaySFX(AudioTypeList.MenuButtonClick_NextLevel_Restart);
        LevelSelection.SetActive(true);
    }

    public void QuitGame()
    {
        AudioManager.Instance.PlaySFX(AudioTypeList.MenuButtonClick_MainMenu_Back);
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
