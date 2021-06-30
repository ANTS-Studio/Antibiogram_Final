using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialScript : MonoBehaviour
{
    public GameObject Panel;
    public GameObject TutorialText;
    public SceneController sceneController;
    public GameController gameController;


    public void CheckIfTutorialIsNeeded()
    {
        if(sceneController.IsCurrentSceneEducational())
        {
            OpenTutorialPanel();
        }
        else
        {
            if(gameController.level == 0)
            {
                OpenTutorialPanel();
            }

        }
    }

    public string GetNextStepTutorialText()
    {
        int stepId = GameController.Instance.currentStepIndex + 1;
        string tutorialText = GameController.Instance.Steps[stepId].TutorialText;
        return tutorialText;

    }
    public void SetTutorialText()
    {
        TutorialText.GetComponent<TMPro.TextMeshProUGUI>().text = GetNextStepTutorialText();
    }

    public void OpenTutorialPanel()
    {
        Panel.SetActive(true);
        SetTutorialText();
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        CheckIfTutorialIsNeeded();
    }
}
