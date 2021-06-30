using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PauseMenu : MonoBehaviour
{
    public GameObject PlayerCamera;
    public GameObject PauseMenuUI;
    public static bool IsPaused = false;
    public MouseLook MouseLookScript;
    public Button Save, Load, Options, Menu;

    // Start is called before the first frame update
    void Start()
    {
        MouseLookScript = PlayerCamera.GetComponent<MouseLook>();
        PauseMenuUI.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (IsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }
    public void SaveGame()
    {
        SaveAndLoad saveAndLoad = new SaveAndLoad();
        SaveSystem.SavePlayer(saveAndLoad);
        Resume();
        //Mozda da napravim neki feedback
    }
    public void LoadGame()
    {
        PlayerData retrievedData = SaveSystem.LoadPlayer();
        Vector3 position;
        position.x = retrievedData.position[0];
        position.y = retrievedData.position[1];
        position.z = retrievedData.position[2];
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.transform.position = new Vector3(position.x, position.y, position.z);
        Slider concSlider = GameObject.FindGameObjectWithTag("ConcentrationBar").GetComponent<Slider>();
        Slider stressSlider = GameObject.FindGameObjectWithTag("StressBar").GetComponent<Slider>();
        Text textButton = GameObject.FindGameObjectWithTag("HintButton").GetComponentInChildren<Text>();
        if(concSlider.IsActive()) concSlider.value = retrievedData.concentration;
        if(stressSlider.IsActive()) stressSlider.value = retrievedData.stress;
        if(textButton.IsActive()) textButton.text = retrievedData.hints.ToString();
        Resume();
    }
    public void Resume()
    {
        MouseLookScript.enabled = true;
        Cursor.visible = false;
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        IsPaused = false;
    }

    void Pause()
    {
        MouseLookScript.enabled = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        IsPaused = true;
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void Quit()
    {
        if (UnityEditor.EditorApplication.isPlaying == true) {
            UnityEditor.EditorApplication.isPlaying = false;
        } else {
            Application.Quit();
        }
    }
}