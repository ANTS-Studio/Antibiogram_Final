using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloseButtonHandler : MonoBehaviour
{
    public GameObject bacteriaDishBackground;
    public GameObject emptyDishBackground;
    public Text genericFeedback;

    public void CloseOverlay()
    {
        GameObject bacteriaDish = GameObject.Find("PetrijevaZdjelicaBakterije");
        if (bacteriaDishBackground.activeSelf) bacteriaDish.GetComponent<GenerateBacteria>().Interaction();

        GameObject emptyDish = GameObject.Find("DrawablePetrieDish");
        if (emptyDishBackground.activeSelf) emptyDish.GetComponent<EnableDrawing>().Interaction();

        Image InteractionProgressImg = GameObject.Find("proggressIndicator").GetComponent<Image>();
        InteractionProgressImg.fillAmount = 0;

        HideAntibioticTray();
        CloseResults();

        genericFeedback.text = "";
    }

    private void HideAntibioticTray()
    {
        Transform itemDropOff = GameObject.Find("ItemDropOff").transform;

        foreach (Transform child in itemDropOff)
        {
            if (child.name == "Tray") child.gameObject.SetActive(false);
        }
    }

    private void CloseResults()
    {
        Transform itemDropOff = GameObject.Find("ItemDropOff").transform;

        foreach (Transform child in itemDropOff)
        {
            if (child.name == "Results") child.gameObject.SetActive(false);
        }
    }
}
