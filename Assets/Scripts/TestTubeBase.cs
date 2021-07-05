using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTubeBase : MonoBehaviour
{
    public void Interaction()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        PlayerInventory inventory = player.GetComponent<PlayerInventory>();

        GameObject selectedItem = inventory.getSelectedItem();

        if(selectedItem.name == "Test tube");

        selectedItem.SetActive(true);
        inventory.RemoveItemByName("Test tube");
    }
}
