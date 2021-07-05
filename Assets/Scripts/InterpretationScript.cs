using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class InterpretationScript : MonoBehaviour
{
    public Button sendButton;
    public GameObject panel;
    public string[] measureNameTags = { "Measure1", "Measure2", "Measure3", "Measure4", "Measure5", "Measure6", "Measure7", "Measure8" };
    public string[] sensitiveNameTags = { "Sensitive1", "Sensitive2", "Sensitive3", "Sensitive4", "Sensitive5", "Sensitive6", "Sensitive7", "Sensitive8" };
    public string[] resistantTags = { "Resistant1", "Resistant2", "Resistant3", "Resistant4", "Resistant5", "Resistant6", "Resistant7", "Resistant8" };
    public string[] toggleTags = { "ToggleGroup1", "ToggleGroup2", "ToggleGroup3", "ToggleGroup4", "ToggleGroup5", "ToggleGroup6", "ToggleGroup7", "ToggleGroup8" };

    public int[] temporaryValues = { 23, 27, 14, 13, 8, 12, 37, 18 };
    public int[] resistantAndSensitiveValues = { 14, 18, 20, 15, 7, 16, 38, 22};

    public int[] collectedValues = new int[8];

    //1. 
    //2. 


    //Nehardkodirani dio
    public void SetMeasurmentValues(int[] inputValues)
    {
        for (int i = 0; i < measureNameTags.Length; i++)
        {
            if(panel.activeSelf) GameObject.FindGameObjectWithTag(measureNameTags[i]).GetComponent<TMPro.TextMeshProUGUI>().text = inputValues[i].ToString();
        }
    }

    //Hardkodirani dio
    public void SetResAndSensValues()
    {
        for (int i = 0; i < resistantAndSensitiveValues.Length; i++)
        {
            GameObject.FindGameObjectWithTag(resistantTags[i]).GetComponent<TMPro.TextMeshProUGUI>().text = resistantAndSensitiveValues[i].ToString();
            GameObject.FindGameObjectWithTag(sensitiveNameTags[i]).GetComponent<TMPro.TextMeshProUGUI>().text = resistantAndSensitiveValues[i].ToString();
        }
    }

    public List<bool> GetUsersInputValues()
    {
        //List<Toggle> outputToggles = new List<Toggle>();
        List<bool> outputToggles = new List<bool>();
        for (int i = 0; i < toggleTags.Length; i++)
        {
            Toggle[] toggles = GameObject.FindGameObjectWithTag(toggleTags[i]).GetComponentsInChildren<Toggle>();
            Debug.Log(toggles);
            for(int j = 0; j < toggles.Length; j++)
            {
                if(toggles[j].isOn)
                {
                    outputToggles.Add(true);

                }
                else
                {
                    outputToggles.Add(false);
                }
            }
        }
        return outputToggles;
    }

    public List<bool> GetInterpretationCorrectness()
    {
        List<int> measurements = new List<int>();
        List<int> resistentList = resistantAndSensitiveValues.OfType<int>().ToList();

        for(int i = 0; i < measureNameTags.Length; i++)
        {
            measurements.Add(int.Parse(GameObject.FindGameObjectWithTag(measureNameTags[i]).GetComponent<TMPro.TextMeshProUGUI>().text));
        }
        List<bool> finalList = new List<bool>();
        for (int i = 0; i < measurements.Count; i++)
        {
            if(measurements[i] > resistentList[i])
            {
                finalList.Add(false);
                finalList.Add(true);
            }
            else
            {
                finalList.Add(true);
                finalList.Add(false);
            }
        }
        return finalList;
    }

    public bool IsInterpretationCorrect()
    {
        SetMeasurmentValues(collectedValues);
        SetResAndSensValues();
        List<bool> userInput = GetUsersInputValues();
        List<bool> correctValues = GetInterpretationCorrectness();
        for(int i = 0; i < userInput.Count; i++)
        {
            if (userInput[i] != correctValues[i])
            {
                panel.SetActive(false);
                return false;
            }
        }
        panel.SetActive(false);
        return true;
    }

    void Start()
    {
        //SetMeasurmentValues(temporaryValues);
        //AddMesuredValue(1);
        SetResAndSensValues();
        panel.SetActive(false);
        sendButton.onClick.AddListener(() => IsInterpretationCorrect());
        //Funkcija SetMeasurementValues(); se zapravo poziva iz druge skripte, dakle, ne smije biti u startu

    }

    public void AddMesuredValue(int newMesurement)
    {
        for(int i = 0; i < 8; i++)
        {
            if (collectedValues[i] != 0) continue;

            collectedValues[i] = newMesurement;
            break;
        }
        SetMeasurmentValues(collectedValues);
    }
}
