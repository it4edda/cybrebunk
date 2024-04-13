using TMPro;
using Unity.Mathematics;
using UnityEngine;

public class DamageNumberManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI damageNumbers;
    Animator                         damageNumberAnimator;
    Transform                        transformDN;

    void Start()
    {
        transformDN          = GameObject.Find("DamageNumberCanvas").GetComponent<Transform>();
    }

    public void DamageNumbers(int numberToDisplay, Transform placeToDisplay)
    {
        damageNumbers.text = numberToDisplay.ToString();
        var b = Instantiate(damageNumbers, FindObjectOfType<Camera>().WorldToScreenPoint(placeToDisplay.position), quaternion.identity, transformDN);
        damageNumberAnimator = b.GetComponent<Animator>();
        Destroy(b, damageNumberAnimator.GetCurrentAnimatorStateInfo(0).length);
    }
}
