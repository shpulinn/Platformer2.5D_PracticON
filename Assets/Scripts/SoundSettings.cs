using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundSettings : MonoBehaviour
{
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private Toggle muteToggle;
    
    public bool settingsScreenShown = false;

    private void OnEnable()
    {
        settingsScreenShown = true;
    }

    private void OnDisable()
    {
        settingsScreenShown = false;
    }

    private void Start()
    {
        AudioListener.volume = PlayerPrefs.GetFloat("GlobalVolume_PP", 1f);
        volumeSlider.value = AudioListener.volume;
        AudioListener.pause = PlayerPrefs.GetInt("MuteSounds", 0) != 0;
        muteToggle.isOn = AudioListener.pause;
    }

    public void AllSoundsToggle(bool value)
    {
        AudioListener.pause = value;
        PlayerPrefs.SetInt("MuteSounds", value == true ? 1 : 0);
        PlayerPrefs.Save();
    }

    public void SetGlobalVolume(float volume)
    {
        AudioListener.volume = volume;
        PlayerPrefs.SetFloat("GlobalVolume_PP", volume);
        PlayerPrefs.Save();
    }
}
