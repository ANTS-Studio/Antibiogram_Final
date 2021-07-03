using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectBacteria : MonoBehaviour
{
    private Text feedbackMsg;
    private Image InteractionProgressImg;
    private float currentInteractionTimer = 0;

    private PlayerInventory inventory;

    private void Start()
    {
        InteractionProgressImg = GameObject.Find("proggressIndicator").GetComponent<Image>();
        feedbackMsg = GameObject.Find("GenericFeedbackMsg").GetComponent<Text>();

        GameObject player = GameObject.FindWithTag("Player");
        inventory = player.GetComponent<PlayerInventory>();

        GetEzaScript();
    }

    private DisinfectionScript GetEzaScript()
    {
        GameObject item = inventory.GetItemByName("Eza");
        if (!item) return null;
        return item.GetComponent<DisinfectionScript>();
    }

    public void GetEzaBacteriaValue()
    {
        DisinfectionScript ezascript = GetEzaScript();
        if (!ezascript) return;
        currentInteractionTimer = ezascript.GetEzaBacteriaPercentage();
    }

    void OnMouseDrag()
    {
        bool hasCorrectItem = inventory.IsInInventory("Eza");
        Debug.Log(hasCorrectItem);

        if (!hasCorrectItem)
        {
            StartCoroutine(ShowFeedbackMsg("You don't have the proper equipment in you inventory!"));
            return;
        }

        //GetEzaBacteriaValue();
        if (Input.GetMouseButton(0)) IncrementCounter();

        UpdateIndicator(4f);
    }

    IEnumerator ShowFeedbackMsg(string msg)
    {
        feedbackMsg.text = msg;
        yield return new WaitForSeconds(2);
        feedbackMsg.text = "";
    }

    private void IncrementCounter()
    {
        currentInteractionTimer += Time.deltaTime;
    }

    private void UpdateIndicator(float requiredTime)
    {
        float progress = currentInteractionTimer / requiredTime;
        UpdateBacteriaCount(progress);
        
        InteractionProgressImg.fillAmount = progress;
    }

    private void UpdateBacteriaCount(float bacteriaCount)
    {
        DisinfectionScript ezascript = GetEzaScript();
        if (!ezascript) return;
        ezascript.SetBacteriaCount(bacteriaCount);
    }
}
