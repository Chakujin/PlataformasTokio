﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;
using DG.Tweening;

public class MainMenuManager : MonoBehaviour
{
    //Manager
    private GameData m_gameData;

    //UI
    public TextMeshProUGUI titleGame;
    public Color firtsColor;
    public Color secondColor;

    //Options
    public Slider mainVolumeSlider;
    public AudioMixer audioMixer;
    public TMP_Dropdown resolutionDropdown;
    Resolution[] resolutions;

    // Start is called before the first frame update
    void Start()
    {
        titleGame.color = secondColor;
        ColorTitle();
        //Find GameObjects
        m_gameData = GameObject.FindGameObjectWithTag("GameData").GetComponent<GameData>();
        
        //Set Volume
        float MainVolume = m_gameData.MainVolume;
        mainVolumeSlider.value = MainVolume;

        //Set Resolution
        resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
        
    }

    //UI
    private void ColorTitle()
    {
        titleGame.DOColor(firtsColor, 3f);
        titleGame.DOColor(secondColor, 3f).SetDelay(3f).OnComplete(() => ColorTitle());
    }

    //Sound Voids
    public void SetVolume(float sliderValue)
    {
        audioMixer.SetFloat("Master", Mathf.Log10(sliderValue) * 20);
        m_gameData.MainVolume = sliderValue;

        if (sliderValue == 0)
        {
            audioMixer.SetFloat("Master", -60);
        }
    }

    //Resolution Voids
    public void SetResolution(int resolutionIndex)
    {
        FindObjectOfType<AudioSource>().Play();

        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        //FindObjectOfType<AudioSource>().Play();
        Screen.fullScreen = isFullscreen;
    }
}
