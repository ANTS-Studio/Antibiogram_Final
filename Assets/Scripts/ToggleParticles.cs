using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ToggleParticles : MonoBehaviour
{
    public Camera mainCam;
    public LayerMask layerMask;
    public Text interactText;
    public ParticleSystem particles;

    public float maxDistance = 70f;

    private bool activeParticles = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = mainCam.ViewportPointToRay(Vector3.one / 2f);
        Debug.DrawRay(ray.origin, ray.direction * maxDistance, Color.red); 

        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo, maxDistance, layerMask))
        {
            interactText.text = "Press 'E' to interact";

            if (Input.GetKeyDown(KeyCode.E) && !activeParticles)
            {
                particles.Play();
                this.activeParticles = true;
            }
            else if (Input.GetKeyDown(KeyCode.E) && activeParticles)
            {
                particles.Stop();
                this.activeParticles = false;
            }
        }
        else
        {
            interactText.text = "";
        }
    }
}
