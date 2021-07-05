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

        Transform itemDropOff = GameObject.Find("ItemDropOff").transform;

        foreach (Transform child in itemDropOff)
        {
            child.gameObject.SetActive(false);
        }

        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().canMove = true;
        GameObject.FindGameObjectWithTag("PlayerCamera").GetComponent<MouseLook>().canMove = true;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        genericFeedback.text = "";
        gameObject.SetActive(false);
    }
}
