using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsScript : MonoBehaviour
{
    public AudioMixer audioMixer;
    public GameObject optionsMenu;
    public Button backButton;
    public SceneController sceneController;
    public GameObject pauseMenu;
    public GameObject eduPauseMenu;

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
        Debug.Log(volume);
    }
    public void SetQuality(int quality)
    {
        QualitySettings.SetQualityLevel(quality);
        Debug.Log(quality);
    }
    public void ToggleFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }

    public void TurnOffMusic(bool isMusicOn)
    {
        if (isMusicOn)
        {
            AudioListener.volume = 1.0f;
        }
        else
        {
            AudioListener.volume = 0.0f;
        }
        
    }

    public void goBack()
    {
        if(sceneController.IsCurrentSceneEducational())
        {
            optionsMenu.SetActive(false);
            pauseMenu.SetActive(false);
            eduPauseMenu.SetActive(true);
        }
        else
        {
            optionsMenu.SetActive(false);
            pauseMenu.SetActive(true);
            eduPauseMenu.SetActive(false);
        }
    }

    void Start()
    {
        optionsMenu.SetActive(false);
        backButton.onClick.AddListener(() => goBack());
    }
}
