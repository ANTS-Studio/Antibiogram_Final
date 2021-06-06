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
