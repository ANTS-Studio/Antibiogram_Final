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
    private

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

        interactText.text = "Press 'E' to disinfect item!";
        if (Input.GetKeyDown(KeyCode.E))
        {
            disinfectionScript.DisinfectItem();
            //interactText.text = "Item disinfected!";
        }
    }

    void DisinfectWithWater(GameObject selectedObject)
    {
        interactText.text = "Press 'E' to wash and disinfect hands!";

        if (Input.GetKeyDown(KeyCode.E))
        {
            PlayerInventory inventory = gameObject.GetComponent<PlayerInventory>();
            inventory.cleanHands = true;
            //interactText.text = "Hands washed and disinfected!";
        }
    }

    void ToggleParticles()
    {
        if (!particles) return;

        // Prikazi poruku playeru
        interactText.text = "Press 'E' to interact";

        // Na pritisak 'E' pokreni/stopiraj dohvaceni particle system
        if (Input.GetKeyDown(KeyCode.E) && !particles.isPlaying) particles.Play();
        else if (Input.GetKeyDown(KeyCode.E) && particles.isPlaying) particles.Stop();
    }
}
