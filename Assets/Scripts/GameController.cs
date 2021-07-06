using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using Debug = UnityEngine.Debug;

public class GameController : MonoBehaviour
{
    public int level;
    [HideInInspector] public bool educationalMode;
    public List<Step> Steps = new List<Step>();
    public int lastStepIndex;
    public int currentStepIndex;
    public int currentNOfMistakes;
    public int totalNOfMistakes;
    [HideInInspector] public bool EndDayTrigger;
    private PlayableDirector director;
    [HideInInspector] public bool entryFlag = false;

    //služi za pristupanje kontroleru od bilo kuda
    public static GameController Instance { get; private set; }

    private void Update()
    {
        if (director && director.state == PlayState.Paused && !entryFlag)
        {
            entryFlag = true;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

            //inicijalno dodavanje stepova ovisno o levelu
            switch (level)
            {
                case 1:
                    AddSteps(1);
                    break;
                case 2:
                    AddSteps(2);
                    break;
                case 3:
                    AddSteps(3);
                    break;
                case 4:
                    AddSteps(4);
                    break;
                case 5:
                    AddSteps(5);
                    break;
            }
        }
    }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            AddSteps(0);
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    //dodavanje stepova ovisno o danu u igri
    void AddSteps(int level)
    {
        var index = 0;
        //index, name, stepDone, wronglyDone, hintText, tutorialText
        switch (level)
        {
            case 0:
                Steps.Add(new Step(index, "Ulazak u laboratorij", false, false, "You need to enter the laboratory.",
                    "Welcome! We will go through the process of creating and interpreting an antibiogram. For starters, let's enter the laboratory."));
                ++index;
                Steps.Add(new Step(index, "Pranje ruku ulaz", false, false, "Look for a sink and wash your hands.",
                    "First, we need to wash our hands."));
                ++index;

                Steps.Add(new Step(index, "Stavljanje rukavica", false, false, "Find gloves and put them on.",
                    "Then, we put on gloves."));
                ++index;

                Steps.Add(new Step(index, "Uzimanje ušice", false, false, "Take the microstreaker.",
                    "Now, we take the microstreaker."));
                ++index;

                Steps.Add(new Step(index, "Sterilizacija ušice", false, false,
                    "Use the bunsen burner to sterilize it.", "...And we sterilize it using flame."));
                ++index;

                Steps.Add(new Step(index, "Pikanje kulture", false, false,
                    "Find the petri dish, open it and take a sample of the bacterial culture.",
                    "After that, we are ready to take a sample of the bacterial culture. Find the petri dish with the bacterial culture and take a sample of it."));
                ++index;
                Steps.Add(new Step(index, "Sterilizacija epruvete", false, false, "Sterilize the test tube with flame.",
                    "Let's take the test tube and sterilize it with flame."));
                ++index;

                Steps.Add(new Step(index, "Stavljanje kulture u epruvetu i miješanje", false, false,
                    "Put the microstreaker in the test tube and mix.",
                    "We then put our microstreaker in the test tube and mix."));
                ++index;

                Steps.Add(new Step(index, "Šaranje podloge", false, false,
                    "Use the microstreaker to go over the petri dish.",
                    "Let's take the other petri dish and now go over the whole surface of the petri dish using the microstreaker."));
                ++index;

                Steps.Add(new Step(index, "Crtanje sektora po zdjelici", false, false,
                    "Take the marker and draw sectors over the petri dish.",
                    "It's time to draw sectors over the petri dish using a marker. We need to make sure that there is enough space for each antibiotic. Let's do eight."));
                ++index;

                Steps.Add(new Step(index, "Uzimanje i sterilizacija pincete", false, false,
                    "Take the tweezers and sterilize it using flame.",
                    "Then, we take the tweezers and sterilize it using flame."));
                ++index;

                Steps.Add(new Step(index, "Uzimanje antibiotika pincetom i postavljanje na sektore", false, false,
                    "Take the antibiotics and put them on the petri dish.",
                    "Using the tweezers we will take antibiotics and put them in the middle of each sector in the petri dish."));
                ++index;

                Steps.Add(new Step(index, "Stavljanje zdjelice u inkubator", false, false,
                    "Put the petri dish in the incubator.",
                    "We then close the petri dish and put it in the incubator, setting the temperature to 37 degrees Celsius."));
                ++index;    

                Steps.Add(new Step(index, "Vađenje zdjelice iz inkubatora i mjerenje", false, false,
                    "Take the petri dish out of the incubator and measure the inhibition zones for each antibiotic.",
                    "The petri dish is left overnight for the results to be visible and interpreted. We currently have a petri dish available for interpretation, our colleagues left it for us. We need to measure the inhibition zones."));
                ++index;
                
                Steps.Add(new Step(index, "Interpretacija antibiograma", false, false, "Go to the laptop and interpret the antibiogram.",
                    "It's time to interpret our antibiogram. Go to the laptop. Check which range your measurements are in. Susceptible (S) means the antibiotic works, Resistant (R) means the antibiotic is not effective."));
                ++index;

                Steps.Add(new Step(index, "Odlaganje rukavica u otpad", false, false,
                    "You need to put the gloves in the biohazardous waste.",
                    "Almost done. Our gloves need to go into biohazardous waste."));
                ++index;

                Steps.Add(new Step(index, "Pranje ruku izlaz", false, false, "Wash your hands in the sink.",
                    "We wash our hands one more time."));
                ++index;

                Steps.Add(new Step(index, "Izlaz laboratorij", false, false, "Go back to your office.",
                    "And we are done with our process. Congratulations, you've made it!"));
                break;
            case 1:
            case 3:
                Steps.Add(new Step(index, "Ulazak u laboratorij", false, false, "You need to enter the laboratory.",
                    "Welcome! We will go through the process of creating and interpreting an antibiogram. For starters, let's enter the laboratory."));
                ++index;
                Steps.Add(new Step(index, "Pranje ruku ulaz", false, false, "Look for a sink and wash your hands.",
                    "First, we need to wash our hands."));
                ++index;

                Steps.Add(new Step(index, "Stavljanje rukavica", false, false, "Find gloves and put them on.",
                    "Then, we put on gloves."));
                ++index;

                Steps.Add(new Step(index, "Uzimanje ušice", false, false, "Take the microstreaker.",
                    "Now, we take the microstreaker."));
                ++index;

                Steps.Add(new Step(index, "Sterilizacija ušice", false, false,
                    "Use the bunsen burner to sterilize it.", "...And we sterilize it using flame."));
                ++index;

                Steps.Add(new Step(index, "Pikanje kulture", false, false,
                    "Find the petri dish, open it and take a sample of the bacterial culture.",
                    "After that, we are ready to take a sample of the bacterial culture. Find the petri dish with the bacterial culture and take a sample of it."));
                ++index;
                Steps.Add(new Step(index, "Sterilizacija epruvete", false, false, "Sterilize the test tube with flame.",
                    "Let's take the test tube and sterilize it with flame."));
                ++index;

                Steps.Add(new Step(index, "Stavljanje kulture u epruvetu i miješanje", false, false,
                    "Put the microstreaker in the test tube and mix.",
                    "We then put our microstreaker in the test tube and mix."));
                ++index;

                Steps.Add(new Step(index, "Šaranje podloge", false, false,
                    "Use the microstreaker to go over the petri dish.",
                    "Let's take the other petri dish and now go over the whole surface of the petri dish using the microstreaker. Top to bottom, then we turn the petri dish by 60 degrees. We do this two more times."));
                ++index;

                Steps.Add(new Step(index, "Crtanje sektora po zdjelici", false, false,
                    "Take the marker and draw sectors over the petri dish.",
                    "It's time to draw sectors over the petri dish using a marker. We need to make sure that there is enough space for each antibiotic. Let's do eight."));
                ++index;

                Steps.Add(new Step(index, "Uzimanje i sterilizacija pincete", false, false,
                    "Take the tweezers and sterilize it using flame.",
                    "Then, we take the tweezers and sterilize it using flame."));
                ++index;

                Steps.Add(new Step(index, "Uzimanje antibiotika pincetom i postavljanje na sektore", false, false,
                    "Take the antibiotics and put them on the petri dish.",
                    "Using the tweezers we will take antibiotics and put them in the middle of each sector in the petri dish."));
                ++index;

                Steps.Add(new Step(index, "Stavljanje zdjelice u inkubator", false, false,
                    "Put the petri dish in the incubator.",
                    "We then close the petri dish and put it in the incubator, setting the temperature to 37 degrees Celsius."));
                ++index;

                Steps.Add(new Step(index, "Odlaganje rukavica u otpad", false, false,
                    "You need to put the gloves in the biohazardous waste.",
                    "Almost done. Our gloves need to go into biohazardous waste."));
                ++index;

                Steps.Add(new Step(index, "Pranje ruku izlaz", false, false, "Wash your hands in the sink.",
                    "We wash our hands one more time."));
                ++index;

                Steps.Add(new Step(index, "Izlaz laboratorij", false, false, "Go back to your office.",
                    "And we are done with our process. Congratulations, you've made it!"));
                break;
            case 2:
                Steps.Add(new Step(index, "Ulazak u laboratorij", false, false, "You need to enter the laboratory.",
                    "Welcome! We will go through the process of creating and interpreting an antibiogram. For starters, let's enter the laboratory."));
                ++index;
                Steps.Add(new Step(index, "Pranje ruku ulaz", false, false, "Look for a sink and wash your hands.",
                    "First, we need to wash our hands."));
                ++index;

                Steps.Add(new Step(index, "Stavljanje rukavica", false, false, "Find gloves and put them on.",
                    "Then, we put on gloves."));
                ++index;

                Steps.Add(new Step(index, "Vađenje zdjelice iz inkubatora i mjerenje", false, false,
                    "Take the petri dish out of the incubator and measure the inhibition zones for each antibiotic.",
                    "The petri dish is left overnight for the results to be visible and interpreted. We currently have a petri dish available for interpretation, our colleagues left it for us. We need to measure the inhibition zones."));
                ++index;
                
                Steps.Add(new Step(index, "Interpretacija antibiograma", false, false, "Go to the laptop and interpret the antibiogram. Susceptible (S) means the antibiotic works, Resistant (R) means the antibiotic is not effective.",
                    "It's time to interpret our antibiogram. Go to the laptop. Check which range your measurements are in. Susceptible (S) means the antibiotic works, Resistant (R) means the antibiotic is not effective."));
                ++index;

                Steps.Add(new Step(index, "Odlaganje rukavica u otpad", false, false,
                    "You need to put the gloves in the biohazardous waste.",
                    "Almost done. Our gloves need to go into biohazardous waste."));
                ++index;

                Steps.Add(new Step(index, "Pranje ruku izlaz", false, false, "Wash your hands in the sink.",
                    "We wash our hands one more time."));
                ++index;

                Steps.Add(new Step(index, "Izlaz laboratorij", false, false, "Go back to your office.",
                    "And we are done with our process. Congratulations, you've made it!"));
                break;
            case 4:
                Steps.Add(new Step(index, "Ulazak u laboratorij", false, false, "You need to enter the laboratory.",
                    "Welcome! We will go through the process of creating and interpreting an antibiogram. For starters, let's enter the laboratory."));
                ++index;
                Steps.Add(new Step(index, "Pranje ruku ulaz", false, false, "Look for a sink and wash your hands.",
                    "First, we need to wash our hands."));
                ++index;

                Steps.Add(new Step(index, "Stavljanje rukavica", false, false, "Find gloves and put them on.",
                    "Then, we put on gloves."));
                ++index;

                Steps.Add(new Step(index, "Vađenje zdjelice iz inkubatora i mjerenje", false, false,
                    "Take the petri dish out of the incubator and measure the inhibition zones for each antibiotic.",
                    "The petri dish is left overnight for the results to be visible and interpreted. We currently have a petri dish available for interpretation, our colleagues left it for us. We need to measure the inhibition zones."));
                ++index;
                
                Steps.Add(new Step(index, "Interpretacija antibiograma", false, false, "Go to the laptop and interpret the antibiogram.",
                    "It's time to interpret our antibiogram. Go to the laptop. Check which range your measurements are in. Susceptible (S) means the antibiotic works, Resistant (R) means the antibiotic is not effective."));
                ++index;

                Steps.Add(new Step(index, "Uzimanje ušice", false, false, "Take the microstreaker.",
                    "Now, we take the microstreaker."));
                ++index;

                Steps.Add(new Step(index, "Sterilizacija ušice", false, false,
                    "Use the bunsen burner to sterilize it.", "...And we sterilize it using flame."));
                ++index;

                Steps.Add(new Step(index, "Pikanje kulture", false, false,
                    "Find the petri dish, open it and take a sample of the bacterial culture.",
                    "After that, we are ready to take a sample of the bacterial culture. Find the petri dish with the bacterial culture and take a sample of it."));
                ++index;
                Steps.Add(new Step(index, "Sterilizacija epruvete", false, false, "Sterilize the test tube with flame.",
                    "Let's take the test tube and sterilize it with flame."));
                ++index;

                Steps.Add(new Step(index, "Stavljanje kulture u epruvetu i miješanje", false, false,
                    "Put the microstreaker in the test tube and mix.",
                    "We then put our microstreaker in the test tube and mix."));
                ++index;

                Steps.Add(new Step(index, "Šaranje podloge", false, false,
                    "Use the microstreaker to go over the petri dish.",
                    "Let's take the other petri dish and now go over the whole surface of the petri dish using the microstreaker. Top to bottom, then we turn the petri dish by 60 degrees. We do this two more times."));
                ++index;

                Steps.Add(new Step(index, "Crtanje sektora po zdjelici", false, false,
                    "Take the marker and draw sectors over the petri dish.",
                    "It's time to draw sectors over the petri dish using a marker. We need to make sure that there is enough space for each antibiotic. Let's do eight."));
                ++index;

                Steps.Add(new Step(index, "Uzimanje i sterilizacija pincete", false, false,
                    "Take the tweezers and sterilize it using flame.",
                    "Then, we take the tweezers and sterilize it using flame."));
                ++index;

                Steps.Add(new Step(index, "Uzimanje antibiotika pincetom i postavljanje na sektore", false, false,
                    "Take the antibiotics and put them on the petri dish.",
                    "Using the tweezers we will take antibiotics and put them in the middle of each sector in the petri dish."));
                ++index;

                Steps.Add(new Step(index, "Stavljanje zdjelice u inkubator", false, false,
                    "Put the petri dish in the incubator.",
                    "We then close the petri dish and put it in the incubator, setting the temperature to 37 degrees Celsius."));
                ++index;

                Steps.Add(new Step(index, "Odlaganje rukavica u otpad", false, false,
                    "You need to put the gloves in the biohazardous waste.",
                    "Almost done. Our gloves need to go into biohazardous waste."));
                ++index;

                Steps.Add(new Step(index, "Pranje ruku izlaz", false, false, "Wash your hands in the sink.",
                    "We wash our hands one more time."));
                ++index;

                Steps.Add(new Step(index, "Izlaz laboratorij", false, false, "Go back to your office.",
                    "And we are done with our process. Congratulations, you've made it!"));
                break;
        }

        lastStepIndex = Steps.Count() - 1;
    }

