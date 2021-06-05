using System;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public float radius = 30f;
    public Transform interactionTransform;
    private bool isFocus = false;
    private bool hasInteracted = false;
    private Transform player;

    public virtual void Interact()
    {
        //za overridanje
        Debug.Log("Interakcija s " + interactionTransform.tag);
    }
    private void Update()
    {
        if (isFocus && !hasInteracted)
        {
            float distance = Vector3.Distance(player.position, interactionTransform.position);
            if (distance <= radius)
            {
                Interact();
                hasInteracted = true;
            }
        }
    }

    public void OnFocused(Transform playerTransform)
    {
        isFocus = true;
        player = playerTransform;
        hasInteracted = false;
    }

    public void OnDefocused()
    {
        isFocus = false;
        player = null;
        hasInteracted = false;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(interactionTransform.position, radius);
    }
}