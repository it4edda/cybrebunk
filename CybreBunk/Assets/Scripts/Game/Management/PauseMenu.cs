using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] Canvas     canvas;
    [SerializeField] GameObject currentTarot;
    Sprite                      currentChamp;
    float                       timeScaleAtStart = 1;
    void Start()
    {
        DiosBestFriend(false);
        currentChamp.GetComponent<Image>().sprite = PlayerManager.selectedCard?.tarotCard.GetComponentInChildren<Sprite>();
        
        //timeScaleAtStart = Time.timeScale;
    }
    public void DiosBestFriend(bool freeze)
    {
        //IsPaused       = freeze;
        Time.timeScale = freeze ? 0 : timeScaleAtStart;
        canvas.enabled = freeze;
    }
    public bool IsPaused
    {
        get => IsPaused;
        set => IsPaused = value;
    }

    public void LoadMenu() => SceneManager.LoadScene("Deity");
}
