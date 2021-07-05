using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetResults : MonoBehaviour
{
    public int resultValue = 0;

    private bool isResultCollected = false;
    private float currentInteractionTimer = 0;
    private Image InteractionProgressImg;
    private InterpretationScript interpetation;

    private void Start()
    {
        GameObject imageContainer = GameObject.Find("proggressIndicator");
        InteractionProgressImg = imageContainer.GetComponent<Image>();

        GetInterpretationScript();
    }

    private void GetInterpretationScript()
    {
        Transform interpretationUI = GameObject.Find("InterpretationCanvas").transform;

        foreach(Transform childInstance in interpretationUI)
        {
            if (childInstance.name != "DarkerPanel") continue;

            foreach(Transform child in childInstance)
            {
                if (child.name != "LighterPanel") continue;
                interpetation = child.gameObject.GetComponent<InterpretationScript>();
            }
        }
    }

    private void OnMouseDrag()
    {
        if (isResultCollected) return;
        if (Input.GetMouseButton(0))
        {
            if (!IncrementInteractionProggress(0.5f)) return;
        
            isResultCollected = true;
            InteractionProgressImg.fillAmount = 0;

            interpetation.AddMesuredValue(resultValue);
        }
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
