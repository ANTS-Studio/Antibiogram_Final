using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndDayResult : MonoBehaviour
{

    private string endDayText;
    private int numberOfMistakes;
    private int numberOfSteps;
    private string concentration;
    private string stress;
    private string hints;
    public Canvas canvas;

    public void DisplayEndResults()
    {
        canvas.enabled = true;
        StartCoroutine(TimerFunction());
    }

    IEnumerator TimerFunction()
    {
        SetText();
        yield return new WaitForSeconds(10);
        canvas.enabled = false;
    }

    public void SetText()
    {
        numberOfMistakes = GameController.Instance.currentNOfMistakes;
        numberOfSteps = GameController.Instance.lastStepIndex + 1;

        int numberOfCorrectAnswers = numberOfSteps - numberOfMistakes;
        endDayText = numberOfCorrectAnswers.ToString() + "/" + numberOfSteps.ToString();
        GameObject.FindGameObjectWithTag("EndResult").GetComponent<TMPro.TextMeshProUGUI>().text = endDayText;

        float percentage = (float)numberOfCorrectAnswers / (float)numberOfSteps * 100;
        int finalPercentage = (int)percentage;
        GameObject.FindGameObjectWithTag("PercentageResult").GetComponent<TMPro.TextMeshProUGUI>().text = finalPercentage.ToString() + "%";

        if(numberOfMistakes >= 3)
        {
            concentration = "- 5";
            stress = "+ 5";
            hints = "- 1";
        }
        else if(numberOfMistakes == 2)
        {
            concentration = "- 5";
            stress = "0";
            hints = "0";
        }
        else if(numberOfMistakes == 1)
        {
            concentration = "0";
            stress = "- 5";
            hints = "+ 1";
        }
        else
        {
            concentration = "0";
            stress = "- 5";
            hints = "+ 2";
        }
        GameObject.FindGameObjectWithTag("ConcentrationResult").GetComponent<TMPro.TextMeshProUGUI>().text = "CONCENTRATION " + concentration; 
        GameObject.FindGameObjectWithTag("StressResult").GetComponent<TMPro.TextMeshProUGUI>().text = "STRESS " + stress;
        GameObject.FindGameObjectWithTag("HintsResult").GetComponent<TMPro.TextMeshProUGUI>().text = "HINTS " + hints;
    }
    private void Start()
    {
        canvas = GameObject.FindGameObjectWithTag("EndDayResults").GetComponent<Canvas>();
        canvas.enabled = false;
    }
}
