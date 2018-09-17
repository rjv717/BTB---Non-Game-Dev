using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[RequireComponent(typeof(AudioSource))]
public class PersistentAudioPlayer : MonoBehaviour
{

    public static PersistentAudioPlayer ins;
    AudioSource player;
    bool initAudio = false;

    public AudioMixer mixer;
    public AudioClip[] tracks;

    public AudioMixerSnapshot[] fullAudio, muteBGM;
    public float transitionTime = 0.4f;

    private void Awake()
    {
        if (ins == null)
        {
            DontDestroyOnLoad(gameObject);
            ins = this;
            player = GetComponent<AudioSource>();
            player.clip = tracks[0];
            player.Play();
        }
        else if (ins != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if (!initAudio)
        {
            SetVolume(SaveManager.LoadSetting("Music"), SaveManager.LoadSetting("SFX"));
            initAudio = true;
        }
    }

    public void SetVolume(float music, float sfx)
    {
        mixer.SetFloat("BGMVolume", SettingToMixerValue(music));
        mixer.SetFloat("SFXVolume", SettingToMixerValue(sfx));
    }

    public void ChangeMusic(int track)
    {
        StartCoroutine(RunMusicChange(track));
    }

    IEnumerator RunMusicChange(int track)
    {
        if (tracks[track] == player.clip)
        {
            yield break;
        }
        else
        {
            mixer.TransitionToSnapshots(muteBGM, new float[] { 1.0f }, transitionTime);
            yield return new WaitForSeconds(transitionTime);
            player.clip = tracks[track];
            player.Play();
            mixer.TransitionToSnapshots(fullAudio, new float[] { 1.0f }, transitionTime);
        }
    }

    float SettingToMixerValue(float settingValue)
    {
        return (1f - settingValue) * 35f - 35f;
    }
}
