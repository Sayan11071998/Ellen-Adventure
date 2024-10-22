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

    // public void Mute(bool status)
    // {
    //     isMute = status;
    //     audioSourceBGM.mute = status;
    //     audioSourceSFX.mute = status;
    //     audioSourcePlayer.mute = status;
    //     audioSourceEnemy.mute = status;
    // }

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

    public void MuteAudioSource(AudioSourceList sourceName, bool value)
    {
        switch (sourceName)
        {
            case AudioSourceList.audioSourcePlayer:
                audioSourcePlayer.mute = value;
                break;

            case AudioSourceList.audioSourceEnemy:
                audioSourceEnemy.mute = value;
                break;

            case AudioSourceList.audioSourceSFX:
                audioSourceSFX.mute = value;
                break;

            case AudioSourceList.audioSourceBGM:
                audioSourceBGM.mute = value;
                break;
        }
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

    public void PlayPlayerWalkAudio(AudioTypeList audio)
    {
        if (isMute) return;
        
        AudioClip clip = GetAudioClip(audio);
        if (clip == null) return;

        audioSourcePlayer.clip = clip;
        audioSourcePlayer.Play();
    }

    public void PlayPlayerJumpAudio(AudioTypeList audio)
    {
        if (isMute) return;
        AudioClip clip = GetAudioClip(audio);
        if (clip == null) return;
        audioSourcePlayer.PlayOneShot(clip);
    }

    public void PlayEnemyFootestepAudio(AudioTypeList audio)
    {
        if (isMute) return;

        AudioClip clip = GetAudioClip(audio);
        if (clip == null) return;

        audioSourceEnemy.clip = clip;
        audioSourceEnemy.Play();
    }

    public void PlayPlayerDeathAudio(AudioTypeList audio)
    {
        if (isMute) return;

        AudioClip clip = GetAudioClip(audio);
        if (clip == null) return;

        audioSourcePlayer.PlayOneShot(clip);
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
    MenuButtonClick_NextLevel,
    MenuButtonClick_Restart,
    MenuButtonClick_MainMenu,
    playerFootstep,
    PlayerJump,
    PlayerDeath,
    EnemyFootstep,
    KeyPickup,
    LevelComplete
}

public enum AudioSourceList
{
    audioSourceSFX,
    audioSourceBGM,
    audioSourcePlayer,
    audioSourceEnemy,
}