using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] Canvas     mainCanvas;
    [SerializeField] GameObject currentTarot;
    
    [SerializeField] Canvas settingsCanvas;
    [Header("Settings")]
    
    [SerializeField] Slider distortionSlider;
    float distoritionValue = 0;
    //0, normal, max
    
    bool                    inSettingsMenu = false;
    
    float                       timeScaleAtStart = 1;
    bool                        isPaused;

    Volume volume;
    void Start()
    {
        DiosBestFriend(false);
        settingsCanvas.enabled                    = false;
        
        currentTarot.GetComponent<Image>().sprite = PlayerManager.selectedCard?.CurrentCard;
        
    }
    public void DiosBestFriend(bool freeze)
    {
        if (inSettingsMenu)
        {
            SettingsToggle(false);
            return;
        }

        ;
        TimeFreeze(freeze);
        mainCanvas.enabled = freeze;
    }

    public void TimeFreeze(bool freeze)
    {
        IsPaused = freeze;
        Time.timeScale = freeze ? 0 : timeScaleAtStart;
    }

    public bool IsPaused
    {
        get => isPaused;
        private set => isPaused = value;
    }

    public void LoadMenu()
    {
        Time.timeScale = timeScaleAtStart;
        SceneManager.LoadScene("Deity");
    }

    public void SettingsToggle(bool visibility)
    {
        inSettingsMenu         = visibility;
        settingsCanvas.enabled = visibility;
        mainCanvas.enabled     = !visibility;

        if (!visibility) SetSettings();
    }
    void SetSettings()
    {
//        distortionSlider.value = volume.
    }
}
