using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class GameController : ScriptableObject
{
    private int _level;
    private bool _educationalMode = true;

    public int Level
    {
        get => _level;
        set => _level = value;
    }

    public bool EducationalMode
    {
        get => _educationalMode;
        set => _educationalMode = value;
    }

    private List<Step> steps = new List<Step>
    {
        new Step(0, "Ulazak u prostoriju", true, false, "Trebaš ući u laboratorij!"),
        new Step(1, "Pranje ruku", false, false, "Potraži slavinu."),
        new Step(2, "Stavljanje rukavica", false, false, "Pronađi rukavice i stavi ih."),
        new Step(3, "Uzimanje inventara", false, false, ""),
        new Step(4, "Postavljanje inventara", false, false, ""),
        new Step(5, "Uzimanje inventara", false, false, ""),
        new Step(6, "Uzimanje ušice", false, false, ""),
        new Step(7, "Sterilizacija ušice vatrom", false, false, ""),
        new Step(8, "Pikanje kulture", false, false, ""),
        new Step(9, "Sterilizacija vrha epruvete", false, false, ""),
        new Step(10, "Stavljanje kulture u epruvetu", false, false, ""),
        new Step(11, "Miješanje kulture u epruveti", false, false, ""),
        new Step(12, "Uzimanje petrijeve zdjelice", false, false, ""),
        new Step(13, "Šaranje podloge na petrijevoj zdjelici", false, false, ""),
        new Step(14, "Zatvaranje zdjelice", false, false, ""),
        new Step(15, "Uzimanje markera za crtanje", false, false, ""),
        new Step(16, "Crtanje sektora po zdjelici", false, false, ""),
        new Step(17, "Pravilno postavljanje zdjelice na radnu podlogu", false, false, ""),
        new Step(18, "Uzimanje pincete", false, false, ""),
        new Step(19, "Dezinfekcija pincete", false, false, ""),
        new Step(20, "Uzimanje antibiotika pincetom i postavljanje na sektore", false, false, ""),
        new Step(21, "Vraćanje pincete", false, false, ""),
        new Step(22, "Stavljanje zdjelice u inkubator", false, false, "")
        //...
    };

    public int GetLastStep()
    {
        Step step = steps.FindLast(x => x.StepDone == true);
        return step != null ? step.ID : 0;
    }

    public int GetNextStep()
    {
        int lastStepId = GetLastStep();
        return ++lastStepId;
    }
    public List<Step> Steps
    {
        get => steps;
        set => steps = value;
    }

    void LogicByLevels()
    {
        if (!_educationalMode)
        {
            switch (_level)
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