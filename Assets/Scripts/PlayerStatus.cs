using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//Educational
//Dayn - Izrada (Da) - Interpretacija (Da) - Tutorial Mode - Prikaz StoryUI(Ne) - Prikaz TutorialUI(Da) 

//Story
//Day0 - Izrada (Da) - Interpretacija (Da) - Tutorial Mode - Prikaz StoryUI(Ne) - Prikaz TutorialUI(Da) 
//Day1 - Izrada (Da) - Interpretacija (Ne) - Prikaz StoryUI(Da) - Prikaz TutorialUI(Ne) 
//Day2 - Izrada (Ne) - Interpretacija (Da) - Prikaz StoryUI(Ne) - Prikaz TutorialUI(Ne) 
//Day3 - Izrada (Da) - Interpretacija (Ne) - Prikaz StoryUI(Ne) - Prikaz TutorialUI(Ne) 
//Day4 - Izrada (Da) - Interpretacija (Da) - Prikaz StoryUI(Ne) - Prikaz TutorialUI(Ne) 
//Day5 - Izrada (Ne) - Interpretacija (Da) - Prikaz StoryUI(Ne) - Prikaz TutorialUI(Ne) 

public class PlayerStatus : MonoBehaviour
{
    int minConcentration = 0;
    int maxStress = 100;
    float elapsed = 0f;

    public int currentConcentration = 0;
    public int currentStress = 0;
    public int numberOfHints = 0;

    public MouseLook mouseLook;
    public Slider stressSlider, concentrationSlider;
    public GameObject Panel;
    public GameObject HintText;
    public SceneController sceneController;
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

    //Async za zatvaranje panela nakon prikazanog hinta
    IEnumerator waitForHint()
    {
        yield return new WaitForSeconds(5);
        Panel.SetActive(false);
    }

    //Funkcija s logikom za otvaranje panela, dohvatom hinta, prikazom hinta i zatvaranjem
    public void OpenPanel()
    {
        int currentStep = GameController.Instance.GetNextStep();
        string text = GameController.Instance.Steps[currentStep].Hint;
        Panel.SetActive(true);
        HintText.GetComponent<TMPro.TextMeshProUGUI>().text = text;
        StartCoroutine(waitForHint());
    }

    //Funkcija koja postavlja pocetne vrijednosti na pocetku scene
    public void SetDefaultValues()
    {
        int day = GameController.Instance.level;
        bool educational = GameController.Instance.educationalMode;

        if (!educational)
        {
            switch (day)
            {
                //Tutorial Mode
                case 0:
                    currentConcentration = 100;
                    currentStress = 0;
                    numberOfHints = 5;
                    break;
                case 1:
                    currentConcentration = 100;
                    currentStress = 0;
                    numberOfHints = 5;
                    break;
                case 2:
                    currentConcentration = 70;
                    currentStress = 30;
                    numberOfHints = 4;
                    break;
                case 3:
                    currentConcentration = 81;
                    currentStress = 25;
                    numberOfHints = 3;
                    break;
                case 4:
                    currentConcentration = 81;
                    currentStress = 25;
                    numberOfHints = 2;
                    break;
                case 5:
                    currentConcentration = 76;
                    currentStress = 60;
                    numberOfHints = 0;
                    break;
            }
        }

        stressSlider = GameObject.FindGameObjectWithTag("StressBar").GetComponent<Slider>();
        stressSlider.value = currentStress;

        concentrationSlider = GameObject.FindGameObjectWithTag("ConcentrationBar").GetComponent<Slider>();
        concentrationSlider.value = currentConcentration;

    }

    //Funkcija za korekciju vrijednosti
    public void CorrectValuesBasedOnMistakes()
    {
        int mistakes = GameController.Instance.currentNOfMistakes;
        if(mistakes >= 3)
        {
            currentConcentration -= 5;
            currentStress += 5;
            numberOfHints -= 1;
        }
        else if(mistakes == 2)
        {
            currentConcentration -= 5;
            currentStress += 0;
            numberOfHints += 0;
        }
        else if(mistakes == 1)
        {
            currentConcentration += 0;
            currentStress -= 5;
            numberOfHints += 1;
        }
        else
        {
            currentConcentration += 0;
            currentStress -= 5;
            numberOfHints += 2;
        }

    }

    //Iteratori za stres i koncentraciju
    public void changeStressAndConcetration()
    {
        if (currentStress < maxStress)
        {
            currentStress += 1;
            stressSlider.value = currentStress;
        }
        if (currentConcentration > minConcentration)
        {
            currentConcentration -= 1;
            concentrationSlider.value = currentConcentration;
        }
    }

    //vraca sekunde u mouse look
    public float ShakeController()
    {
        float concentration = currentConcentration;
        float stress = currentStress;
        float calculation = 2 * concentration - stress;
        //Debug.Log(calculation);

        if (calculation == 182f) //182
        {
            return 0.3f;
        }
        else if (calculation == 164f)
        {
            return 0.7f;
        }
        else if (calculation == 137f)
        {
            return 1.1f;
        }
        else if (calculation == 116f)
        {
            return 1.4f;
        }
        else if (calculation == 98f)
        {
            return 1.7f;
        }
        else if (calculation == 68f)
        {
            return 1.9f;
        }
        else if (calculation == 41f)
        {
            return 2.1f;
        }
        else if (calculation == 26f)
        {
            return 2.4f;
        }
        else if (calculation == 11f)
        {
            return 3f;
        }
        else if (calculation < 0)
        {
            calculation = 200f;
        }
        return 0f;
    }

    void Start()
    {
        SetDefaultValues();
        if(GameController.Instance.level > 1)
        {
            CorrectValuesBasedOnMistakes();
        }

        //Ako je trenutna scena edukacijska disable UI
        if (GameController.Instance.educationalMode)
        {
            bandh.SetActive(false);
        }
        //Ako je trenutni level = 0 disable Hints, show StoryUI, TutorialScript upravlja prikazom TutorialUI-ja
        else if (GameController.Instance.level == 0)
        {
            Panel.SetActive(false);
        }
        //Inace prikazi StoryUI
        else
        {
            bandh.SetActive(true);
            Panel.SetActive(false);
        }
    }

    void Update()
    {
        if (GameController.Instance.level != 0 || GameController.Instance.educationalMode == true) { 
            elapsed += Time.deltaTime;
            if (elapsed > 8f)
            {
                elapsed = elapsed % 1;
                changeStressAndConcetration();
                ShakeController();
            }
            if (Input.GetKeyDown("h"))
            {
                onKeyPressedRemoveHintAndShow();
                OpenPanel();
            }
        }
    }
}
