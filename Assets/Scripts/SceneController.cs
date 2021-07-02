using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneController : MonoBehaviour
{
    public Scene scene;

    void Start()
    {
        scene = SceneManager.GetActiveScene();
        Debug.Log("Active Scene is " + GetCurrentScene());
        Debug.Log(IsCurrentSceneEducational());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public string GetCurrentScene()
    {
        scene = SceneManager.GetActiveScene();
        return scene.name;
    }

    public bool IsCurrentSceneEducational()
    {
        string sceneName = GetCurrentScene();
        if(sceneName == "Educational")
        {
            //Debug.Log("Trenutna scena je edukacijska");
            return true;
        }
        else
        {
            //Debug.Log("Trenutna scena nije edukacijska");
            return false;
        }
    }
}

