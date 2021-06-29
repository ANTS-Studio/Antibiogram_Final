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
            var stepToBeDone = GameController.Instance.Steps.Find(x => x.Name.Equals("Sterilizacija ušice")); //4. korak
            GameController.Instance.Steps[stepToBeDone.ID].StepDone = true;
            GameController.Instance.CheckIfPreviousStepDone();
            int nextStep = GameController.Instance.GetNextStep();
            Debug.Log("Next: " + GameController.Instance.Steps[nextStep].Name);
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
