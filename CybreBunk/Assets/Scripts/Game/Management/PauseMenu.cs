using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public static event Action<bool> pausing; 
    
    [Header("Pause Menu")]
    [SerializeField] Canvas     mainCanvas;
    [SerializeField] GameObject currentTarot;
    [SerializeField] GameObject itemList;
    [SerializeField] ItemVisual itemVisual;
    
    [Header("Settings")]
    [SerializeField] Canvas settingsCanvas;
    [SerializeField] Slider distortionSlider;

    [Header("Sound")]
    [SerializeField] AudioClip declineSound;
    [SerializeField] AudioClip acceptSound;
    AudioSource audioSource;
    
    bool                    inSettingsMenu = false;
    
    float                       timeScaleAtStart = 1;
    bool                        isPaused;

    Volume volume;

    public Queue<ItemVisual> visualsQueue = new();
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        DiosBestFriend(false);
        settingsCanvas.enabled                    = false;
        
        currentTarot.GetComponent<Image>().sprite = PlayerManager.selectedCard?.CurrentCard;
        
    }
    public void DiosBestFriend(bool freeze)
    {
        if (isPaused && !mainCanvas.enabled)
        {
            return;
        }
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
        pausing?.Invoke(freeze);
    }

    void SetUpItemWiew()
    {
        while (visualsQueue.Count < PlayerInventory.instance.items.Count)
        {
            ItemVisual currentItemVisual = Instantiate(itemVisual, itemList.transform);
            visualsQueue.Enqueue(currentItemVisual);
        }

        foreach (ItemData item in PlayerInventory.instance.items)
        {
            //TODO turn this into a object pool
            ItemVisual currentItemVisual = visualsQueue.Dequeue();
            currentItemVisual.gameObject.SetActive(true);
            currentItemVisual.SetUpVisual(item);
        }
    }

    public void TimeFreeze(bool freeze)
    {
        audioSource.PlayOneShot(freeze ? acceptSound: declineSound);
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
        audioSource.PlayOneShot(declineSound);
        Time.timeScale = timeScaleAtStart;
        SceneManager.LoadScene("Deity");
    }

    public void SettingsToggle(bool visibility)
    {
        audioSource.PlayOneShot(visibility ? acceptSound : declineSound);
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
