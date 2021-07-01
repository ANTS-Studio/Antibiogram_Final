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
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
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
