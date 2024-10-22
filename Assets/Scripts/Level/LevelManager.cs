using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private static LevelManager instance;
    public static LevelManager Instance { get { return instance; } }
    public string[] Levels;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if (GetLevelStatus(Levels[0]) == LevelStatus.LOCKED)
            SetLevelStatus(Levels[0], LevelStatus.UNLOCKED);
    }

    public void MarkCurrentLevelComplete()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        Instance.SetLevelStatus(currentScene.name, LevelStatus.COMPLETED);

        UnlockNextLevel(currentScene);
    }

    public void UnlockNextLevel(Scene currentScene)
    {
        int currentSceneIndex = Array.FindIndex(Levels, level => level == currentScene.name);
        int nextSceneIndex = currentSceneIndex + 1;

        if (CheckIfValidLevel(nextSceneIndex))
            SetLevelStatus(Levels[nextSceneIndex], LevelStatus.UNLOCKED);
    }

    public bool CheckIfValidLevel(int levelIndex)
    {
        if (levelIndex < Levels.Length)
            return true;
        else
            return false;
    }

    public LevelStatus GetLevelStatus(string level)
    {
        LevelStatus levelStatus = (LevelStatus)PlayerPrefs.GetInt(level, 0);
        return levelStatus;
    }

    public void SetLevelStatus(string level, LevelStatus levelStatus)
    {
        PlayerPrefs.SetInt(level, (int)levelStatus);
    }
}