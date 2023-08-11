using UnityEngine;
using UnityEngine.UI;

public class SpaceUIElementsEvenly : MonoBehaviour
{
    public RectTransform[] elementsToSpace;
    public RectTransform   startPoint;
    public RectTransform   endPoint;

    void Start()
    {
        SpaceUIElements();
    }
    void SpaceUIElements()
    {
        int numElements = elementsToSpace.Length;
        if (numElements == 0)
        {
            Debug.LogWarning("No elements to space.");
            return;
        }

        float totalDistance        = Mathf.Abs(endPoint.anchoredPosition.x - startPoint.anchoredPosition.x);

        for (int i = 0; i < numElements; i++)
        {
            float t    = i / (float)(numElements - 1);
            float newX = Mathf.Lerp(startPoint.anchoredPosition.x, endPoint.anchoredPosition.x, t);
            elementsToSpace[i].anchoredPosition = new Vector3(newX, elementsToSpace[i].anchoredPosition.y, -i);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawLine(startPoint.anchoredPosition, endPoint.anchoredPosition);
    }
}
