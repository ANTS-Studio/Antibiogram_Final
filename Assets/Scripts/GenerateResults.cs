using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateResults : MonoBehaviour
{
    public Transform resultCircle;
    public GameObject resultPreset;

    private List<Vector3> positions;
    private bool isGenerated = false;

    public void GenerateAntibiogramResults()
    {
        if (isGenerated) return;
        isGenerated = true;

        ClearAntibiogram();
        if (GameController.Instance.educationalMode) GenerateResultCircles();
        else GetResults();
    }

    private void ClearAntibiogram()
    {
        GameObject parentContainer = GameObject.Find("ItemDropOff");
        positions = GetPositionList(parentContainer);
    }

    private List<Vector3> GetPositionList(GameObject parentContainer)
    {
        List<Vector3> positions = new List<Vector3>();

        foreach (Transform childInstance in parentContainer.transform)
        {
            if (childInstance.name != "DrawablePetrieDishBackground") continue;


            foreach (Transform child in childInstance)
            {
                if (child.name == "Antibiotic") positions.Add(child.position);
                Destroy(child.gameObject);
            }
        }

        return positions;
    }

    private int GetResultValue(int curLevel = -1)
    {
        int result = Random.Range(10, 30); ;
        switch (curLevel)
        {
            case -1: // Edukaciski
                result = Random.Range(10, 30);
                break;
            case 2: //  Lose
                result = Random.Range(5, 15);
                break;
            case 4: // Dobro
                result = Random.Range(15, 50);
                break;
        }

        return result;
    }

    private void GenerateResultCircles()
    {
        GameObject parentContainer = GameObject.Find("ItemDropOff");
        GameObject resultParent = new GameObject();
        resultParent.name = "Results";
        resultParent.transform.SetParent(parentContainer.transform, false);

        foreach (Vector3 position in positions)
        {
            int circleSize = Random.Range(2, 5);
            resultCircle.localScale = new Vector3(1 * circleSize, 0.125f, 1 * circleSize);

            GameObject newGameobj = Instantiate(resultCircle, position, Quaternion.identity).gameObject;

            newGameobj.GetComponent<GetResults>().resultValue = GetResultValue();
            newGameobj.transform.rotation *= Quaternion.Euler(0.0f, 90f, 90.0f);
            newGameobj.transform.parent = resultParent.transform;
        }

        resultParent.SetActive(false);
    }

    private void GetResults()
    {
        int curLevel = GameController.Instance.level;
        if (GameController.Instance.educationalMode) return;

        Transform parent = GameObject.Find("ItemDropOff").transform;

        GameObject results = Instantiate(resultPreset, parent.position, parent.rotation, parent);
        results.transform.SetParent(parent);
        results.transform.localPosition = new Vector3(-0.075f, 0, 0);

        foreach (Transform child in resultPreset.transform)
        {
            child.GetComponent<GetResults>().resultValue = GetResultValue(curLevel);
        }

        results.SetActive(false);
    }
}
