using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] Canvas     canvas;
    [SerializeField] GameObject currentTarot;
    float                       timeScaleAtStart = 1;
    bool                        isPaused;
    void Start()
    {
        DiosBestFriend(false);

        currentTarot.GetComponent<Image>().sprite = PlayerManager.selectedCard?.CurrentCard;
    }
    public void DiosBestFriend(bool freeze)
    {
        IsPaused       = freeze;
        Time.timeScale = freeze ? 0 : timeScaleAtStart;
        canvas.enabled = freeze;
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
}
