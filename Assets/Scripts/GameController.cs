﻿using System;
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
            case 1:
            case 2:
            case 3:
            case 4:
            case 5:
                var array = Steps.ToArray();
                Steps.Add(new Step(index, "Ulazak u laboratorij", false, false, "Trebaš ući u laboratorij!",
                    "Dobrodošli! U edukacijskom načinu naučit ćete proces izrade antibiograma. Za početak, uđimo u laboratorij."));
                ++index;
                Steps.Add(new Step(index, "Pranje ruku ulaz", false, false, "Potraži slavinu i operi ruke.",
                    "Prvo što trebamo napraviti je oprati ruke."));
                ++index;

                Steps.Add(new Step(index, "Stavljanje rukavica", false, false, "Pronađi rukavice i stavi ih.",
                    "Zatim, stavljamo rukavice."));
                ++index;

                Steps.Add(new Step(index, "Uzimanje ušice", false, false, "Uzmi metalnu ušicu.",
                    "Onda uzimamo metalnu ušicu, tj. ezu..."));
                ++index;

                Steps.Add(new Step(index, "Sterilizacija ušice", false, false,
                    "Koristi plamenik kako bi sterilizirao ušicu.", "...I steriliziramo ju pomoću plamena."));
                ++index;

                // Steps.Add(new Step(index, "Pikanje kulture", false, false,
                //     "Pronađi petrijevu zdjelicu s kulturom i pikaj kulturu.",
                //     "Nakon toga, spremni smo za pikanje kulture. Pronađi petrijevu zdjelicu i pikaj kulturu."));
                // ++index;
                //
                // Steps.Add(new Step(index, "Sterilizacija vrha epruvete", false, false, "Koristi epruvetu s plamenikom.",
                //     "Nakon pikanja, uzimamo epruvetu i steriliziramo ju pomoću plamena."));
                // ++index;
                //
                // Steps.Add(new Step(index, "Stavljanje kulture u epruvetu i miješanje", false, false,
                //     "Ušicu stavi u epruvetu i promiješaj.",
                //     "Onda pikanu kulturu stavljamo u epruvetu i miješamo."));
                // ++index;
                //
                // Steps.Add(new Step(index, "Uzimanje petrijeve zdjelice", false, false, "Uzmi petrijevu zdjelicu.",
                //     "Sada, uzimamo petrijevu zdjelicu..."));
                // ++index;
                //
                // Steps.Add(new Step(index, "Šaranje podloge na petrijevoj zdjelici", false, false,
                //     "Šaraj po podlozi sa ušicom.",
                //     "I šaramo po podlozi. Potrebno je išarati podlogu, okrenuti zdjelicu za 60° i ponovno šarati, te još jednom za 60° i šarati."));
                // ++index;
                //
                // Steps.Add(new Step(index, "Zatvaranje zdjelice", false, false, "Zatvori zdjelicu.",
                //     "Zatvaramo zdjelicu."));
                // ++index;
                //
                // Steps.Add(new Step(index, "Crtanje sektora po zdjelici", false, false, "Nacrtaj sektore po zdjelici.",
                //     "Crtamo sektore po zdjelici! Moramo se pobrinuti da ima dovoljno mjesta za antibiotike i njihovo djeleovanje, stoga nacrtajmo 4 sektora."));
                // ++index;
                //
                // Steps.Add(new Step(index, "Uzimanje i sterilizacija pincete", false, false,
                //     "Uzmi pincetu i steriliziraj ju na vatri.",
                //     "Uzmimo pincetu i sterilizirajmo ju na vatri."));
                // ++index;
                //
                // Steps.Add(new Step(index, "Uzimanje antibiotika pincetom i postavljanje na sektore", false, false,
                //     "Uzmi antibiotik pomoću pincete i postavi ih u zdjelicu.",
                //     "Pincetom ćemo uzeti antibiotik i postaviti ga u centar sektora na zdjelici."));
                // ++index;
                //
                // Steps.Add(new Step(index, "Stavljanje zdjelice u inkubator", false, false,
                //     "Stavi zdjelicu u inkubator.",
                //     "Zatim zdjelicu zatvaramo i stavljamo ju u inkubator na 37.5 stupnjeva."));
                // ++index;
                //
                // Steps.Add(new Step(index, "Vađenje zdjelice iz inkubatora", false, false,
                //     "Izvadi zdjelicu iz inkubatora.",
                //     "Ostavili smo zdjelicu preko noći i sada ju možemo izvaditi te vidjeti naše rezultate."));
                // ++index;
                //
                // Steps.Add(new Step(index, "Postavljanje zdjelice na stol i mjerenje zona inhibicije", false, false,
                //     "Stavi zdjelicu na stol i uzmi ravnalo kako bi izmjerio zone inhibicije.",
                //     "Stavljamo zdjelicu na stol i mjerimo zone inhibicije pomoću ravnala."));
                // ++index;
                //
                // Steps.Add(new Step(index, "Interpretacija antibiograma", false, false, "Protumači antibiogram.",
                //     "Vrijeme je za tumačenje naših rezultata. Bla bla bla bla."));
                // ++index;

                Steps.Add(new Step(index, "Odlaganje rukavica u otpad", false, false,
                    "Rukavice trebaš baciti u biološki otpad.",
                    "Sada, naše rukavice trebamo odložiti u biološki otpad."));
                ++index;

                Steps.Add(new Step(index, "Odlaganje eze u otpad", false, false,
                    "Ezu trebaš baciti u biološki otpad.",
                    "I ezu."));
                ++index;

                Steps.Add(new Step(index, "Pranje ruku izlaz", false, false, "Potraži slavinu i operi ruke.",
                    "Još jednom peremo ruke..."));
                ++index;

                Steps.Add(new Step(index, "Izlaz laboratorij", false, false, "Vrati se u svoj ured.",
                    "I gotovi smo s našim procesom! Možemo se vratiti u ured kako bi započeli novi dan."));

                lastStepIndex = Steps.Count() - 1;
                break;
        }
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
            Debug.Log("-- NEW DAY -- DAY:" + level);

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