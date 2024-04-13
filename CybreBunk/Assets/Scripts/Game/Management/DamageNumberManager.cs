using TMPro;
using Unity.Mathematics;
using UnityEngine;

public class DamageNumberManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI damageNumbers;
    Animator damageNumberAnimator;

    void Start()
    {
        damageNumberAnimator = GetComponent<Animator>();
    }

    public void DamageNumbers(int numberToDisplay, Transform placeToDisplay)
    {
        var a = GameObject.Find("DamageNumberCanvas").GetComponent<Transform>();
        damageNumbers.text = numberToDisplay.ToString();
        var b = Instantiate(damageNumbers, FindObjectOfType<Camera>().WorldToScreenPoint(placeToDisplay.position), quaternion.identity, a);
        Destroy(b, damageNumberAnimator.GetCurrentAnimatorStateInfo(0).length);
    }
}
