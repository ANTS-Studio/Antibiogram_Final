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

    public float maxDistance = 70f;

    private RaycastHit hitInfo;
    private ParticleSystem particles;
    private List<string> canDisinfectWithFire;
    private List<string> canDisinfectWithWater;
    private List<string> interactableItems;
    private List<string> secondaryInteractions;

    private float currentInteractionTimer = 0;
    public Image InteractionProgressImg;

    public float fireInteractionDuration = 4f;
    public float waterInteractionDuration = 4f;

    private float coolDownTimer = 1f;

    // Start is called before the first frame update
    void Start()
    {
        canDisinfectWithFire = new List<string> { "Eza", "Pinceta" };
        canDisinfectWithWater = new List<string> { "" };

        interactableItems = new List<string> { "PetrijevaZdjelicaBakterije", "DrawablePetrieDish", "LabCentrifuga" };
        secondaryInteractions = new List<string> { "PetrijevaZdjelicaBakterije", "DrawablePetrieDish", "Poklopac" };
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = mainCam.ViewportPointToRay(Vector3.one / 2f);
        Debug.DrawRay(ray.origin, ray.direction * maxDistance, Color.red);

        // Trazi item koji se nalaze na odredenom layeru
        if (Physics.Raycast(ray, out hitInfo, maxDistance, layerMask))
        {
            particles = hitInfo.collider.gameObject.GetComponentInChildren<ParticleSystem>();

            GameObject selectedObject = GetSelectedItem();
            string hitItemName = hitInfo.collider.gameObject.name;

            if (CanInteract(hitItemName))
                GenericInteraction(hitInfo.collider.gameObject);
            else if (hitItemName == "bunsen_burner" && CanDisinfectFire(selectedObject))
                this.DisinfectWithFire(selectedObject);
            else if (hitItemName == "ID309" && canDisinfectWater())
                this.DisinfectWithWater(selectedObject);
            else
                ToggleParticles();
        }
        else
        {
            interactText.text = "";
            secondaryInteractText.text = "";
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
                    break;
                case "PetrijevaZdjelicaBakterije":
                    hitItem.gameObject.GetComponent<GenerateBacteria>().Interaction();
                    break;
                case "DrawablePetrieDish":
                    hitItem.gameObject.GetComponent<EnableDrawing>().Interaction();
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
                break;
            case "DrawablePetrieDish":
                hitItem.gameObject.GetComponent<EnableDrawing>().SecondaryInteraction();
                break;
            case "Poklopac":
                hitItem.gameObject.GetComponent<PetrieDishLid>().Interaction();
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

    private string GetFeedbackMsg(string hitItemName)
    {
        string selectedItemName = GetSelectedItem().name;
        string feedbackMsg = "";

        if (hitItemName == "DrawablePetrieDish")
        {
            switch (selectedItemName)
            {
                case "Pinceta":
                    feedbackMsg = "place antibiotics";
                    break;
                case "Marker":
                    feedbackMsg = "draw on the petrie dish";
                    break;
                case "Eza":
                    feedbackMsg = "spread bacteria in the dish";
                    break;
            }
        }
        else if (hitItemName == "PetrijevaZdjelicaBakterije")
        {
            feedbackMsg = "collect bacteria";
        }
        else if (hitItemName == "LabCentrifuga")
        {
            feedbackMsg = "put the antibiogram in and select temperature";
        }

        return "Press 'E' to " + (feedbackMsg == "" ? " interact" : feedbackMsg);
    }
    private string GetSecondaryFeedbackMsg(string hitItemName)
    {
        string feedbackMsg = "";

        if (hitItemName == "DrawablePetrieDish")
        {
            feedbackMsg = "put the lid on";
        }
        else if (hitItemName == "Poklopac")
        {
            feedbackMsg = "take the lid off";
        }
        else if (hitItemName == "PetrijevaZdjelicaBakterije")
        {
            feedbackMsg = "put the lid on";
        }

        return "Press 'R' to " + (feedbackMsg == "" ? " interact" : feedbackMsg);
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

        interactText.text = "HOLD TO DISINFECT ITEM"; //Hold 'E' to disinfect item!
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
        interactText.text = "HOLD TO WASH AND DISINFECT HANDS"; //Hold 'E' to wash and disinfect hands!

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
    void ToggleParticles()
    {
        if (!particles) return;

        // Prikazi poruku playeru
        interactText.text = "PRESS E TO TOGGLE PARTICLES";

        // Na pritisak 'E' pokreni/stopiraj dohvaceni particle system
        if (Input.GetKeyDown(KeyCode.E) && !particles.isPlaying)
        {
            particles.Play();
        }
        else if (Input.GetKeyDown(KeyCode.E) && particles.isPlaying)
        {
            particles.Stop();
        }
    }
}
