using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] Canvas     mainCanvas;
    [SerializeField] GameObject currentTarot;
    
    [Header("Settings")]
    [SerializeField] Canvas settingsCanvas;
    //[SerializeField] Slider distortionSlider;
    bool                    inSettingsMenu = false;
    
    float                       timeScaleAtStart = 1;
    bool                        isPaused;
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
        IsPaused       = freeze;
        Time.timeScale = freeze ? 0 : timeScaleAtStart;
        mainCanvas.enabled = freeze;
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
        
    }
}
