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
        new Step(0, "Ulazak u laboratorij", true, false, "Trebaš ući u laboratorij!",
            "Dobrodošli! U edukacijskom načinu naučit ćete proces izrade antibiograma."),
        new Step(1, "Pranje ruku", false, false, "Potraži slavinu i operi ruke.",
            "Prvo što trebamo napraviti je oprati ruke."),
        new Step(2, "Stavljanje rukavica", false, false, "Pronađi rukavice i stavi ih.", "Zatim, stavljamo rukavice."),
        new Step(3, "Uzimanje inventara i postavljanje inventara na stol", false, false,
            "Uzmi sve što ti je potrebno za izradu antibiograma i stavi na stol.",
            "U hladnjaku ćemo pronaći bla bla i to trebamo uzeti i staviti na stol."),
        new Step(4, "Uzimanje ušice", false, false, "Uzmi metalnu ešicu.", "Onda uzimamo metalnu ušicu, tj. ezu..."),
        new Step(5, "Sterilizacija ušice vatrom", false, false, "Koristi plamenik kako bi sterilizirao ušicu.",
            "I steriliziramo ju pomoću plamena."),
        new Step(6, "Pikanje kulture", false, false, "Pronađi petrijevu zdjelicu s kulturom i pikaj kulturu.",
            "Nakon toga, spremni smo za pikanje kulture. Pronađi petrijevu zdjelicu i pikaj kulturu."),
        new Step(7, "Sterilizacija vrha epruvete", false, false, "Koristi epruvetu s plamenikom.",
            "Nakon pikanja, uzimamo epruvetu i steriliziramo ju pomoću plamena."),
        new Step(8, "Stavljanje kulture u epruvetu i miješanje", false, false, "Ušicu stavi u epruvetu i promiješaj.",
            "Onda pikanu kulturu stavljamo u epruvetu i miješamo."),
        new Step(9, "Uzimanje petrijeve zdjelice", false, false, "Uzmi petrijevu zdjelicu.",
            "Sada, uzimamo petrijevu zdjelicu..."),
        new Step(10, "Šaranje podloge na petrijevoj zdjelici", false, false, "Šaraj po podlozi sa ušicom.",
            "I šaramo po podlozi. Potrebno je išarati podlogu, okrenuti zdjelicu za 60° i ponovno šarati, te još jednom za 60° i šarati."),
        new Step(11, "Zatvaranje zdjelice", false, false, "Zatvori zdjelicu.", "Zatvaramo zdjelicu."),
        new Step(12, "Crtanje sektora po zdjelici", false, false, "Nacrtaj sektore po zdjelici.",
            "Crtamo sektore po zdjelici! Moramo se pobrinuti da ima dovoljno mjesta za antibiotike i njihovo djeleovanje, stoga nacrtajmo 4 sektora."),
        new Step(13, "Uzimanje i sterilizacija pincete", false, false, "Uzmi pincetu i steriliziraj ju na vatri.",
            "Uzmimo pincetu i sterilizirajmo ju na vatri."),
        new Step(14, "Uzimanje antibiotika pincetom i postavljanje na sektore", false, false,
            "Uzmi antibiotik pomoću pincete i postavi ih u zdjelicu.",
            "Pincetom ćemo uzeti antibiotik i postaviti ga u centar sektora na zdjelici."),
        new Step(15, "Stavljanje zdjelice u inkubator", false, false, "Stavi zdjelicu u inkubator.",
            "Zatim zdjelicu zatvaramo i stavljamo ju u inkubator na 37.5 stupnjeva."),
        new Step(16, "Vađenje zdjelice iz inkubatora", false, false, "Izvadi zdjelicu iz inkubatora.",
            "Ostavili smo zdjelicu preko noći i sada ju možemo izvaditi te vidjeti naše rezultate."),
        new Step(17, "Postavljanje zdjelice na stol i mjerenje zona inhibicije", false, false,
            "Stavi zdjelicu na stol i uzmi ravnalo kako bi izmjerio zone inhibicije.",
            "Stavljamo zdjelicu na stol i mjerimo zone inhibicije pomoću ravnala."),
        new Step(18, "Interpretacija antibiograma", false, false, "Protumači antibiogram.",
            "Vrijeme je za tumačenje naših rezultata. Bla bla bla bla."),
        new Step(19, "Odlaganje rukavica u otpad", false, false, "Rukavice trebaš baciti u biološki otpad.",
            "Sada, naše rukavice trebamo odložiti u biološki otpad."),
        new Step(20, "Pranje ruku", false, false, "Potraži slavinu i operi ruke.", "Još jednom peremo ruke..."),
        new Step(21, "Izlazak iz laboratorija", false, false, "Vrati se u svoj ured.", "I gotovi smo s našim procesom!")
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