using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;
    public static AudioManager Instance { get { return instance; } }

    public AudioSource audioSourceBGM;
    public AudioSource audioSourceSFX;
    public AudioSource audioSourcePlayer;
    public AudioSource audioSourceEnemy;
    public AudioType[] audioList;

    public bool isMute = false;
    public float bgmVolume = 1f;
    public float sfxVolume = 1f;

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
        SetGameVolume(0.2f, 0.5f);
        PlayBGM(global::AudioTypeList.BackGroundMusic);
    }

    public void Mute(bool status)
    {
        isMute = status;
        audioSourceBGM.mute = status;
        audioSourceSFX.mute = status;
    }

    public void SetGameVolume(float bgmVolume, float sfxVolume)
    {
        audioSourceBGM.volume = bgmVolume;
        audioSourceSFX.volume = sfxVolume;
    }

    public AudioClip GetAudioClip(AudioTypeList audio)
    {
        AudioType audioItem = Array.Find(audioList, item => item.audioType == audio);
        if (audioItem != null)
            return audioItem.audioClip;
        return null;
    }

    public void PlayBGM(AudioTypeList audio)
    {
        if (isMute) return;

        AudioClip clip = GetAudioClip(audio);
        if (clip == null) return;

        audioSourceBGM.clip = clip;
        audioSourceBGM.Play();
    }

    public void PlaySFX(AudioTypeList audio)
    {
        if (isMute) return;

        AudioClip clip = GetAudioClip(audio);
        if (clip == null) return;

        audioSourceSFX.PlayOneShot(clip);
    }
}

[Serializable]
public class AudioType
{
    public AudioTypeList audioType;
    public AudioClip audioClip;
}

public enum AudioTypeList
{
    BackGroundMusic,
    MenuButtonClick_Locked,
    MenuButtonClick_Unlocked,
    MenuButtonClick_NextLevel_Restart,
    MenuButtonClick_MainMenu_Back,
    playerFootstep,
    PlayerJump,
    PlayerHurt,
    PlayerDeath,
    EnemyFootstep,
    KeyPickup,
    LevelComplete
}