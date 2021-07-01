using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Day0Load : MonoBehaviour
{
    private void OnEnable()
    {
        SceneManager.LoadScene("Day0", LoadSceneMode.Single);
    }
}
