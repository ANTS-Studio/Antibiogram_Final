using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncubatorScript : MonoBehaviour
{
    public GameObject GUIParent;
    private bool isGUIOpen = false;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }

    public void Interaction()
    {
        if (!IsAntibiogramSelected() && !isGUIOpen) return;
        isGUIOpen = !isGUIOpen;

        ToggleCursor();
        ToggleKeypadGUI();
        TogglePlayerMovement(); 
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
}
