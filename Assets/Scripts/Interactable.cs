using System;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public float radius = 30f;
    public Transform interactionTransform;
    public GameObject obj;
    private bool isFocus = false;
    private bool hasInteracted = false;
    private Transform player;
    private Animator anim;

    public void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        anim = playerObj.GetComponent<Animator>();
        if (gameObject.transform.Find("InteractionPoint") != null)
        {
            Transform forInteractionTransform = gameObject.transform.Find("InteractionPoint");
            interactionTransform = forInteractionTransform;
        }
        else interactionTransform = transform;
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
        if (interactionTransform != null) Gizmos.DrawWireSphere(interactionTransform.position, radius);
    }

    public virtual void Interact()
    {
        Debug.Log("Interakcija s " + transform.tag);
        switch (interactionTransform.tag)
        {
            case "Fotelja":
                InteractionFotelja();
                break;
            case "Vrata":
                InteractionVrata();
                break;
            default:
                break;
        }
    }

    public void InteractionFotelja()
    {
        anim.SetTrigger(Animator.StringToHash("SitDown"));
    }

    public void InteractionVrata()
    {
        //to do
    }
}