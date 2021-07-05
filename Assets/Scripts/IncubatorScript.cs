using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IncubatorScript : MonoBehaviour
{
    public GameObject GUIParent;
    public Button closeButton;
    public bool antibiogramInside = false;
    private bool isGUIOpen = false;
    
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void Interaction()
    {
        if (antibiogramInside && !isGUIOpen) OpenInterpretationGUI();
        if (!IsAntibiogramSelected() && !isGUIOpen) return;

        isGUIOpen = !isGUIOpen;
        antibiogramInside = true;

        GenerateResults();

        ToggleCursor();
        ToggleKeypadGUI();
        TogglePlayerMovement(); 
    }

    private void GenerateResults()
    {
        gameObject.GetComponent<GenerateResults>().GenerateAntibiogramResults();
    }

    private bool IsAntibiogramSelected()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        PlayerInventory inventory = player.GetComponent<PlayerInventory>();

        string itemName = inventory.getSelectedItem().name;
        if (itemName != "EmptyPetrieDish") return false;

        inventory.RemoveItemByName("EmptyPetrieDish");
        return true;
    }

    private void ToggleKeypadGUI()
    {
        bool isActive = GUIParent.activeSelf;
        GUIParent.SetActive(!isActive);
    }
    private void ToggleCursor()
    {
        if (GUIParent.activeSelf)
            Cursor.lockState = CursorLockMode.Locked;
        else
            Cursor.lockState = CursorLockMode.Confined;

        Cursor.visible = !Cursor.visible;
    }

    private void TogglePlayerMovement()
    {
        GameObject player = GameObject.FindWithTag("Player");
        PlayerController movementScript = player.GetComponent<PlayerController>();
        movementScript.ToggleMovement();


        MouseLook cameraMovementScript = GameObject.FindWithTag("PlayerCamera").GetComponent<MouseLook>();
        cameraMovementScript.ToggleMovement();
    }


    private void OpenInterpretationGUI()
    {
        ActivateResults();
        ToggleCursor();
        TogglePlayerMovement();
        ToggleCancelButton();
    }

    private void ToggleCancelButton()
    {
        bool isActive = closeButton.gameObject.activeSelf;
        closeButton.gameObject.SetActive(!isActive);
    }

    private void ActivateResults()
    {
        Transform parentContainer = GameObject.Find("ItemDropOff").transform;

        foreach (Transform child in parentContainer)
        {
            if (child.name == "DrawablePetrieDishBackground" || child.name == "Results") child.gameObject.SetActive(true);
        }
    }
}
