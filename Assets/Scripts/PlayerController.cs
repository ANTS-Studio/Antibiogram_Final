using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Animator anim;
    public Camera cam;
    public Interactable focus;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    
    // Update is called once per frame
    void Update()
    {
        anim.SetFloat("Vertical", Input.GetAxis("Vertical"));
        anim.SetFloat("Horizontal", Input.GetAxis("Horizontal"));
        
        if (Input.GetKeyDown(KeyCode.E))
        {
            Ray ray = cam.ViewportPointToRay(Vector3.one / 2f);
            RaycastHit hit;
            Debug.Log("HIT E");
            if (Physics.Raycast(ray, out hit, 100))
            {
                Debug.Log("in raycast");
                Interactable interactable = hit.collider.GetComponent<Interactable>();
                Debug.Log(interactable);
                if (interactable != null)
                {
                    SetFocus(interactable);
                }
            }
        }
        else
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100))
            {
                RemoveFocus();
            }
        }
    }

    void SetFocus(Interactable newFocus)
    {
        focus = newFocus;
        newFocus.OnFocused(transform);
    }

    void RemoveFocus()
    {
        focus = null;
    }
}