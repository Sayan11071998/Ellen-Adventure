using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameUIController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private CanvasRenderer[] hearts;

    private int playerScore = 0;

    private void Awake()
    {
        scoreText = gameObject.GetComponent<TextMeshProUGUI>();
        playerController = FindObjectOfType<PlayerController>();
    }

    private void Start()
    {
        foreach (CanvasRenderer heart in hearts)
        {
            heart.gameObject.SetActive(true);
        }

        RefreshUI();

        AudioManager.Instance.MuteAudioSource(AudioSourceList.audioSourcePlayer, false);
        AudioManager.Instance.MuteAudioSource(AudioSourceList.audioSourceEnemy, false);
        AudioManager.Instance.MuteAudioSource(AudioSourceList.audioSourceSFX, false);
        AudioManager.Instance.MuteAudioSource(AudioSourceList.audioSourceBGM, false);
        AudioManager.Instance.PlayBGM(AudioTypeList.BackGroundMusic);
    }

    private void Update()
    {
        RefreshHealthUI();
    }

    public void IncreaseScore(int increment)
    {
        playerScore += increment;
        RefreshUI();
    }

    public void RefreshUI()
    {
        scoreText.text = "Score: " + playerScore.ToString();
    }

    public void RefreshHealthUI()
    {
        int playerHearts = playerController.getPlayerLives();

        foreach (CanvasRenderer heart in hearts)
        {
            heart.gameObject.SetActive(false);
        }

        for (int i = 0; i < playerHearts; i++)
        {
            hearts[i].gameObject.SetActive(true);
        }
    }
}
