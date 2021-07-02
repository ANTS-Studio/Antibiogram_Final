using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour{
    public float MouseRotateSpeed = 120f;
    float xRotation = 0f;
    
    public GameObject player;

    public void ShakeThatCamera()
    {

    }

    void Start(){
        Cursor.lockState = CursorLockMode.Locked;
        player = GameObject.FindWithTag("Player");
    }

    void Update(){
        float mouseX = Input.GetAxis("Mouse X") * MouseRotateSpeed * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * MouseRotateSpeed * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        //float shaker = xRotation + Random.Range(-2, 2);

        //if funkcija pozvana onda:
        //transform.localRotation = Quaternion.Euler(xRotation + Random.Range(-2, 2) , 0, 0);
        //inace
        transform.localRotation = Quaternion.Euler(xRotation , 0, 0);

        player.transform.Rotate(0, mouseX, 0);
    }
}

//Story
//Day0 - Izrada (Da) - Interpretacija (Da)
//Day1 - Izrada (Da) - Interpretacija (Ne)
//Day2 - Izrada (Ne) - Interpretacija (Da)
//Day3 - Izrada (Da) - Interpretacija (Ne)
//Day4 - Izrada (Da) - Interpretacija (Da)
//Day5 - Izrada (Ne) - Interpretacija (Da)
