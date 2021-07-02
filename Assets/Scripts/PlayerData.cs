using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public string steps;
    public float[] position;
    public int concentration;
    public int stress;
    public int hints;
    public int currentScene;
    public int currentStep;
    public int numberOfMistakes;

    public PlayerData () {
        SaveAndLoad saveAndLoad = GameObject.FindGameObjectWithTag("SaveLoad").GetComponent<SaveAndLoad>();
        concentration = saveAndLoad.concentration;
        stress = saveAndLoad.stress;
        hints = saveAndLoad.hints;
        position = new float[3];
        position[0] = saveAndLoad.position[0];
        position[1] = saveAndLoad.position[1];
        position[2] = saveAndLoad.position[2];
        currentScene = saveAndLoad.currentLevel;
        currentStep = saveAndLoad.currentStep;
        numberOfMistakes = saveAndLoad.numberOfMistakes;
        //steps = saveAndLoad.listKoraka;
        //nesto = saveAndLoad.step;

    }
}

