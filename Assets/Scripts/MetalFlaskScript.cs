using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MetalFlaskScript : MonoBehaviour
{
    private float currentInteractionTimer = 0;
    public Image InteractionProgressImg;
    
    void Start()
    {
        GameObject proggressIndicator = GameObject.Find("proggressIndicator");
        InteractionProgressImg = proggressIndicator.GetComponent<Image>();
    }

    // Start is called before the first frame update
    public void Interaction()
    {
        if (!IsEzaSelected()) return;

        if (!IncrementInteractionProggress(4f)) return;
        SetWasInFlask(true);

        currentInteractionTimer = 0;
    }

    private bool IsEzaSelected()
    {
        GameObject player = GameObject.Find("Player");
        PlayerInventory inventory = player.GetComponent<PlayerInventory>();

        GameObject selectedItem = inventory.getSelectedItem();
        
        if (selectedItem.name != "Eza") return false;

        float bacteriaCount = selectedItem.GetComponent<DisinfectionScript>().GetEzaBacteriaPercentage();
        if (bacteriaCount < 1) return false;

        return true;
    }

    private void SetWasInFlask(bool value)
    {
        GameObject player = GameObject.Find("Player");
        PlayerInventory inventory = player.GetComponent<PlayerInventory>();
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
