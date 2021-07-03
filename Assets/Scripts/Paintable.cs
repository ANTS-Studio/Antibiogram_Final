using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paintable : MonoBehaviour
{
    public Camera mainCam;
    public GameObject MarkerTrail;
    public GameObject BacteriaTrail;

    public float MarkerSize = 0.005f;
    public float BacteriaSize = 0.01f;

    private PlayerInventory inventory;

    // Start is called before the first frame update
    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        inventory = player.GetComponent<PlayerInventory>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            
            Ray Ray = mainCam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if(Physics.Raycast(Ray, out hit))
            {
                string hitItemName = hit.collider.gameObject.name;
                if (hitItemName != "DrawablePetrieDishBackground") return;

                Draw(hit.point);
            }
        }
    }

    private void Draw(Vector3 hitPoint)
    {
        string selectedItemName = inventory.getSelectedItem().name;


        switch (selectedItemName)
        {
            case "Marker":
                DrawMarker(hitPoint);
                break;
            case "Eza":
                DrawBacteria(hitPoint);
                break;
            case "Pinceta":
                break;
        }
    }

    private void DrawMarker(Vector3 hitPoint)
    {
        var go = Instantiate(MarkerTrail, hitPoint + Vector3.left * 0.2f, transform.rotation, transform);
        go.transform.localScale = Vector3.one * MarkerSize;
    }

    private void DrawBacteria(Vector3 hitPoint)
    {
        var go = Instantiate(BacteriaTrail, hitPoint + Vector3.left * 0.1f, transform.rotation, transform);
        go.transform.localScale = Vector3.one * BacteriaSize;
    }
}
