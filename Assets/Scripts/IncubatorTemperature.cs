using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IncubatorTemperature : MonoBehaviour
{
    public Text output;
    public AudioSource buttonPressSound;

    private float coolDownTimer = 1;

    private void Start()
    {
        GameObject soundSource = GameObject.Find("LabCentrifuga");
        buttonPressSound = soundSource.GetComponent<AudioSource>();

        SetOutput("0");
    }

    private void Update()
    {
        UpdateCooldown();
    }

    // ########################################################### //

    public void OnOKPress()
    {
        ButtonPressSoundPlay();

        if (IsCooldownActive()) return;
        coolDownTimer = 0;

        GameObject incubator = GameObject.Find("LabCentrifuga");
        incubator.GetComponent<IncubatorScript>().Interaction();
        
        int nextStep = GameController.Instance.GetNextStep();
        int thisStep = GameController.Instance.GetStepIndexByName("Stavljanje zdjelice u inkubator"); //13. korak
        if (nextStep != thisStep)
        {
            GameController.Instance.CheckIfPreviousStepsDone(thisStep);
            GameController.Instance.SetStepAsDone(thisStep);
                
        }
        else GameController.Instance.SetStepAsDone(thisStep);
    }

    public void DeleteLastInput()
    {
        ButtonPressSoundPlay();
        if (IsCooldownActive()) return;
        coolDownTimer = 0;

        string selectedValue = GetSelectedValue();

        if (selectedValue.Length == 1) selectedValue = "00";

        selectedValue = selectedValue.Remove(selectedValue.Length - 1, 1);
        SetOutput(selectedValue);
    }

    public void OnKeypadInput(string keyPadInput)
    {
        ButtonPressSoundPlay();
        if (IsCooldownActive()) return;
        coolDownTimer = 0;

        string selectedValue = GetSelectedValue();

        if (selectedValue.Length >= 3) return;

        if (selectedValue == "0") selectedValue = keyPadInput;
        else selectedValue += keyPadInput;


        SetOutput(selectedValue);
    }


    private string GetSelectedValue()
    {
        return output.text.Split('C')[0];
    }

    private void SetOutput(string selectedValue)
    {
        output.text = selectedValue + "C ";
    }

    // ########################################################### //

    private void UpdateCooldown()
    {
        if (!IsCooldownActive()) return;
        coolDownTimer += Time.deltaTime;

    }

    private bool IsCooldownActive()
    {
        return coolDownTimer <= 0.25f;
    }

    private void ButtonPressSoundPlay()
    {
        buttonPressSound.Play();
    }
}
