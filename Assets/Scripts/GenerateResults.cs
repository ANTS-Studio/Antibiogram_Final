using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateResults : MonoBehaviour
{
    public Transform resultCircle;

    private List<Vector3> positions;
    private bool isGenerated = false;

    public void GenerateAntibiogramResults()
    {
        if (isGenerated) return;
        isGenerated = true;

        ClearAntibiogram();
        GenerateResultCircles();
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

    private int GetResultValue()
    {
        return Random.Range(5, 15);
    }

    private void GenerateResultCircles()
    {
        GameObject parentContainer = GameObject.Find("ItemDropOff");
        GameObject resultParent = new GameObject();
        resultParent.name = "Results";
        resultParent.transform.SetParent(parentContainer.transform, false);

        foreach (Vector3 position in positions)
        {
            int circleSize = Random.Range(5, 5);
            resultCircle.localScale = new Vector3(1 * circleSize, 0.125f, 1 * circleSize);

            GameObject newGameobj = Instantiate(resultCircle, position, Quaternion.identity).gameObject;

            newGameobj.GetComponent<GetResults>().resultValue = GetResultValue();
            newGameobj.transform.rotation *= Quaternion.Euler(0.0f, -35.0f, 90.0f);
            newGameobj.transform.parent = resultParent.transform;
        }

        resultParent.SetActive(false);
    }
}
