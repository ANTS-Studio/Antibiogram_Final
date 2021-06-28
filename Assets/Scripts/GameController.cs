using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public int level;
    public bool educationalMode;
    public List<Step> Steps = new List<Step>();
    public int lastStepIndex;
    public int currentStepIndex;
    public int nOfMistakes;
    private bool _doorTrigger;

    public bool DoorTrigger
    {
        get => _doorTrigger;
        set
        {
            _doorTrigger = value;
            if(_doorTrigger) EndDay();
        }
    }

    public static GameController Instance { get; private set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            AddSteps();
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
    }

    void AddSteps()
    {
        //stepovi ovisno o tome koji je level...
        var index = 0;
        Steps.Add(new Step(index, "Ulazak u laboratorij", false, false, "Trebaš ući u laboratorij!",
            "Dobrodošli! U edukacijskom načinu naučit ćete proces izrade antibiograma."));
        ++index;
        Steps.Add(new Step(index, "Pranje ruku", false, false, "Potraži slavinu i operi ruke.",
            "Prvo što trebamo napraviti je oprati ruke."));
        ++index;

        Steps.Add(new Step(index, "Stavljanje rukavica", false, false, "Pronađi rukavice i stavi ih.",
            "Zatim, stavljamo rukavice."));
        ++index;

        Steps.Add(new Step(index, "Uzimanje ušice", false, false, "Uzmi metalnu ušicu.",
            "Onda uzimamo metalnu ušicu, tj. ezu..."));
        ++index;

        Steps.Add(new Step(index, "Sterilizacija ušice vatrom", false, false,
            "Koristi plamenik kako bi sterilizirao ušicu.", "I steriliziramo ju pomoću plamena."));
        ++index;

        Steps.Add(new Step(index, "Pikanje kulture", false, false,
            "Pronađi petrijevu zdjelicu s kulturom i pikaj kulturu.",
            "Nakon toga, spremni smo za pikanje kulture. Pronađi petrijevu zdjelicu i pikaj kulturu."));
        ++index;

        Steps.Add(new Step(index, "Sterilizacija vrha epruvete", false, false, "Koristi epruvetu s plamenikom.",
            "Nakon pikanja, uzimamo epruvetu i steriliziramo ju pomoću plamena."));
        ++index;

        Steps.Add(new Step(index, "Stavljanje kulture u epruvetu i miješanje", false, false,
            "Ušicu stavi u epruvetu i promiješaj.",
            "Onda pikanu kulturu stavljamo u epruvetu i miješamo."));
        ++index;

        Steps.Add(new Step(index, "Uzimanje petrijeve zdjelice", false, false, "Uzmi petrijevu zdjelicu.",
            "Sada, uzimamo petrijevu zdjelicu..."));
        ++index;

        Steps.Add(new Step(index, "Šaranje podloge na petrijevoj zdjelici", false, false,
            "Šaraj po podlozi sa ušicom.",
            "I šaramo po podlozi. Potrebno je išarati podlogu, okrenuti zdjelicu za 60° i ponovno šarati, te još jednom za 60° i šarati."));
        ++index;

        Steps.Add(new Step(index, "Zatvaranje zdjelice", false, false, "Zatvori zdjelicu.",
            "Zatvaramo zdjelicu."));
        ++index;

        Steps.Add(new Step(index, "Crtanje sektora po zdjelici", false, false, "Nacrtaj sektore po zdjelici.",
            "Crtamo sektore po zdjelici! Moramo se pobrinuti da ima dovoljno mjesta za antibiotike i njihovo djeleovanje, stoga nacrtajmo 4 sektora."));
        ++index;

        Steps.Add(new Step(index, "Uzimanje i sterilizacija pincete", false, false,
            "Uzmi pincetu i steriliziraj ju na vatri.",
            "Uzmimo pincetu i sterilizirajmo ju na vatri."));
        ++index;

        Steps.Add(new Step(index, "Uzimanje antibiotika pincetom i postavljanje na sektore", false, false,
            "Uzmi antibiotik pomoću pincete i postavi ih u zdjelicu.",
            "Pincetom ćemo uzeti antibiotik i postaviti ga u centar sektora na zdjelici."));
        ++index;

        Steps.Add(new Step(index, "Stavljanje zdjelice u inkubator", false, false,
            "Stavi zdjelicu u inkubator.",
            "Zatim zdjelicu zatvaramo i stavljamo ju u inkubator na 37.5 stupnjeva."));
        ++index;

        Steps.Add(new Step(index, "Vađenje zdjelice iz inkubatora", false, false,
            "Izvadi zdjelicu iz inkubatora.",
            "Ostavili smo zdjelicu preko noći i sada ju možemo izvaditi te vidjeti naše rezultate."));
        ++index;

        Steps.Add(new Step(index, "Postavljanje zdjelice na stol i mjerenje zona inhibicije", false, false,
            "Stavi zdjelicu na stol i uzmi ravnalo kako bi izmjerio zone inhibicije.",
            "Stavljamo zdjelicu na stol i mjerimo zone inhibicije pomoću ravnala."));
        ++index;

        Steps.Add(new Step(index, "Interpretacija antibiograma", false, false, "Protumači antibiogram.",
            "Vrijeme je za tumačenje naših rezultata. Bla bla bla bla."));
        ++index;

        Steps.Add(new Step(index, "Odlaganje rukavica u otpad", false, false,
            "Rukavice trebaš baciti u biološki otpad.",
            "Sada, naše rukavice trebamo odložiti u biološki otpad."));
        ++index;

        Steps.Add(new Step(index, "Pranje ruku", false, false, "Potraži slavinu i operi ruke.",
            "Još jednom peremo ruke..."));
        ++index;

        Steps.Add(new Step(index, "Izlazak iz laboratorija", false, false, "Vrati se u svoj ured.",
            "I gotovi smo s našim procesom!"));

        lastStepIndex = Steps.Count() - 1;
    }

    public void ResetSteps()
    {
        Steps.ForEach(x => x.StepDone = false);
        Steps.ForEach(x => x.WronglyDone = false);
    }

    public void EndDay()
    {
        if ((lastStepIndex == currentStepIndex) && _doorTrigger && currentStepIndex != 0)
        {
            nOfMistakes = GetNOfMistakes();
            ResetSteps();
            Debug.Log("-- NEW DAY --");
            //start cutscene
        }
    }
    
    public int GetCurrentStep()
    {
        var step = Steps.FindLast(x => x.StepDone == true);
        if (step != null) currentStepIndex = step.ID;
        return currentStepIndex;
    }

    public int GetNextStep()
    {
        var lastStepId = GetCurrentStep();
        return ++lastStepId;
    }

    public bool CheckIfPreviousStepDone()
    {
        if (!Steps[GetCurrentStep() - 1].StepDone)
        {
            Steps[GetCurrentStep() - 1].WronglyDone = true;
            return false;
        }
        else return true;
    }

    public int GetNOfMistakes()
    {
        return Steps.Count(x => x.WronglyDone == true);
    }

    void LogicByLevels()
    {
        if (!educationalMode)
        {
            switch (level)
            {
                case 0:
                    break;
                case 1:
                    break;
                case 2:
                    break;
                case 3:
                    break;
                case 4:
                    break;
                case 5:
                    break;
            }
        }
        else
        {
        }
    }
}