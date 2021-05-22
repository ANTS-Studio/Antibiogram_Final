using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisinfectItem : MonoBehaviour
{
    public bool isDisinfected = false;
    private bool hasBacteria = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.checkForMistake();
        this.disinfectedItem();
    }
    void checkForMistake()
    {
        bool isMistake = !isDisinfected && hasBacteria;
        Debug.Log("Mistake: " + isMistake);
    }
    void disinfectedItem()
    {

    }
}
