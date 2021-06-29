using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveAndLoad : MonoBehaviour
{
    public float[] position;
    public int concentration;
    public int stress;
    public int hints;

    public int currentLevel;
    public int currentStep;
    public int numberOfMistakes;

    public PlayerStatus playerStatus;
    public GameObject player;
    public GameController gameController;


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

        currentLevel = gameController.level;
        currentStep = gameController.currentStepIndex;
        numberOfMistakes = gameController.currentNOfMistakes;      
    }
}
