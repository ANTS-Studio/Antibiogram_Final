using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PetrieDishLid : MonoBehaviour
{
    PlayerInventory inventory;
    
    private void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory>();
    }

    public void Interaction()
    {
        ToggleActiveState();
        ToggleItemInInventory();
    }

    private void ToggleActiveState()
    {
        Transform parent = gameObject.transform.parent;
        parent.gameObject.SetActive(false);
    }

    private void ToggleItemInInventory()
    {
        inventory.AddItemToInventory(gameObject);
    }
}
