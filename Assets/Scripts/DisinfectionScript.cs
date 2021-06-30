using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisinfectionScript : MonoBehaviour
{
    public PickupableObject pickupableObject;

    private void Start()
    {
        pickupableObject.isClean = false;
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
    }
    public bool GetIsClean()
    {
        return pickupableObject.isClean;
    }
    private void OnTriggerEnter(Collider other)
    {
        pickupableObject.isClean = false;
    }
}
