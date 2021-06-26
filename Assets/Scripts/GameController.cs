using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class GameController : ScriptableObject
{
    private int level;
    private bool educationalMode = true;

    private List<Step> steps = new List<Step>
    {
        new Step(0, "Ulazak u prostoriju", 1, 0, true, false),
        new Step(1, "Pranje ruku", 2, 0, false, false),
        new Step(2, "Stavljanje rukavica", 3, 1, false, false),
        new Step(3, "Uzimanje inventara", 4, 2, false, false),
        new Step(4, "Postavljanje inventara", 5, 3, false, false),
        new Step(5, "Uzimanje inventara", 6, 4, false, false),
        new Step(6, "Uzimanje ušice", 7, 5, false, false),
        new Step(7, "Sterilizacija ušice vatrom", 8, 6, false, false),
        new Step(8, "Pikanje kulture", 9, 7, false, false),
        new Step(9, "Sterilizacija vrha epruvete", 10, 8, false, false),
        new Step(10, "Stavljanje kulture u epruvetu", 11, 9, false, false),
        new Step(11, "Miješanje kulture u epruveti", 12, 10, false, false),
        new Step(12, "Uzimanje petrijeve zdjelice", 13, 11, false, false),
        new Step(13, "Šaranje podloge na petrijevoj zdjelici", 14, 12, false, false),
        new Step(14, "Zatvaranje zdjelice", 15, 13, false, false),
        new Step(15, "Uzimanje markera za crtanje", 16, 14, false, false),
        new Step(16, "Crtanje sektora po zdjelici", 17, 15, false, false),
        new Step(17, "Pravilno postavljanje zdjelice na radnu podlogu", 18, 16, false, false),
        new Step(18, "Uzimanje pincete", 19, 17, false, false),
        new Step(19, "Dezinfekcija pincete", 20, 18, false, false),
        new Step(20, "Uzimanje antibiotika pincetom i postavljanje na sektore", 19, 20, false, false),
        new Step(21, "Vraćanje pincete", 22, 20, false, false),
        new Step(22, "Stavljanje zdjelice u inkubator", 23, 21, false, false)
        //...
    };

    public List<Step> Steps
    {
        get => steps;
        set => steps = value;
    }

    public GameController()
    {
    }

    void Start()
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

    void Update()
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