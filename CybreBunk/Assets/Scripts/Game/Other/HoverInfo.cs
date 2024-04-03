using System;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class HoverInfo : MonoBehaviour
{
    [SerializeField] CanvasGroup canvasGroup;
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] TextMeshProUGUI descriptionText;
    [SerializeField] Vector2 offset;
    void OnEnable()
    {
        ItemVisual.OnHover += DisplayInfo;
        ItemVisual.OnUnHover += HideInfo;
        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0;
    }

    void OnDisable()
    {
        ItemVisual.OnHover -= DisplayInfo;
        ItemVisual.OnUnHover -= HideInfo;
    }

    void DisplayInfo(ItemData item, PointerEventData pointerEventData, GameObject senderObject)
    {
        gameObject.transform.position = pointerEventData.position + offset;
        canvasGroup.alpha = 1;
        nameText.text = item.itemName;
        descriptionText.text = item.itemDescription;
    }

    void HideInfo(ItemData arg1, GameObject arg2)
    {
        canvasGroup.alpha = 0;
    }
}
