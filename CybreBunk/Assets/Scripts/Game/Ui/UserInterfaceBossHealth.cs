using System;
using UnityEngine;
using UnityEngine.UI;

public class UserInterfaceBossHealth : MonoBehaviour
{
    Slider slider;
    void Start()
    {
        slider = GetComponent<Slider>();
        slider.gameObject.SetActive(false);
    }

    public void SetValues(int max, int min)
    {
        slider.maxValue = max;
        slider.minValue = min;
    }
    public void UpdateHealthValue(float value)
    {
        slider.value = value;
    }
}