    //metoda za resetiranje stepova prije loadanja novog dana
    public void ResetSteps()
    {
        Steps.Clear();
    }


    //funkcija za završavanje dana; ako su svi koraci završeni i ako se igrač vrati u svoj ured onda se izvršava
    public void EndDay()
    {
        if (EndDayTrigger)
        {
            currentNOfMistakes = GetCurrentNOfMistakes();
            CalculateMistakes();
            ++level;
            entryFlag = false;
            ResetSteps();
            GameObject EndResultPanel = GameObject.FindGameObjectWithTag("EndResultPanel");
            EndResultPanel.GetComponentInChildren<EndDayResult>().DisplayEndResults();
            Invoke("LoadLevel", 10.0f);
        }
    }

    void LoadLevel()
    {
        switch (level)
        {
            case 1:
                LoadCutScene("CutSceneDay1");
                break;
            case 2:
                LoadCutScene("CutSceneDay2");
                break;
            case 3:
                LoadCutScene("CutSceneDay3");
                break;
            case 4:
                LoadCutScene("CutSceneDay4");
                break;
            case 5:
                LoadCutScene(Ending() == 1 ? "CutSceneDay5" : "CutSceneDay5B");
                break;
        }
    }


    public int GetStepIndexByName(string StepName)
    {
        var step = Steps.Find(x => x.Name == StepName);
        return step.ID;
    }

