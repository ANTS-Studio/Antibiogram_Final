using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject PlayerCamera;
    public GameObject PauseMenuUI;
    public static bool IsPaused = false;
    public MouseLook MouseLookScript;

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
        SceneManager.LoadScene("Menu_Scene");
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