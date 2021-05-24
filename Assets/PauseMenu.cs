using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject PlayerCamera;
    public GameObject PauseMenuUI;
    public static bool IsPaused = false;
    public bool MouseLookScript;

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
                MouseLookScript = true;
                Resume();
            }
            else
            {
                MouseLookScript = false;
                Pause();
            }
        }
    }

    void Resume()
    {
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        IsPaused = false;
    }

    void Pause()
    {
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        IsPaused = true;
    }
}