using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatus : MonoBehaviour
{
    int minConcentration = 0;
    int currentConcentration = 100;
    int maxStress = 100;
    int currentStress = 0;
    int numberOfHints = 5;
    public Slider stressSlider, concentrationSlider;
    public GameObject Panel;
    public GameObject HintText;
    //Dohvati broj iz Hinta
    private string getButtonText()
    {
        string buttonText = GameObject.FindGameObjectWithTag("HintButton").GetComponentInChildren<Text>().text;
        return buttonText;
    }
    //Postavi broj u Hint
    private void setButtonText(string temporaryText)
    {
        GameObject.FindGameObjectWithTag("HintButton").GetComponentInChildren<Text>().text = temporaryText.ToString();
    }
    //Funkcija s logikom za hintove
    public void onKeyPressedRemoveHintAndShow()
    {
        int parsedText = int.Parse(getButtonText());
        if (parsedText > 0)
        {
            parsedText -= 1;
            Debug.Log(parsedText);
            setButtonText(parsedText.ToString());
        }
        else
        {
            //Vrati poruku da nema vise hintova
        }
    }
    //Iteratori za stres i koncentraciju
    public void changeStressAndConcetration()
    {
        if (currentStress < maxStress) { 
            currentStress += 1;
            stressSlider.value = currentStress;
        }
        if (currentConcentration > minConcentration) {
            currentConcentration -= 1;
            concentrationSlider.value = currentConcentration;
        }
    }
    //Funkcija koja postavlja pocetne vrijednosti na pocetku scene
    public void setDefaultValues()
    {
        //Panel = GameObject.FindGameObjectWithTag("HintPanel");//.GetComponent<GameObject>();
        //Tu ce ici funkcija koja dohvaca scene (Day 0, Day 1, etc.) i shodno tome postavlja vrijednosti jer svaki dan ima svoj default value
        currentStress = 0;
        stressSlider = GameObject.FindGameObjectWithTag("StressBar").GetComponent<Slider>();
        stressSlider.value = currentStress;

        currentConcentration = 100;
        concentrationSlider = GameObject.FindGameObjectWithTag("ConcentrationBar").GetComponent<Slider>();
        concentrationSlider.value = currentConcentration;

        numberOfHints = 5;

    }
    IEnumerator waitForHint()
    {
        yield return new WaitForSeconds(5);
        Panel.SetActive(false);
    }
    //GameOBject.FindGameObjectWithTag("HintPanel").GetComponentInChildren<Text>().text = temporaryText.ToString();
    public void OpenPanel()
    {
        Panel.SetActive(true);
        string temporaryText = "Hint koji proizlazi iz nekog koraka u procesu.";
        HintText.GetComponent<TMPro.TextMeshProUGUI>().text = temporaryText;
        StartCoroutine(waitForHint());
    }

    //public void ClosePanel()
    //{
    //    if (Panel.activeSelf)
    //    {
    //        Panel.SetActive(false);
    //    }
    //}

    void Start()
    {
        //Panel = GameObject.FindGameObjectWithTag("HintPanel");
        Panel.SetActive(false);

        setDefaultValues();
    }

    void Update()
    {
        float elapsed = 0f;

        elapsed += Time.deltaTime;
        if (elapsed >= 10f)
        {
            elapsed = elapsed % 1;
            changeStressAndConcetration();
        }

        if (Input.GetKeyDown("h"))
        {
            onKeyPressedRemoveHintAndShow();
            OpenPanel();
        }
    }
}

//TODO
//1. Funkcija koja dohvaca scene i postavlja defaultne vrijednosti koncentracije, stresa i hintova.
//2.
//3.

//public void getCurrentScene()
//{
//if day 0
//elif day 1
//elif day 2
//elif day 3
//elif day 4
//elif day 5
//elif day 6
//}