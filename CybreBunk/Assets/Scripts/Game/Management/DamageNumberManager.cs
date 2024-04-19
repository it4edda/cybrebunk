using TMPro;
using Unity.Mathematics;
using UnityEngine;

public class DamageNumberManager : MonoBehaviour
{
   [SerializeField] TextMeshProUGUI damageNumberPrefab;
       Transform damageNumberCanvas;
   
       void Start()
       {
           damageNumberCanvas = GameObject.Find("DamageNumberCanvas").GetComponent<Transform>();
       }

       public void ShowDamageNumber(int numberToDisplay, Transform placeToDisplay)
       {
           TextMeshProUGUI damageNumber = Instantiate(damageNumberPrefab, damageNumberCanvas);
           
           damageNumber.text = numberToDisplay.ToString();
           
           Vector3 screenPosition = Camera.main.WorldToScreenPoint(placeToDisplay.position);

           damageNumber.rectTransform.position = screenPosition;

           Animator a = GetComponent<Animator>();
           Destroy(damageNumber.gameObject, a.GetCurrentAnimatorStateInfo(0).length);
       }
}
