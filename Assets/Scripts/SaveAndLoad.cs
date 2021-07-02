using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DefaultNamespace;

public class SaveAndLoad : MonoBehaviour
{
    public float[] position;
    public int concentration;
    public int stress;
    public int hints;
    //public List<Step> koraci = new List<Step>();
    public string listaKoraka;

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
        //step = koraci.ToArray();
        //koraci = GameController.Instance.Steps;
        //listaKoraka = koraci.//.ToString();
        currentLevel = GameController.Instance.level;
        currentStep = GameController.Instance.currentStepIndex;
        numberOfMistakes = GameController.Instance.currentNOfMistakes;      
    }
}
