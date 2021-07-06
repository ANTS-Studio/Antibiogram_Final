using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IncubatorScript : MonoBehaviour
{
    public GameObject GUIParent;
    public Button closeButton;
    public bool antibiogramInside = false;
    public bool overwrite = false;
    private bool isGUIOpen = false;
    
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        //GenerateResults();
        //OpenInterpretationGUI();

        int curLevel = GameController.Instance.level;
        if (curLevel == 2 || curLevel == 4) overwrite = true;
    }

    public void Interaction()
    {
        if ((antibiogramInside && !isGUIOpen) || overwrite) OpenInterpretationGUI();
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

        GameObject teleportPosition = GameObject.Find("TeleportPosition");
        player.transform.position = teleportPosition.transform.position;
        player.transform.rotation = Quaternion.Euler(0, 90, 0);
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
            else if (child.name == "Results 1(Clone)") child.gameObject.SetActive(true);
        }
    }
}
