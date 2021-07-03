using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour{
    public float MouseRotateSpeed = 120f;
    float xRotation = 0f;

    public PlayerStatus status;
    public GameObject player;
    public bool canMove = true;

    IEnumerator ShakeThatCamera(float time)
    {
        float mouseY = Input.GetAxis("Mouse Y") * MouseRotateSpeed * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        float elapsed = 0;
        while(elapsed < time)
        {
            transform.localRotation = Quaternion.Euler(xRotation + Random.Range(-2.5f, 2.5f) , 0, 0);
            elapsed += Time.deltaTime;
            yield return null;
        }

    }

    public void ToggleMovement()
    {
        canMove = !canMove;
        transform.localRotation = Quaternion.Euler(0, 0, 0);
    }

    void Start() {
        Cursor.lockState = CursorLockMode.Locked;
        var barAndH =  GameObject.FindWithTag("BandH");
        status = barAndH.GetComponent<PlayerStatus>();
        player = GameObject.FindWithTag("Player");
    }

    void Update(){
        if (!canMove) return;

        float mouseX = Input.GetAxis("Mouse X") * MouseRotateSpeed * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * MouseRotateSpeed * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        float duration = status.ShakeController();
        if (duration > 0f)
        {
            StartCoroutine(ShakeThatCamera(duration));
        }
        transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        player.transform.Rotate(0, mouseX, 0);
    }
}


//funkcija prima sekunde i kao argument stavlja u trešnju


//Story
//Day0 - Izrada (Da) - Interpretacija (Da)
//Day1 - Izrada (Da) - Interpretacija (Ne)
//Day2 - Izrada (Ne) - Interpretacija (Da)
//Day3 - Izrada (Da) - Interpretacija (Ne)
//Day4 - Izrada (Da) - Interpretacija (Da)
//Day5 - Izrada (Ne) - Interpretacija (Da)
