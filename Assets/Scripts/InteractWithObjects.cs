using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InteractWithObjects : MonoBehaviour
{
    public Camera mainCam;
    public LayerMask layerMask;
    public Text interactText;

    public float maxDistance = 70f;

    private RaycastHit hitInfo;
    private ParticleSystem particles;
    private List<string> canDisinfectWithFire;
    private List<string> canDisinfectWithWater;

    private float currentInteractionTimer = 0;
    public Image InteractionProgressImg;

    public float fireInteractionDuration = 4f;
    public float waterInteractionDuration = 4f;

    // Start is called before the first frame update
    void Start()
    {
        canDisinfectWithFire = new List<string> { "Eza" };
        canDisinfectWithWater = new List<string> { "" };
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

            PlayerInventory inventory = gameObject.GetComponent<PlayerInventory>();
            GameObject selectedObject = inventory.getSelectedItem();

            if (hitInfo.collider.gameObject.name == "bunsen_burner" && CanDisinfectFire(selectedObject))
                this.DisinfectWithFire(selectedObject);
            else if (hitInfo.collider.gameObject.name == "ID309" && canDisinfectWater())
                this.DisinfectWithWater(selectedObject);
            else
                ToggleParticles();
        }
        else
        {
            interactText.text = "";
        }
    }

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
            
            GameController.Instance.Steps[1].StepDone = true;
            int lastStep = GameController.Instance.GetLastStep();
            Debug.Log("Current: " + GameController.Instance.Steps[lastStep].Name);
            int nextStep = GameController.Instance.GetNextStep();
            Debug.Log("Next: " + GameController.Instance.Steps[nextStep].Name);
            
            currentInteractionTimer = 0;
        }
        else currentInteractionTimer = 0;

        UpdateInteractionImg(waterInteractionDuration);
    }
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

    void ToggleParticles()
    {
        if (!particles) return;

        // Prikazi poruku playeru
        interactText.text = "INTERACT";

        // Na pritisak 'E' pokreni/stopiraj dohvaceni particle system
        if (Input.GetKeyDown(KeyCode.E) && !particles.isPlaying) particles.Play();
        else if (Input.GetKeyDown(KeyCode.E) && particles.isPlaying) particles.Stop();
    }
}
