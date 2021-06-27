using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    public LayerMask layerMask;

    public Camera mainCam;
    public GameObject dropOff;

    public Text selectedSlotDisplay;
    public List<Text> interactText;

    public float maxDistance = 60f;

    private bool hasGloves = false;
    public bool cleanHands = false;

    private int selectedInventorySlot = 0;
    public int inventorySize = 5;
    public List<GameObject> playerInventory = new List<GameObject>();

    // when no item in inventory, getSelectedItem returns this
    private GameObject fallBackObject;

    // Start is called before the first frame update
    void Start()
    {
        fallBackObject = new GameObject("emptyList");
    }

    // Update is called once per frame
    void Update()
    {
        this.SelectInventorySlot();

        this.DropItem();
        this.AddItemToInventory();
    }

    void SetText(int fieldIndex, string text)
    {
        fieldIndex = fieldIndex > interactText.Count ? fieldIndex : 0;
        interactText[fieldIndex].text = text;
    }
    public GameObject getSelectedItem()
    {
        return playerInventory.Count == 0 ? fallBackObject : playerInventory[selectedInventorySlot];
    }
    void AddItemToInventory()
    {
        Ray ray = mainCam.ViewportPointToRay(Vector3.one / 2f);
        //Debug.DrawRay(ray.origin, ray.direction * maxDistance, Color.blue); // Prikazi Ray

        RaycastHit hitInfo;

        // Trazi item koji se nalaze na odredenom layeru
        if (Physics.Raycast(ray, out hitInfo, maxDistance, layerMask))
        {
            GameObject hitItem = hitInfo.collider.gameObject;
            if (hitItem.name == "LabOpasniOtpad")
            {
                ThrowAwayItem();
                ThrowAwayGloves();
                return;
            }
            if (hitItem.name == "Gloves")
            {
                EquipGloves(hitItem);
                return;
            }
            // Prikazi poruku playeru i dohvati reference na pogodeni item
            SetText(0, "Press 'E' to pick up");

            // Ako player pritisne 'E' i ima mjesta u inventory, dodaj item
            if (Input.GetKeyDown(KeyCode.E) && playerInventory.Count <= inventorySize)
            {
                hitItem.SetActive(false);
                playerInventory.Add(hitItem);
                this.AdjustSelectedItemDisplay();
            }
        }
        else
        {
            SetText(0, "");
        }
    }

    void EquipGloves(GameObject gloves)
    {
        SetText(0, "Press 'E' to put on gloves!");

        if (Input.GetKeyDown(KeyCode.E))
        {
            gloves.SetActive(false);
            hasGloves = true;
            
            Debug.Log(GameController.Instance.Steps);
            GameController.Instance.Steps[2].StepDone = true;
            GameController.Instance.CheckIfPreviousStepDone();
            int lastStep = GameController.Instance.GetLastStep();
            Debug.Log("Current: " + GameController.Instance.Steps[lastStep].Name);
            int nextStep = GameController.Instance.GetNextStep();
            Debug.Log("Next: " + GameController.Instance.Steps[nextStep].Name);
            
            if (!cleanHands) Debug.Log("+1 Mistake(player didnt was hands)");
        }
    }
    void ThrowAwayGloves()
    {
        if (!hasGloves) return;
        SetText(1, "Press 'R' to throw away gloves!");

        if (Input.GetKeyDown(KeyCode.R))
        {
            hasGloves = false;
            cleanHands = false;
        }
    }

    void ThrowAwayItem()
    {
        if (playerInventory.Count == 0)
        {
            SetText(0, "");
            return;
        }

        SetText(0, "Press 'E' to throw item away");

        if (Input.GetKeyDown(KeyCode.E))
        {
            GameObject item = getSelectedItem();
            playerInventory.RemoveAt(selectedInventorySlot);

            Destroy(item);
            this.AdjustSelectedItemDisplay();
        }
    }
    void DropItem()
    {
        // Ako nema item-a u inventry, ne izvrsavaj funkciju
        if (playerInventory.Count == 0) return;

        // 'G' triggera item drop
        if (Input.GetKeyDown(KeyCode.G))
        {
            // Ukloni item iz inventory-a
            GameObject item = playerInventory[selectedInventorySlot];
            playerInventory.RemoveAt(selectedInventorySlot);

            // Postavi poziciju itema ispred playera i ukljuci item
            item.transform.position = dropOff.transform.position;
            item.SetActive(true);

            // Postavi selectedSlot posto ovaj item nije vise u inventory
            this.AdjustSelectedItemDisplay();
        }
    }

    void AdjustSelectedItemDisplay()
    {
        int itemsInInventory = playerInventory.Count;

        // Ako nema item-a u inventory, izbrisi display tekst i postavi selectedSlot na -1
        if (itemsInInventory == 0)
        {
            selectedInventorySlot = -1;
        }

        // Ako je dodan prvi item u inventory, postavi selectedSlot na njega
        else if (selectedInventorySlot == -1)
        {
            selectedInventorySlot = 0;
        }

        // Ako je iz inventory-a izbacen posljednji item, postavi selectedSlot na prvi slobodni ili na -1
        else if (selectedInventorySlot > itemsInInventory - 1)
        {
            selectedInventorySlot = itemsInInventory - 1;
        }

        // Postavi/Ukloni ime item-a
        selectedSlotDisplay.text = selectedInventorySlot != -1 ? getSelectedItem().name : "";
    }
    void SelectInventorySlot()
    {
        float mouseScroll = Input.mouseScrollDelta.y;
        int itemInInventorys = playerInventory.Count;

        // Ako scroll wheel nije pomaknut ili nema item-a u inventory-u, ne izvrsavaj funkciju
        if (mouseScroll == 0 || itemInInventorys == 0) return;

        // Odaberi sljedeci item u inventory-u ako trenutni nije ujedno i posljednji
        if (mouseScroll > 0 && (selectedInventorySlot < (itemInInventorys - 1)))
        {
            selectedInventorySlot++;
        }
        // Odaberi prethodni item u inventory-u ako trenutni nije ujedno i prvi
        else if (mouseScroll < 0 && selectedInventorySlot > 0)
        {
            selectedInventorySlot--;
        }

        // Postavi display tekst imena odabranog item-a
        selectedSlotDisplay.text = getSelectedItem() ? getSelectedItem().name : "";
    }
}
