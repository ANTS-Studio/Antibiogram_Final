using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//TODO
//1. Dohvat broja pogresaka
//2. Funkcija za dohvata levela
//3. 
//0. U edukacijskom modeu i Day0 nema hintova!!! Treba promijeniti logiku
//0.1 Izmijeniti defaultne vrijednosti
//0.2 Dodati funkciju koja provjerava je li edu ili day0
//0.3 Dodati funkciju koja sprjecava otvaranje panela


public class PlayerStatus : MonoBehaviour
{
    int minConcentration = 0;
    int maxStress = 100;
    public int currentConcentration = 0;
    public int currentStress = 0;
    public int numberOfHints = 0;

    public Slider stressSlider, concentrationSlider;
    public GameObject Panel;
    public GameObject HintText;
    public SceneController sceneController;
    public GameController gameController;
    public GameObject bandh;

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
            setButtonText(parsedText.ToString());
            numberOfHints = parsedText;
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

    //Async za zatvaranje panela nakon prikazanog hinta
    IEnumerator waitForHint()
    {
        yield return new WaitForSeconds(5);
        Panel.SetActive(false);
    }

    //Funkcija s logikom za otvaranje panela, dohvatom hinta, prikazom hinta i zatvaranjem
    public void OpenPanel()
    {
        int currentStep = gameController.GetNextStep();
        string text = gameController.Steps[currentStep].Hint;
        Panel.SetActive(true);
        HintText.GetComponent<TMPro.TextMeshProUGUI>().text = text;
        StartCoroutine(waitForHint());
    }

    //Funkcija koja postavlja pocetne vrijednosti na pocetku scene
    public void setDefaultValues()
    {
        string scene = sceneController.GetCurrentScene();

        switch(scene)
        {
            case "Educational":
                //Jos cemo vidjeti
                break;
            case "Day0":
                currentConcentration = 100;
                currentStress = 0;
                numberOfHints = 5;
                break;
            case "Day1":
                currentConcentration = 95;
                currentStress = 3;
                numberOfHints = 5;
                break;
            case "Day2":
                currentConcentration = 90;
                currentStress = 6;
                numberOfHints = 4;
                break;
            case "Day3":
                currentConcentration = 85;
                currentStress = 9;
                numberOfHints = 4;
                break;
            case "Day4":
                currentConcentration = 80;
                currentStress = 12;
                numberOfHints = 3;
                break;
            case "Day5":
                currentConcentration = 75;
                currentStress = 15;
                numberOfHints = 3;
                break;
            case "Day6":
                currentConcentration = 100;
                currentStress = 0;
                numberOfHints = 5;
                break;
        }

        ////Dohvati broj pogresaka iz GameControllera - integer
        //int numberOfMistakes = 0; //=function call
        
        //if(numberOfMistakes == 2)
        //{
        //    currentConcentration -= 10;
        //    currentStress += 10;
        //    numberOfHints -= 1;
        //}   
        //else if(numberOfMistakes == 1)
        //{
        //    currentConcentration -= 5;
        //    currentStress += 5;
        //}
        //else
        //{
        //    if(currentConcentration <= 90) currentConcentration += 10;
        //    if (currentStress >= 3) currentStress -= 3;
        //    numberOfHints += 2;
        //}

        stressSlider = GameObject.FindGameObjectWithTag("StressBar").GetComponent<Slider>();
        stressSlider.value = currentStress;

        concentrationSlider = GameObject.FindGameObjectWithTag("ConcentrationBar").GetComponent<Slider>();
        concentrationSlider.value = currentConcentration;

    }

    void Start()
    {


        //Ako je trenutna scena edukacijska disable UI
        if(sceneController.IsCurrentSceneEducational())
        {
            bandh.SetActive(false);
        }
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
        //Ako nije nulti dan, omoguci hintove
        if (gameController.level != 0) { 
            if (Input.GetKeyDown("h"))
            {
                onKeyPressedRemoveHintAndShow();
                OpenPanel();
            }
        }
    }
}
