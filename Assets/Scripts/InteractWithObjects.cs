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

            if (hitInfo.collider.gameObject.name == "bunsen_burner" && CanDisinfect(selectedObject, "fire"))
                this.DisinfectWithFire(selectedObject);
            else if (hitInfo.collider.gameObject.name == "ID309" && CanDisinfect(selectedObject, "water"))
                this.DisinfectWithWater(selectedObject);
            else
                ToggleParticles();
        }
        else
        {
            interactText.text = "";
        }
    }

    bool CanDisinfect(GameObject selectedObject, string disinfectionMethod)
    {
        List<string> namesList = disinfectionMethod == "fire" ? canDisinfectWithFire : canDisinfectWithWater;

        bool containsItem = namesList.Contains(selectedObject.name);
        if (!containsItem || !particles) return false;

        DisinfectionScript disinfectionScript = selectedObject.GetComponent<DisinfectionScript>();
        return  particles.isPlaying && !disinfectionScript.GetIsClean();
    }

    void DisinfectWithFire(GameObject selectedObject)
    {
        DisinfectionScript disinfectionScript = selectedObject.GetComponent<DisinfectionScript>();

        interactText.text = "Press 'E' to disinfect item!";
        if (Input.GetKeyDown(KeyCode.E))
        {
            disinfectionScript.DisinfectItem();
            interactText.text = "Item disinfected!";
        }
    }

    void DisinfectWithWater(GameObject selectedObject)
    {

    }

    void ToggleParticles()
    {
        if (!particles) return;

        // Prikazi poruku playeru
        interactText.text = "Press 'E' to turn interact";

        // Na pritisak 'E' pokreni/stopiraj dohvaceni particle system
        if (Input.GetKeyDown(KeyCode.E) && !particles.isPlaying) particles.Play();
        else if (Input.GetKeyDown(KeyCode.E) && particles.isPlaying) particles.Stop();
    }
}
