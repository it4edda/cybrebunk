using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class UserInterfaceHealth : MonoBehaviour
{
    [SerializeField] RectTransform prefabToInstantiate;
    [SerializeField] RectTransform startPoint;
    [SerializeField] RectTransform endPoint;
    int                            maxHealth = 5; // Maximum health value

    readonly List<RectTransform> instantiatedElements = new();

    void UpdateUI()
    {
        int numElements = instantiatedElements.Count;

        if (numElements == 0) { return; }

        float spacingFactor    = maxHealth - 1;
        float spacingIncrement = Mathf.Abs(startPoint.anchoredPosition.x - endPoint.anchoredPosition.x) / spacingFactor;

        for (var i = 0; i < numElements; i++)
        {
            float newX = startPoint.anchoredPosition.x + spacingIncrement * i;

            instantiatedElements[i].anchoredPosition =
                new Vector3(newX, instantiatedElements[i].anchoredPosition.y, -i);
        }
    }

    public void ModifyHealth(int changeAmount) //ONLY VISUALS
    {
        switch (changeAmount)
        {
            // Reducing health
            case < 0 when instantiatedElements.Count <= 0:
                return;

            case < 0:
            {
                for (int i = 0; i < math.abs(changeAmount); i++)
                {
                    if (instantiatedElements.Count <= 0) return;
                    Destroy(instantiatedElements[^1].gameObject);
                    instantiatedElements.RemoveAt(instantiatedElements.Count - 1);
                }
                UpdateUI();
                break;
            }

            // Increasing health
            case > 0:
            {
                if (instantiatedElements.Count + changeAmount <= maxHealth)
                {
                    for (var i = 0; i < changeAmount; i++)
                    {
                        RectTransform newElement = Instantiate(prefabToInstantiate, transform);
                        instantiatedElements.Add(newElement);
                    }

                    UpdateUI();
                }

                break;
            }
        }
    }
    /*public void ModifyHealth(int changeAmount) //ONLY VISUALS
    {
        if (changeAmount < 0) // Reducing health
        {
            if (instantiatedElements.Count <= 0) return;

            for (int i = 0; i < math.abs(changeAmount); i++)
            {
                if (instantiatedElements.Count <= 0) return;
                Destroy(instantiatedElements[^1].gameObject);
                instantiatedElements.RemoveAt(instantiatedElements.Count - 1);
            }
            UpdateUI();
        }
        else if (changeAmount > 0) // Increasing health
        {
            if (instantiatedElements.Count + changeAmount <= maxHealth)
            {
                for (var i = 0; i < changeAmount; i++)
                {
                    RectTransform newElement = Instantiate(prefabToInstantiate, transform);
                    instantiatedElements.Add(newElement);
                }

                UpdateUI();
            }
        }
    }*/
    public void SetMaxHealth(int newMaxHealth)
    {
        //int i = newMaxHealth - maxHealth;
        maxHealth = newMaxHealth;
        ModifyHealth(maxHealth);          //i);

        UpdateUI();
    }
}
