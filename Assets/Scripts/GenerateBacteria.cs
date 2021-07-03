using UnityEngine;
using UnityEngine.UI;

public class GenerateBacteria : MonoBehaviour
{
    public int numberOfCircles = 50;
    public int parentCircleSize = 12;

    public Transform circle;
    public Button closeButton;
    public GameObject teleportPosition;

    private GameObject parentObject;

    void Start()
    {
        CreateParent();
        GenerateBacteriaCircles();

        SetBacteriaPosition();
    }

    private void CreateParent()
    {
        parentObject = new GameObject();
        parentObject.SetActive(false);
    }

    private void GenerateBacteriaCircles()
    {
        for (int i = 0; i < numberOfCircles; i++)
        {
            int circleSize = Random.Range(1, 5);
            circle.localScale = new Vector3(1 * circleSize, 0.125f, 1 * circleSize);

            Vector2 pos = Random.insideUnitCircle * parentCircleSize;
            Vector3 newPos = new Vector3(pos.x, 0, pos.y);

            var newGameobj = Instantiate(circle, newPos, Quaternion.identity);
            newGameobj.transform.parent = parentObject.transform;
        }
    }

    private void SetBacteriaPosition()
    {
        Transform player = GameObject.FindWithTag("Player").transform;
        Transform playerModel = player.GetChild(0);

        foreach (Transform child in playerModel)
        {
            if (child.name != "ItemDropOff") continue;

            parentObject.transform.position = child.position + (child.forward * 4);
            parentObject.transform.parent = child.transform;

            break;
        }

        parentObject.transform.rotation = Quaternion.Euler(90, 0, -95);
    }

    public void Interaction()
    {
        TogglePetrieDishBackground();
        ToggleCancleButton();
        ToggleCursor();
        TogglePlayerMovement();
        ToggleBacteria();
    }

    private void TogglePetrieDishBackground()
    {
        Transform parent = parentObject.transform.parent;

        foreach(Transform child in parent)
        {
            if (child.name != "PetrieDishBackground") continue;
            child.gameObject.SetActive(!child.gameObject.activeSelf);
        }   
    }

    private void ToggleCancleButton()
    {
        bool isActive = closeButton.gameObject.activeSelf;
        closeButton.gameObject.SetActive(!isActive);
    }

    private void ToggleBacteria()
    {
        parentObject.SetActive(!parentObject.activeSelf);
    }

    private void ToggleCursor()
    {
        if (parentObject.activeSelf)
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

        player.transform.position = teleportPosition.transform.position;
        player.transform.rotation = Quaternion.Euler(0, 90, 0);
    }

    public void SecondaryInteraction()
    {
        ToggleDishLid();
        RemoveItemFromInventory();
    }

    private void ToggleDishLid()
    {
        Transform parent = gameObject.transform.parent;
        foreach(Transform child in parent)
        {
            if (child.name != "Poklopac") continue;
            child.gameObject.SetActive(true);
        }
    }

    private void RemoveItemFromInventory()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        PlayerInventory inventory = player.GetComponent<PlayerInventory>();

        inventory.RemoveItemByName("Poklopac");
    }
}
