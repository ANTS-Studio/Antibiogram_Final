using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisinfectionScript : MonoBehaviour
{
    public PickupableObject pickupableObject;

    private void Start()
    {
        pickupableObject.isClean = false;
        pickupableObject.bacteriaPercentage = 0;
        pickupableObject.wasInFlask = false;
    }

    public void DisinfectItem()
    {
        pickupableObject.isClean = true;
        if (pickupableObject.name == "Eza")
        {
            int nextStep = GameController.Instance.GetNextStep();
            int thisStep = GameController.Instance.GetStepIndexByName("Sterilizacija ušice"); //4. korak
            if (nextStep != thisStep)
            {
                GameController.Instance.CheckIfPreviousStepsDone(thisStep);
                GameController.Instance.SetStepAsDone(thisStep);
                
            }
            else GameController.Instance.SetStepAsDone(thisStep);
        }
        else if(pickupableObject.name == "Epruveta")
        {
            GameObject player = GameObject.Find("Player");
            PlayerInventory inventory = player.GetComponent<PlayerInventory>();

            GameObject flask = inventory.GetItemByName("Metal flask");
            flask.layer = 6;
        }
    }
    public bool GetIsClean()
    {
        return pickupableObject.isClean;
    }
    private void OnTriggerEnter(Collider other)
    {
        pickupableObject.isClean = false;
    }

    public void SetBacteriaCount(float bacteriaPercentage)
    {
        pickupableObject.bacteriaPercentage = bacteriaPercentage;
    }

    public float GetEzaBacteriaPercentage()
    {
        return pickupableObject.bacteriaPercentage;
    }

    public void SetWasInFlask(bool value)
    {
        pickupableObject.wasInFlask = value;
    }

    public bool GetWasInFlask()
    {
        return pickupableObject.wasInFlask;
    }
}
