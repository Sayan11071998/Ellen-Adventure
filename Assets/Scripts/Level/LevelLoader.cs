using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class LevelLoader : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private string LevelName;

    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnCLick);
    }

    private void OnCLick()
    {
        LevelStatus levelStatus = LevelManager.Instance.GetLevelStatus(LevelName);
        switch (levelStatus)
        {
            case LevelStatus.LOCKED:
                Debug.Log("Can't play this Level till you unlock it");
                AudioManager.Instance.PlaySFX(AudioTypeList.MenuButtonClick_Locked);
                break;
            case LevelStatus.UNLOCKED:
                AudioManager.Instance.PlaySFX(AudioTypeList.MenuButtonClick_Unlocked);
                SceneManager.LoadScene(LevelName);
                break;
            case LevelStatus.COMPLETED:
                AudioManager.Instance.PlaySFX(AudioTypeList.MenuButtonClick_NextLevel_Restart);
                SceneManager.LoadScene(LevelName);
                break;
        }
    }
}