using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ToggleParticles : MonoBehaviour
{
    public Camera mainCam;
    public LayerMask layerMask;
    public Text interactText;

    public float maxDistance = 70f;

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

        // Trazi item koji se nalaze na odredenom layeru
        if (Physics.Raycast(ray, out hitInfo, maxDistance, layerMask))
        {
            // Dohvati particle system pogodenog itema, ako ne postoji particleSystem return
            ParticleSystem particles = hitInfo.collider.gameObject.GetComponentInChildren<ParticleSystem>();
            if (!particles) return;

            // Prikazi poruku playeru
            interactText.text = "Press 'E' to turn interact";

            // Na pritisak 'E' pokreni/stopiraj dohvaceni particle system
            if (Input.GetKeyDown(KeyCode.E) && !particles.isPlaying)
            {
                particles.Play();
            }
            else if (Input.GetKeyDown(KeyCode.E) && particles.isPlaying)
            {
                particles.Stop();
            }
        }
        else
        {
            interactText.text = "";
        }
    }
}
