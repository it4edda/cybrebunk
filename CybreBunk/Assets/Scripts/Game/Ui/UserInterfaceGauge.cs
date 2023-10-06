using System;
using UnityEngine;
using UnityEngine.UI;

public class UserInterfaceGauge : MonoBehaviour
{
    [SerializeField] int maxValue;
    
    int                  currentValue;
    bool                 maxedGauge = false;
    
    Slider   slider;
    SatanicC satanicC;
    void Start()
    {
        slider   = GetComponent<Slider>();
        satanicC = FindObjectOfType<SatanicC>();
        SetMaxGaugeValue(maxValue);
    }
    public void SetMaxGaugeValue(int value) //use in start
    {
        maxValue     = value;
        slider.maxValue = maxValue;
    }

    public void UpdateGaugeSlider(int maxValue, int value)
    {
        SetMaxGaugeValue(maxValue);
        UpdateGaugeSlider(value);
    }
    public void UpdateGaugeSlider(int value)
    {
        currentValue += value;
        slider.value = currentValue <= maxValue ? currentValue + value : MaxGaugeTrigger();
        //ADD RED VIGNETTE?
    }
    int MaxGaugeTrigger()
    {
        Debug.Log("MaxTrigger");
        satanicC.ResetSatan();
        return maxValue;
    }
    public int MaxValue => maxValue;
}
