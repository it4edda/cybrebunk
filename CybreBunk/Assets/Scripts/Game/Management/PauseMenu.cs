using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [Header("Pause Menu")]
    [SerializeField] Canvas     mainCanvas;
    [SerializeField] GameObject currentTarot;
    [SerializeField] GameObject itemList;
    [SerializeField] GameObject itemVisual;
    
    [Header("Settings")]
    [SerializeField] Canvas settingsCanvas;
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
        
        TimeFreeze(freeze);
        mainCanvas.enabled = freeze;
        if (freeze)
        {
            SetUpItemWiew();
        }
    }

    void SetUpItemWiew()
    {
        foreach (ItemData item in PlayerInventory.instance.items)
        {
            //TODO turn this into a object pool
            GameObject currentItemVisual = Instantiate(itemVisual, itemList.transform);
            currentItemVisual.GetComponent<ItemVisual>().SetUpVisual(item);
        }
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