    public int GetNextStep()
    {
        var step = Steps.Find(x => x.StepDone == false && x.WronglyDone == false);
        if (step != null)
        {
            currentStepIndex = step.ID;
            return currentStepIndex;
        }

        currentStepIndex = 999;
        return currentStepIndex;
    }

    public void SetStepAsDone(int stepId)
    {
        Steps[stepId].StepDone = true;
        int nextStep = GetNextStep();
        if (stepId != lastStepIndex) Debug.Log("Next: " + Steps[nextStep].Name);
    }

    //ukoliko prethodni koraci nisu izvršeni, tj igrač ih je zaboravio, onda su to pogreške i to će se odražavati na ishod
    public void CheckIfPreviousStepsDone(int thisStepId)
    {
        int previousStepIndex = thisStepId - 1;
        if (!Steps[previousStepIndex].StepDone)
        {
            Steps.ForEach(x =>
            {
                if (x.ID <= previousStepIndex && !x.StepDone)
                {
                    x.WronglyDone = true;
                    x.StepDone = true;
                }
            });

            //posebni slucajevi
            int stavljanjeR = GetStepIndexByName("Stavljanje rukavica");
            int odlaganjeR = GetStepIndexByName("Odlaganje rukavica u otpad");
            if (Steps[stavljanjeR].WronglyDone)
            {
                Steps[odlaganjeR].WronglyDone = true;
                Steps[odlaganjeR].StepDone = true;
            }

            //za ezu...
        }
    }

    //broj pogrešaka za taj dan
    public int GetCurrentNOfMistakes()
    {
        return Steps.Count(x => x.WronglyDone);
    }

    //ukupne pogreške u igri
    public int CalculateMistakes()
    {
        totalNOfMistakes += currentNOfMistakes;
        return totalNOfMistakes;
    }

    //funkcija za ishod igre ovisno o ukupnom broju pogrešaka
    public int Ending()
    {
        if (totalNOfMistakes < 9)
        {
            return 1; //good ending cutscene
        }

        return 0; //bad ending cutscene
    }

    public void LoadCutScene(string cutsceneTag)
    {
        GameObject cutScene = GameObject.FindGameObjectWithTag(cutsceneTag);
        cutScene.GetComponent<Canvas>().enabled = true;
        director = cutScene.GetComponent<PlayableDirector>();
        cutScene.SetActive(true);
        director.Play();
    }
}