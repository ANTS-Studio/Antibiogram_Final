using System;
using DefaultNamespace;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public float radius = 30f;
    public Transform interactionTransform;
    public GameObject obj;
    public GameObject PlayerCamera;
    public GameObject interpretationPanel;
    private bool isFocus = false;
    private bool hasInteracted = false;
    public static bool IsPaused = false;
    public MouseLook MouseLookScript;
    private Transform player;
    private Animator anim;
    private Boolean opened = false;
    public InterpretationScript interpretation;

    public void Start()
    {
        PlayerCamera = GameObject.FindGameObjectWithTag("PlayerCamera");
        MouseLookScript = PlayerCamera.GetComponent<MouseLook>();
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
        switch (interactionTransform.tag)
        {
            case "Vrata":
                InteractionVrata();
                break;
            case "Laptop":
                InteractionLaptop();
                break;
            default:
                break;
        }
    }
    public void InteractionLaptop()
    {
        //Button u skripti InterpretationScript vraca je li interpretacija tocna ili netocna. Vraca bool
        interpretationPanel.SetActive(true);
        MouseLookScript.enabled = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0f;
        IsPaused = true;
        interpretation.SetMeasurmentValues(interpretation.collectedValues);

    }

    public void InteractionVrata()
    {
        Animator door = transform.GetComponent<Animator>();
        opened = !opened;
        door.SetBool("Opened", opened);
        PlayDoorSound();

        int nextStep = GameController.Instance.GetNextStep();
        int ulazLab = GameController.Instance.GetStepIndexByName("Ulazak u laboratorij"); //0. korak
        int izlazLab = GameController.Instance.GetStepIndexByName("Izlaz laboratorij"); //7. korak

        //izlaz iz laboratorija, iznad je zbog uvjeta
        if (nextStep != izlazLab && GameController.Instance.Steps[ulazLab].StepDone)
        {
            GameController.Instance.CheckIfPreviousStepsDone(izlazLab);
            GameController.Instance.SetStepAsDone(izlazLab);
            GameController.Instance.EndDayTrigger = true;
            GameController.Instance.EndDay();
        }
        else if (nextStep == izlazLab)
        {
            GameController.Instance.SetStepAsDone(izlazLab);
            GameController.Instance.EndDayTrigger = true;
            GameController.Instance.EndDay();
        }


        //ulaz u laboratorij
        if (nextStep == ulazLab)
        {
            GameController.Instance.SetStepAsDone(ulazLab);
        }
    }
    private void PlayDoorSound()
    {
        GameObject door = GameObject.Find("OfficeLabVrata");
        door.GetComponent<AudioSource>().Play();
    }
}