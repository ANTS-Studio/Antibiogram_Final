using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Inventory/Item")]
public class PickupableObject : ScriptableObject
{
    public bool isClean = false;
    public float bacteriaPercentage = 0;
    public bool wasInFlask = false;
}
