using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

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
        //var array = playerStatus.GetPlayerStatusData();
        concentration = playerStatus.currentConcentration;
        stress = playerStatus.currentStress;
        hints = playerStatus.numberOfHints;
        //concentration = array[0];
        //stress = array[1];
        //hints = array[2];

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
