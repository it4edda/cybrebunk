using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;

public class ChampionSelect : MonoBehaviour
{
    [SerializeField] Transform[] positions;
    [SerializeField] TMP_Text    descriptionText;
    //[SerializeField] TarotData  //Why didnt i just do this?
    [SerializeField] ScriptableObject[] tarots;
    [SerializeField] GameObject[]       selectedTarots;
    [SerializeField] List<string>       leftInputKeys;
    [SerializeField] List<string>       rightInputKeys;
    [SerializeField] KeyCode            continueInput;
    [SerializeField] int                selected;
    [SerializeField] float              swapHaste = 2;
    [SerializeField] bool               hideCards = true; //DONT USE THIS
    [SerializeField] Vector2            hiddenPosition;

    [Header("OtherButtons")]
    [SerializeField] Button continueButton;
    [SerializeField] Transform titleObject;
    [SerializeField] Vector2   titlePos1;
    [SerializeField] Vector2   titlePos2;
    [SerializeField] float     scaleHaste;
    [SerializeField] float     titleScale1;
    [SerializeField] float     titleScale2;
    [SerializeField] string    descriptionWhileHidden;
    [SerializeField] Vector2   descriptionBoxPos1; //= -112.4f;
    [SerializeField] Vector2   descriptionBoxPos2;
    [SerializeField] Transform quitButton;
    [SerializeField] Vector2   quitBoxPos1;
    [SerializeField] Vector2   quitBoxPos2;

     [Header("OtherOther")]
    [SerializeField] SceneTransitions transitions;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip   bloopSound;
    [SerializeField] AudioClip   acceptInput;
    [SerializeField] AudioClip   declineInput;
    bool                         anyKeyToStartTimed;
    Camera cam;
    
    void Start()
    {
        for (var i = 0; i < selectedTarots.Length; i++)
        {
            int index = (selected + i) % tarots.Length;
            //selectedTarots[i]                                       = Instantiate((tarots[index] as TarotData)?.CurrentCard);
            selectedTarots[i]                                       = Instantiate(new GameObject(), hiddenPosition, quaternion.identity);
            selectedTarots[i].AddComponent<SpriteRenderer>().sprite = (tarots[index] as TarotData)?.CurrentCard;
        }

        CardStatus(true);
    }
    
    void Update()
    {
        if (!hideCards)
        {
            foreach (string i in leftInputKeys)  if (Input.GetKeyDown(i)) SelectL();
            foreach (string i in rightInputKeys) if (Input.GetKeyDown(i)) SelectR();
            if (Input.GetKeyDown(continueInput) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.E)) LoadGame();
        }
        else if (Input.anyKey)
        {
            audioSource.PlayOneShot(bloopSound);
            
            StartCoroutine(WaitASecond());
            hideCards          = false;
            anyKeyToStartTimed = true;
            UpdateTarotDescriptionText();
        }

        for (var i = 0; i < selectedTarots.Length; i++)
        {
            selectedTarots[i].transform.position = 
                Vector3.Lerp(selectedTarots[i].transform.position, hideCards ? hiddenPosition : positions[i].position, swapHaste * Time.deltaTime);
        }

        Vector2 titleScale = new Vector2(hideCards ? titleScale1 : titleScale2, hideCards ? titleScale1 : titleScale2);
        titleObject.localScale = Vector2.Lerp(titleObject.localScale, titleScale,scaleHaste  * Time.deltaTime);
        
        titleObject.position   = Vector3.Lerp(titleObject.position, hideCards ? titlePos1 : titlePos2, swapHaste * Time.deltaTime);
        
        StartCoroutine(WaitASecond2());
        if (!anyKeyToStartTimed) return;
        
        descriptionText.rectTransform.parent.position = 
            Vector3.Lerp(descriptionText.rectTransform.parent.position, hideCards ? descriptionBoxPos2 : descriptionBoxPos1, swapHaste * Time.deltaTime );
        
        quitButton.position = 
            Vector3.Lerp(quitButton.position, hideCards ? quitBoxPos2 : quitBoxPos1, swapHaste * Time.deltaTime );
    }
#region shitty ienumer
    IEnumerator WaitASecond( )
    {
        yield return new WaitForSeconds(1);
        continueButton.interactable = true;
        //yield return value                = true;
    }
    IEnumerator WaitASecond2()
    {
        yield return new WaitForSeconds(1);
        anyKeyToStartTimed = true;
    }
#endregion
    
    void CardStatus(bool isHidden)
    {
        hideCards            = isHidden;
        descriptionText.text = isHidden ? descriptionWhileHidden : (tarots[selected] as TarotData)?.CurrentDescription;
    }
    
    public void SelectL()
    {
        audioSource.PlayOneShot(bloopSound);
        Destroy(selectedTarots[^1]);                        //Destroy last
        for (int i = selectedTarots.Length - 1; i > 0; i--) //move all upward
        {
            selectedTarots[i] = selectedTarots[i - 1];
        }
        selected          = (selected - 1 + tarots.Length) % tarots.Length;                                                      //decrement the selected index
        //selectedTarots[0] = Instantiate((tarots[selected] as TarotData)?.CurrentCard, positions[0].position, quaternion.identity); //spawn first

        selectedTarots[0]                                       = Instantiate(new GameObject(), positions[0].position, quaternion.identity);
        //alexgråt        //selectedTarots[0]                                       = new GameObject("tar", typeof(SpriteRenderer));
        Debug.Log("Fix this duplicate game-object problem whatever");
        selectedTarots[0].AddComponent<SpriteRenderer>().sprite = (tarots[selected] as TarotData)?.CurrentCard;
        
        UpdateTarotDescriptionText();
    }

    public void SelectR()
    {
        audioSource.PlayOneShot(bloopSound);
        Destroy(selectedTarots[0]);                         // Destroy first
        for (var i = 0; i < selectedTarots.Length - 1; i++) // Move all downward
        {
            selectedTarots[i] = selectedTarots[i + 1];
        }
        selected           = (selected + 1) % tarots.Length;                                                                       //increment the selected index
        //selectedTarots[^1] = Instantiate((tarots[selected] as TarotData)?.CurrentCard, positions[^1].position, quaternion.identity); // Spawn last
        
        selectedTarots[^1]                                       = Instantiate(new GameObject(), positions[^1].position, quaternion.identity);
        selectedTarots[^1].AddComponent<SpriteRenderer>().sprite = (tarots[selected] as TarotData)?.CurrentCard;
        
        UpdateTarotDescriptionText();
    }
    void UpdateTarotDescriptionText()
    {
        string tarotDescription = (tarots[selected] as TarotData)?.CurrentDescription;
        descriptionText.text = tarotDescription;
    }
    public void LoadGame()
    {
        var savedTarot = tarots[selected] as TarotData;

        if (!savedTarot.isPlayable)
        {
            audioSource.PlayOneShot(declineInput);
            return;
        }
        audioSource.PlayOneShot(acceptInput);
        PlayerManager.selectedCard = savedTarot;
        //SceneManager.LoadScene("Game");
        StartCoroutine(transitions.Transition("Game"));
    }
    public void QuitGame()
    {
        Debug.Log("HA");
        Application.Quit();
    }
    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(descriptionBoxPos1, 40);
        Gizmos.DrawWireSphere(descriptionBoxPos2, 40);
        Gizmos.DrawWireSphere(titlePos1,          1);
        Gizmos.DrawWireSphere(titlePos2,          1);
        Gizmos.DrawWireSphere(quitBoxPos1,        40);
        Gizmos.DrawWireSphere(quitBoxPos2,        40);
    }

}
