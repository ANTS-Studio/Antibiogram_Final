
using System;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public float radius = 35f;
    public Vector3 position;
    private bool isFocus = false;
    private Transform player;

    private void Update()
    {
        if (isFocus)
        {
            float distance = Vector3.Distance(player.position, transform.position);
            if (distance <= radius)
            {
                Debug.Log("INTERACT");
            }
        }
    }

    public void OnFocused(Transform playerTransform)
    {
        isFocus = true;
        player = playerTransform;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        position = transform.position;
        Gizmos.DrawWireSphere(position, radius);
    }
}

