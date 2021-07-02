using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class PlayMenu : MonoBehaviour
{
    private PlayableDirector director;
    public GameObject ControlPanel;
    public void PlayGame()
    {
        SceneManager.LoadScene("Day0", LoadSceneMode.Single);
        GameController.Instance.educationalMode = false;
    }

    public void PlayEdu()
    {
        SceneManager.LoadScene("Educational", LoadSceneMode.Single);
        GameController.Instance.educationalMode = true;
    }
    private void Awake()
    {
        director = ControlPanel.GetComponent<PlayableDirector>();
    }

    public void StartTimeline()
    {
        ControlPanel.SetActive(true);
        director.Play();
    }
}
