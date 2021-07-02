using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using DefaultNamespace;

public class SaveAndLoad : MonoBehaviour
{
    public float[] position;
    public int concentration;
    public int stress;
    public int hints;
    public List<Step> steps;
    public int currentLevel;
    public int currentStep;
    public int numberOfMistakes;

    public PlayerStatus playerStatus;
    public GameObject player;
    
    void Update()
    {
        concentration = playerStatus.currentConcentration;
        stress = playerStatus.currentStress;
        hints = playerStatus.numberOfHints;

        position = new float[3];
        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;
        position[2] = player.transform.position.z;

        steps = GameController.Instance.Steps;
        currentLevel = GameController.Instance.level;
        currentStep = GameController.Instance.currentStepIndex;
        numberOfMistakes = GameController.Instance.currentNOfMistakes;      
    }
}
