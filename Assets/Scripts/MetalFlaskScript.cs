using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MetalFlaskScript : MonoBehaviour
{
    private float currentInteractionTimer = 0;
    private PlayerInventory inventory;
    public Image InteractionProgressImg;
    
    void Start()
    {
        GameObject proggressIndicator = GameObject.Find("proggressIndicator");
        InteractionProgressImg = proggressIndicator.GetComponent<Image>();

        GameObject player = GameObject.Find("Player");
        inventory = player.GetComponent<PlayerInventory>();
    }

    // Start is called before the first frame update
    public void Interaction()
    {
        if (!IsEzaSelected())
        {
            currentInteractionTimer = 0;
            return;
        }

        if (Input.GetKey(KeyCode.E))
        {
            if (!IncrementInteractionProggress(4f)) return;
            SetWasInFlask(true);

            currentInteractionTimer = 0;
        }
    }

    private bool IsEzaSelected()
    {
        GameObject selectedItem = inventory.getSelectedItem();
        
        if (selectedItem.name != "Eza") return false;

        float bacteriaCount = selectedItem.GetComponent<DisinfectionScript>().GetEzaBacteriaPercentage();
        if (bacteriaCount < 1) return false;

        if (gameObject.GetComponent<DisinfectionScript>().GetWasInFlask()) return false;

        return true;
    }

    private void SetWasInFlask(bool value)
    {
        GameObject selectedItem = inventory.getSelectedItem();
        selectedItem.GetComponent<DisinfectionScript>().SetWasInFlask(value);
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

}
