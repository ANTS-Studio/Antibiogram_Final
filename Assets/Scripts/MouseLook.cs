using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour{
    public float MouseRotateSpeed = 120f;
    float xRotation = 0f;

    public GameObject player;

    void Start(){
        Cursor.lockState = CursorLockMode.Locked;
        player = GameObject.FindWithTag("Player");
    }

    void Update(){
        float mouseX = Input.GetAxis("Mouse X") * MouseRotateSpeed * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * MouseRotateSpeed * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        player.transform.Rotate(0, mouseX, 0);
    }
}

