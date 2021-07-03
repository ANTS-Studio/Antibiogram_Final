using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Antibiotic : MonoBehaviour
{
    public Camera mainCam;
    private GameObject curObj;

    private Vector3 screenPoint;
    private Vector3 offset;

    private void Start()
    {
        curObj = gameObject;
    }


    void OnMouseDown()
    {
        screenPoint = mainCam.WorldToScreenPoint(curObj.transform.position);
        offset = curObj.transform.position - mainCam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
    }

    void OnMouseDrag()
    {
        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
        Vector3 curPosition = mainCam.ScreenToWorldPoint(curScreenPoint) + offset;
        curObj.transform.position = curPosition;
    }

    private void OnMouseUp()
    {
        GameObject petrieDish = GameObject.Find("DrawablePetrieDishBackground");
        curObj.transform.parent = petrieDish.transform;
    }
}
