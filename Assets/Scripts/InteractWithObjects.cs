using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InteractWithObjects : MonoBehaviour
{
    public Camera mainCam;
    public LayerMask layerMask;
    public Text interactText;
    public Text secondaryInteractText;
    public Text genericFeedbackText;
    public Text primaryPickupText;
    public GameObject panel;
    public GameObject panel2;
    public GameObject primaryTest;

    public float maxDistance = 70f;

    private RaycastHit hitInfo;
    private ParticleSystem particles;
    private List<string> canDisinfectWithFire;
    private List<string> HoldButtonInteractionObjects;
    private List<string> interactableItems;
    private List<string> secondaryInteractions;
    private bool entryFlag = false;
    private float currentInteractionTimer = 0;
    public Image InteractionProgressImg;

    public float fireInteractionDuration = 4f;
    public float waterInteractionDuration = 4f;

    private float coolDownTimer = 1f;

    // Start is called before the first frame update
    void Start()
    {
        canDisinfectWithFire = new List<string> { "Eza", "Pinceta", "Test tube" };
        HoldButtonInteractionObjects = new List<string> { "Test tube" };

        panel = GameObject.FindGameObjectWithTag("PanelForRaycast");
        panel2 = GameObject.FindGameObjectWithTag("PanelForRaycast2");

        interactableItems = new List<string> { "PetrijevaZdjelicaBakterije", "DrawablePetrieDish", "LabCentrifuga", "TestTubeBase" };
        secondaryInteractions = new List<string> { "PetrijevaZdjelicaBakterije", "DrawablePetrieDish", "Poklopac" };
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = mainCam.ViewportPointToRay(Vector3.one / 2f);
        //Debug.DrawRay(ray.origin, ray.direction * maxDistance, Color.red);
        // Trazi item koji se nalaze na odredenom layeru
        if (Physics.Raycast(ray, out hitInfo, maxDistance, layerMask))
        {

            particles = hitInfo.collider.gameObject.GetComponentInChildren<ParticleSystem>();
            GameObject selectedObject = GetSelectedItem();
            string hitItemName = hitInfo.collider.gameObject.name;

            if (CanInteract(hitItemName))
            {
                if (genericFeedbackText.text.Equals("") && interactText.text.Equals(""))
                {
                    panel.SetActive(false);
                    GenericInteraction(hitInfo.collider.gameObject);
                }
                else
                {
                    panel.SetActive(true);
                    GenericInteraction(hitInfo.collider.gameObject);
                }
            }
            else if (HoldButtonInteractionObjects.Contains(hitItemName))
            { 
                HoldButtonInteraction(hitInfo.collider.gameObject);
            }
            else if (hitItemName == "bunsen_burner" && CanDisinfectFire(selectedObject))
            {
                this.DisinfectWithFire(selectedObject);
                panel.SetActive(true);
            }
            else if (hitItemName == "ID309" && canDisinfectWater())
            {
                this.DisinfectWithWater(selectedObject);
            }
            else
            {
                ToggleParticles(hitInfo.collider.gameObject);
                panel.SetActive(true);
            }
        }
        else
        {
            interactText.text = "";
            secondaryInteractText.text = "";
            panel.SetActive(false);
            panel2.SetActive(false);
        }
    }

    private void HoldButtonInteraction(GameObject hitItem)
    {
        switch (hitItem.name)
        {
            case "Test tube":
                interactText.text = "HOLD E TO STIR"; //Hold 'E' to disinfect item!
                panel.SetActive(true);

                hitItem.gameObject.GetComponent<MetalFlaskScript>().Interaction();
                if(!entryFlag) {
                    int nextStep = GameController.Instance.GetNextStep();
                    int thisStep = GameController.Instance.GetStepIndexByName("Stavljanje kulture u epruvetu i miješanje");
                    if (nextStep != thisStep)
                    {
                        GameController.Instance.CheckIfPreviousStepsDone(thisStep);
                        GameController.Instance.SetStepAsDone(thisStep);
                    }
                    else
                    {
                        GameController.Instance.SetStepAsDone(thisStep);
                    }
                    entryFlag = true;
                
                } 
                break;
        }
    }

    // ########################################################### //

    private void GenericInteraction(GameObject hitItem)
    {
        if (InteractionCooldownActive()) return;

        if (interactableItems.Contains(hitItem.name))
            PrimaryInteraction(hitItem);
        if (secondaryInteractions.Contains(hitItem.name))
            SecondaryInteraction(hitItem);
    }

    private void PrimaryInteraction(GameObject hitItem)
    {
        interactText.text = GetFeedbackMsg(hitItem.name);

        if (Input.GetKey(KeyCode.E))
        {
            coolDownTimer = 0;

            switch (hitItem.name)
            {
                case "LabCentrifuga":
                    hitItem.gameObject.GetComponent<IncubatorScript>().Interaction();
                    panel.SetActive(true);
                    break;
                case "PetrijevaZdjelicaBakterije":
                    hitItem.gameObject.GetComponent<GenerateBacteria>().Interaction();
                    panel.SetActive(true);
                    //primaryTest.SetActive(false);
                    break;
                case "DrawablePetrieDish":
                    hitItem.gameObject.GetComponent<EnableDrawing>().Interaction();
                    panel.SetActive(true);
                    break;
                case "TestTubeBase":
                    hitItem.gameObject.GetComponent<TestTubeBase>().Interaction();
                    break;
                case "Metal flask":
                    hitItem.gameObject.GetComponent<MetalFlaskScript>().Interaction();
                    panel.SetActive(true);
                    break;
            }
        }
    }
   
    private void SecondaryInteraction(GameObject hitItem)
    {
        secondaryInteractText.text = GetSecondaryFeedbackMsg(hitItem.name);

        if (!Input.GetKey(KeyCode.R)) return;

        coolDownTimer = 0;
        secondaryInteractText.text = "";

        switch (hitItem.name)
        {
            case "PetrijevaZdjelicaBakterije":
                hitItem.gameObject.GetComponent<GenerateBacteria>().SecondaryInteraction();
                panel2.SetActive(true);
                break;
            case "DrawablePetrieDish":
                hitItem.gameObject.GetComponent<EnableDrawing>().SecondaryInteraction();
                panel2.SetActive(true);
                break;
            case "Poklopac":
                hitItem.gameObject.GetComponent<PetrieDishLid>().Interaction();
                panel2.SetActive(true);
                break;
        }
    }

    // ########################################################### //

    private bool InteractionCooldownActive()
    {
        if (coolDownTimer > 0.25f) return false;

        coolDownTimer += Time.deltaTime;
        return true;
    }

    private bool CanInteract(string itemName)
    {
        return interactableItems.Contains(itemName) || secondaryInteractions.Contains(itemName);
    }

    private GameObject GetSelectedItem()
    {
        PlayerInventory inventory = gameObject.GetComponent<PlayerInventory>();
        return inventory.getSelectedItem();
    }

    // ########################################################### //
    private bool CheckInteractionField()
    {
        string text = primaryPickupText.GetComponent<Text>().text;

        return text.Length > 0;
    }

    private string GetFeedbackMsg(string hitItemName)
    {
        string selectedItemName = GetSelectedItem().name;
        string feedbackMsg = "";

        if (hitItemName == "DrawablePetrieDish")
        {
            switch (selectedItemName)
            {
                case "Pinceta":
                    feedbackMsg = "PLACE ANTIBIOTICS";
                    panel2.SetActive(true);
                    break;
                case "Marker":
                    feedbackMsg = "DRAW ON THE PETRI DISH";
                    panel2.SetActive(true);
                    break;
                case "Eza":
                    feedbackMsg = "SPREAD BACTERIA ON THE DISH";
                    panel2.SetActive(true);
                    break;
            }
        }
        else if (hitItemName == "PetrijevaZdjelicaBakterije")
        {
            feedbackMsg = "COLLECT BACTERIA";
            panel2.SetActive(true);
        }
        else if (hitItemName == "LabCentrifuga")
        {
            IncubatorScript incubator = GameObject.Find("LabCentrifuga").GetComponent<IncubatorScript>();

            if(incubator.antibiogramInside || incubator.overwrite) feedbackMsg = "MEASURE THE INHIBITION ZONES";
            else feedbackMsg = "PUT THE ANTIBIOGRAM IN AND SELECT THE TEMPERATURE";
            
            //panel2.SetActive(true);
        }
        else if(hitItemName == "TestTubeBase")
        {
            feedbackMsg = "place down the test tube";
        }
        if (CheckInteractionField()) return "";

        return "PRESS E TO " + (feedbackMsg == "" ? " INTERACT" : feedbackMsg);
    }
    private string GetSecondaryFeedbackMsg(string hitItemName)
    {
        string feedbackMsg = "";

        if (hitItemName == "DrawablePetrieDish")
        {
            feedbackMsg = "PUT THE LID ON";
            panel2.SetActive(true);
        }
        else if (hitItemName == "Poklopac")
        {
            feedbackMsg = "TAKE THE LID OFF";
            panel2.SetActive(true);
        }
        else if (hitItemName == "PetrijevaZdjelicaBakterije")
        {
            feedbackMsg = "PUT THE LID ON";
            panel2.SetActive(true);
        }
        else
        {
            panel2.SetActive(false);
        }

        return "PRESS R TO " + (feedbackMsg == "" ? " INTERACT" : feedbackMsg);
    }

    // ########################################################### //

    bool CanDisinfectFire(GameObject selectedObject)
    {
        bool containsItem = canDisinfectWithFire.Contains(selectedObject.name);
        if (!containsItem || !particles) return false;

        DisinfectionScript disinfectionScript = selectedObject.GetComponent<DisinfectionScript>();

        return particles.isPlaying && !disinfectionScript.GetIsClean();
    }

    bool canDisinfectWater()
    {
        bool cleanHands = gameObject.GetComponent<PlayerInventory>().cleanHands;
        if (particles && particles.isPlaying && !cleanHands) return true;
        else return false;
    }

    void DisinfectWithFire(GameObject selectedObject)
    {
        DisinfectionScript disinfectionScript = selectedObject.GetComponent<DisinfectionScript>();

        interactText.text = "HOLD E TO DISINFECT ITEM"; //Hold 'E' to disinfect item!
        panel.SetActive(true);
        if (Input.GetKey(KeyCode.E))
        {
            if (!IncrementInteractionProggress(fireInteractionDuration)) return;
            disinfectionScript.DisinfectItem();
            currentInteractionTimer = 0;
        }
        else currentInteractionTimer = 0;

        UpdateInteractionImg(fireInteractionDuration);
    }

    void DisinfectWithWater(GameObject selectedObject)
    {
        interactText.text = "HOLD E TO WASH AND DISINFECT HANDS"; //Hold 'E' to wash and disinfect hands!
        panel.SetActive(true);

        if (Input.GetKey(KeyCode.E))
        {
            if (!IncrementInteractionProggress(waterInteractionDuration)) return;

            PlayerInventory inventory = gameObject.GetComponent<PlayerInventory>();
            inventory.cleanHands = true;
            
            int nextStep = GameController.Instance.GetNextStep();
            int pranjeUlaz = GameController.Instance.GetStepIndexByName("Pranje ruku ulaz"); //1. korak
            int pranjeIzlaz = GameController.Instance.GetStepIndexByName("Pranje ruku izlaz"); //6. korak

            if (nextStep != pranjeIzlaz && (GameController.Instance.Steps[pranjeUlaz].StepDone || GameController.Instance.Steps[pranjeUlaz].WronglyDone))
            {
                GameController.Instance.CheckIfPreviousStepsDone(pranjeIzlaz);
                GameController.Instance.SetStepAsDone(pranjeIzlaz);
                
            }
            else if(nextStep == pranjeIzlaz) GameController.Instance.SetStepAsDone(pranjeIzlaz);
            
            if (nextStep != pranjeUlaz && !(GameController.Instance.Steps[pranjeUlaz].StepDone || GameController.Instance.Steps[pranjeUlaz].WronglyDone))
            {
                GameController.Instance.CheckIfPreviousStepsDone(pranjeUlaz);
                GameController.Instance.SetStepAsDone(pranjeUlaz);
                
            }
            else if (nextStep == pranjeUlaz)
            {
                GameController.Instance.SetStepAsDone(pranjeUlaz);
            }
            
            currentInteractionTimer = 0;
        }
        else currentInteractionTimer = 0;

        UpdateInteractionImg(waterInteractionDuration);
    }

    // ########################################################### //

    bool IncrementInteractionProggress(float requiredTime)
    {
        currentInteractionTimer += Time.deltaTime;
        UpdateInteractionImg(requiredTime);

        return currentInteractionTimer >= requiredTime;
    }

    void UpdateInteractionImg(float requiredTime)
    {
        float progressPcnt = currentInteractionTimer / requiredTime;
        InteractionProgressImg.fillAmount = progressPcnt;
    }

    // ########################################################### //
    void ToggleParticles(GameObject hitObject)
    {
        if (!particles) return;

        // Prikazi poruku playeru
        interactText.text = "PRESS E TO INTERACT";
        panel.SetActive(true);

        // Na pritisak 'E' pokreni/stopiraj dohvaceni particle system
        if (Input.GetKeyDown(KeyCode.E) && !particles.isPlaying)
        {
            particles.Play();
            ToggleParticleSounds(hitObject, false);
        }
        else if (Input.GetKeyDown(KeyCode.E) && particles.isPlaying)
        {
            particles.Stop();
            ToggleParticleSounds(hitObject, true);
        }

        
    }

    private void ToggleParticleSounds(GameObject hitObject, bool muteSound)
    {
        AudioSource audio = hitObject.GetComponent<AudioSource>();
        if (!audio) return;
        audio.mute = muteSound;
    }
}
