using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public int level;
    public bool educationalMode = true;
    public List<Step> steps;
    
    void Start(){
        steps = new List<Step>();  
        steps.Add(new Step(1, "Ulazak u prostoriju", 2, 0, true));
        steps.Add(new Step(2, "Pranje ruku", 3, 1, false));
        steps.Add(new Step(3, "Stavljanje rukavica", 4, 2, false));
        steps.Add(new Step(4, "Uzimanje inventara", 5, 3, false));
        steps.Add(new Step(5, "Postavljanje inventara", 6, 4, false));
        steps.Add(new Step(6, "Uzimanje inventara", 7, 5, false));
        steps.Add(new Step(7, "Uzimanje ušice", 8, 6, false));
        steps.Add(new Step(8, "Sterilizacija ušice vatrom", 9, 7, false));
        steps.Add(new Step(9, "Pikanje kulture", 10, 8, false));
        steps.Add(new Step(10, "Sterilizacija vrha epruvete", 11, 9, false));
        steps.Add(new Step(11, "Stavljanje kulture u epruvetu", 12, 10, false));
        steps.Add(new Step(12, "Miješanje kulture u epruveti", 13, 11, false));
        steps.Add(new Step(13, "Uzimanje petrijeve zdjelice", 14, 12, false));
        steps.Add(new Step(14, "Šaranje podloge na petrijevoj zdjelici", 15, 13, false));
        steps.Add(new Step(15, "Zatvaranje zdjelice", 16, 14, false));
        steps.Add(new Step(16, "Uzimanje markera za crtanje", 17, 15, false));
        steps.Add(new Step(17, "Crtanje sektora po zdjelici", 18, 16, false));
        steps.Add(new Step(18, "Pravilno postavljanje zdjelice na radnu podlogu", 19, 17, false));
        steps.Add(new Step(19, "Uzimanje pincete", 20, 18, false));
        steps.Add(new Step(20, "Dezinfekcija pincete", 21, 19, false));
        steps.Add(new Step(21, "Uzimanje antibiotika pincetom i postavljanje na sektore", 22, 20, false));
        steps.Add(new Step(22, "Vraćanje pincete", 23, 21, false));
        steps.Add(new Step(23, "Stavljanje zdjelice u inkubator", 24, 22, false)); 
        //...
        
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

    void Update(){
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
        } else
        {
            
        }
    }
}

