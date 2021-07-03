using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnableDrawing : MonoBehaviour
{
    public Button closeButton;
    public GameObject teleportPosition;
    public Text interactionText;
    private Transform PetrieDishBackground;
    private PlayerInventory inventory;
    private string selectedItemName;

    private void Start()
    {
        Transform parent = GameObject.FindGameObjectsWithTag("Dropoff")[0].transform;

        foreach (Transform child in parent)
        {
            if (child.name == "DrawablePetrieDishBackground") PetrieDishBackground = child;
        }
    }

    // ########################################################### //

    private bool IsItemSelected()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        inventory = player.GetComponent<PlayerInventory>();

        List<string> allowedItem = new List<string> {"Marker", "Eza", "Pinceta"};
        selectedItemName = inventory.getSelectedItem().name;

        if (selectedItemName == "Pinceta") ShowAntibioticTray();
        Debug.Log(selectedItemName + " " + allowedItem.Contains(selectedItemName));
        return allowedItem.Contains(selectedItemName);
    }

    private void ShowAntibioticTray()
    {
        Transform itemDropOff = GameObject.Find("ItemDropOff").transform;

        foreach (Transform child in itemDropOff)
        {
            if (child.name == "Tray") child.gameObject.SetActive(true);
        }
    }

    public void Interaction()
    {
        if (!IsItemSelected()) return;

        ToggleCursor();
        TogglePetrieDishBackground();
        ToggleCancelButton();
        TogglePlayerMovement();
    }

    private void TogglePetrieDishBackground()
    {
        PetrieDishBackground.gameObject.SetActive(!PetrieDishBackground.gameObject.activeSelf);
    }

    private void ToggleCancelButton()
    {
        bool isActive = closeButton.gameObject.activeSelf;
        closeButton.gameObject.SetActive(!isActive);

        if (isActive && selectedItemName == "Eza")
        {
            int nextStep = GameController.Instance.GetNextStep();
            int thisStep = GameController.Instance.GetStepIndexByName("Šaranje podloge"); //8. korak
            if (nextStep != thisStep)
            {
                GameController.Instance.CheckIfPreviousStepsDone(thisStep);
                GameController.Instance.SetStepAsDone(thisStep);
            }
            else
            {
                GameController.Instance.SetStepAsDone(thisStep);
            }
        }
        else if (isActive && selectedItemName == "Marker")
        {
            int nextStep = GameController.Instance.GetNextStep();
            int thisStep = GameController.Instance.GetStepIndexByName("Crtanje sektora po zdjelici"); //10. korak
            if (nextStep != thisStep)
            {
                GameController.Instance.CheckIfPreviousStepsDone(thisStep);
                GameController.Instance.SetStepAsDone(thisStep);
            }
            else
            {
                GameController.Instance.SetStepAsDone(thisStep);
            }
        }
        else if (isActive && selectedItemName == "Pinceta")
        {
            int nextStep = GameController.Instance.GetNextStep();
            int thisStep = GameController.Instance.GetStepIndexByName("Uzimanje antibiotika pincetom i postavljanje na sektore"); //12. korak
            if (nextStep != thisStep)
            {
                GameController.Instance.CheckIfPreviousStepsDone(thisStep);
                GameController.Instance.SetStepAsDone(thisStep);
            }
            else
            {
                GameController.Instance.SetStepAsDone(thisStep);
            }
        }
    }

    private void ToggleCursor()
    {
        if (PetrieDishBackground.gameObject.activeSelf)
            Cursor.lockState = CursorLockMode.Locked;
        else
            Cursor.lockState = CursorLockMode.Confined;

        Cursor.visible = Cursor.lockState == CursorLockMode.Confined ? true : false;
    }

    private void TogglePlayerMovement()
    {
        GameObject player = GameObject.FindWithTag("Player");
        PlayerController movementScript = player.GetComponent<PlayerController>();
        movementScript.ToggleMovement();


        MouseLook cameraMovementScript = GameObject.FindWithTag("PlayerCamera").GetComponent<MouseLook>();
        cameraMovementScript.ToggleMovement();

        player.transform.position = teleportPosition.transform.position;
        player.transform.rotation = Quaternion.Euler(0, 90, 0);
    }

    // ########################################################### //

    public void SecondaryInteraction()
    {
        ToggleDishLid();
        RemoveItemFromInventory();
    }

    private void ToggleDishLid()
    {
        Transform parent = gameObject.transform.parent;
        foreach (Transform child in parent)
        {
            if (child.name != "Poklopac") continue;
            child.gameObject.SetActive(true);
            int nextStep = GameController.Instance.GetNextStep();
            int thisStep = GameController.Instance.GetStepIndexByName("Zatvaranje zdjelice"); //9. korak
            if (nextStep != thisStep)
            {
                GameController.Instance.CheckIfPreviousStepsDone(thisStep);
                GameController.Instance.SetStepAsDone(thisStep);
            }
            else
            {
                GameController.Instance.SetStepAsDone(thisStep);
            }
        }

        interactionText.text = "";
        if (IsReadyForPickUp()) ActivateBoundingBox();
    }

    private void RemoveItemFromInventory()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        PlayerInventory inventory = player.GetComponent<PlayerInventory>();

        inventory.RemoveItemByName("Poklopac");
    }

    private bool IsReadyForPickUp()
    {
        Transform parent = gameObject.transform.parent;


        return true;
    }

    private void ActivateBoundingBox()
    {
        GameObject parent = gameObject.transform.parent.gameObject;
        BoxCollider parentColider = parent.GetComponent<BoxCollider>();

        parentColider.enabled = true;
    }
}