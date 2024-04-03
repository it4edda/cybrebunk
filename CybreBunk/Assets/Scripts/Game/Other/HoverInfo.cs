using UnityEngine;
using TMPro;

public class HoverInfo : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;

    public void SetText(string newText)
    {
        text.text = newText;
    }
    void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = Camera.main.transform.position.z + Camera.main.nearClipPlane;
        transform.position = mousePosition;
    }
}
