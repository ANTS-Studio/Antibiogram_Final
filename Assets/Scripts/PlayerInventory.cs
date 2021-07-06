using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    public LayerMask layerMask;

    public Camera mainCam;
    public GameObject dropOff;

    public Text selectedSlotDisplay;
    public List<Text> interactText;
    public GameObject panel;
    public GameObject panel2;

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
        panel = GameObject.FindGameObjectWithTag("PanelForRaycast");
        panel.SetActive(false);
        panel2 = GameObject.FindGameObjectWithTag("PanelForInventory");
        panel2.SetActive(false);
        fallBackObject = new GameObject("emptyList");
    }

    // Update is called once per frame
    void Update()
    {
        GetList();
        this.SelectInventorySlot();
        this.DropItem();
        this.AddItemToInventory();
    }

    public void GetList()
    {
        if(playerInventory.Count == 0)
        {
            panel2.SetActive(false);
        }
        else
        {
            panel2.SetActive(true);
        }
    }

    void SetText(int fieldIndex, string text)
    {
        //if interactable false - pokazi ovo
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
            SetText(0, "PRESS E TO INTERACT");
            panel.SetActive(true);

            // Ako player pritisne 'E' i ima mjesta u inventory, dodaj item
            if (Input.GetKeyDown(KeyCode.E) && playerInventory.Count <= inventorySize)
            {
                if (hitItem.name == "Eza")
                {
                    int nextStep = GameController.Instance.GetNextStep();
                    int thisStep = GameController.Instance.GetStepIndexByName("Uzimanje ušice"); //3. korak
                    if (nextStep != thisStep)
                    {
                        GameController.Instance.CheckIfPreviousStepsDone(thisStep);
                        GameController.Instance.SetStepAsDone(thisStep);
                    }
                    else GameController.Instance.SetStepAsDone(thisStep);
                }

                hitItem.SetActive(false);
                AddItemToInventory(hitItem);
            }
        }
        else
        {
            SetText(0, "");
        }
    }

    void EquipGloves(GameObject gloves)
    {
        SetText(0, "PRESS E TO INTERACT");
        panel.SetActive(true);

        if (Input.GetKeyDown(KeyCode.E))
        {
            gloves.SetActive(false);
            hasGloves = true;
            
            int nextStep = GameController.Instance.GetNextStep();
            int thisStep = GameController.Instance.GetStepIndexByName("Stavljanje rukavica"); //2. korak
            if (nextStep != thisStep)
            {
                GameController.Instance.CheckIfPreviousStepsDone(thisStep);
                GameController.Instance.SetStepAsDone(thisStep);
                
            }
            else GameController.Instance.SetStepAsDone(thisStep);

            if (!cleanHands) Debug.Log("+1 Mistake(player didnt wash hands)");
        }
    }
    void ThrowAwayGloves()
    {
        if (!hasGloves)
        {
            return;
        }
        SetText(1, "PRESS R TO INTERACT");
        panel.SetActive(true);

        if (Input.GetKeyDown(KeyCode.R))
        {
            hasGloves = false;
            cleanHands = false;
            
            int nextStep = GameController.Instance.GetNextStep();
            int thisStep = GameController.Instance.GetStepIndexByName("Odlaganje rukavica u otpad"); //5. korak
            if (nextStep != thisStep)
            {
                GameController.Instance.CheckIfPreviousStepsDone(thisStep);
                GameController.Instance.SetStepAsDone(thisStep);
                
            }
            else GameController.Instance.SetStepAsDone(thisStep);
        }
    }

    void ThrowAwayItem()
    {
        if (playerInventory.Count == 0)
        {
            SetText(0, "");
            return;
        }

        GameObject item = getSelectedItem();
        if (item.name == "EmptyPetrieDish") return;

        SetText(0, "PRESS E TO INTERACT");
        panel.SetActive(true);

        if (Input.GetKeyDown(KeyCode.E))
        {
            playerInventory.RemoveAt(selectedInventorySlot);

            Destroy(item);
            
            int nextStep = GameController.Instance.GetNextStep();
            int thisStep = GameController.Instance.GetStepIndexByName("Odlaganje eze u otpad"); //6. korak
            if (nextStep != thisStep)
            {
                GameController.Instance.CheckIfPreviousStepsDone(thisStep);
                GameController.Instance.SetStepAsDone(thisStep);
                
            }
            else GameController.Instance.SetStepAsDone(thisStep);
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
            if (item.name == "EmptyPetrieDish") return;

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
        if (itemsInInventory == 0) selectedInventorySlot = -1;

        // Ako je dodan prvi item u inventory, postavi selectedSlot na njega
        else if (selectedInventorySlot == -1) selectedInventorySlot = 0;

        // Ako je iz inventory-a izbacen posljednji item, postavi selectedSlot na prvi slobodni ili na -1
        else if (selectedInventorySlot > itemsInInventory - 1) selectedInventorySlot = itemsInInventory - 1;

        // Postavi/Ukloni ime item-a
        string itemName = getSelectedItem().name;
        selectedSlotDisplay.text = selectedInventorySlot != -1 ? GetCorrectItemName(itemName).ToUpper() : "";

        //panel2.SetActive(true);
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
        string itemName = getSelectedItem() ? getSelectedItem().name : "";
        selectedSlotDisplay.text = GetCorrectItemName(itemName).ToUpper();
    }
    
    private string GetCorrectItemName(string itemName)
    {
        string result = itemName;
        switch (itemName)
        {
            case "Pinceta":
                result = "Tweezers";
                break;

            case "EmptyPetrieDish":
                result = "Antibiogram";
                break;
            
            case "Poklopac":
                result = "Lid";
                break;

            case "Eza":
                result = "Microstreaker";
                break;
        }
        return result;
    }

    public void AddItemToInventory(GameObject newItem)
    {
        gameObject.GetComponent<AudioSource>().Play();
        playerInventory.Add(newItem);
        this.AdjustSelectedItemDisplay();
    }
    public void RemoveItemByName(string itemName)
    {
        for (int index = 0; index < playerInventory.Count; index++)
        {
            GameObject item = playerInventory[index];
            if (item.name != itemName) continue;

            playerInventory.RemoveAt(index);

            AdjustSelectedItemDisplay();
            break;
        }
    }
    public GameObject GetItemByName(string itemName)
    {
        foreach (GameObject item in playerInventory)
        {
            if (item.name == itemName) return item;
        }
        return null;
    }
    public bool IsInInventory(string itemName)
    {
        bool result = false;

        foreach (GameObject item in playerInventory)
        {
            if (item.name != itemName) continue;

            result = true;
            break;
        }

        return result;
    }
}
