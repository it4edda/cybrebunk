using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChampionSelect : MonoBehaviour
{
    [SerializeField] Transform[]  positions;
    [SerializeField] GameObject[] tarots;
    [SerializeField] GameObject[] selectedTarots;
    [SerializeField] List<string> leftInputKeys;
    [SerializeField] List<string> rightInputKeys;
    [SerializeField] KeyCode      continueInput;
    [SerializeField] int          selected;
    [SerializeField] float        swapHaste = 2;
    void Start()
    {
        for (var i = 0; i < selectedTarots.Length; i++)
        {
            int index = (selected + i) % tarots.Length;
            selectedTarots[i] = Instantiate(tarots[index]);
        }
    }
    void Update()
    {
        // ReSharper disable once ForeachCanBePartlyConvertedToQueryUsingAnotherGetEnumerator
        foreach (string i in leftInputKeys) { if (Input.GetKeyDown(i)) SelectL(); }
        // ReSharper disable once ForeachCanBePartlyConvertedToQueryUsingAnotherGetEnumerator
        foreach (string i in rightInputKeys) { if (Input.GetKeyDown(i)) SelectR(); }
        if (Input.GetKeyDown(continueInput)) LoadGame();

        for (var i = 0; i < selectedTarots.Length; i++)
        {
            selectedTarots[i].transform.position = Vector3.Lerp(selectedTarots[i].transform.position,
                                                                positions[i].position, swapHaste * Time.deltaTime);
        }
    }
    public void SelectL()
    {
        Destroy(selectedTarots[^1]); //Destroy last

        for (int i = selectedTarots.Length - 1; i > 0; i--) //move all upward
        {
            selectedTarots[i] = selectedTarots[i - 1];
        }

        selected          = (selected - 1 + tarots.Length) % tarots.Length; //decrement the selected index
        selectedTarots[0] = Instantiate(tarots[selected], positions[0].position, quaternion.identity); //spawn first
    }

    public void SelectR()
    {
        Destroy(selectedTarots[0]); // Destroy first

        for (var i = 0; i < selectedTarots.Length - 1; i++) // Move all downward
        {
            selectedTarots[i] = selectedTarots[i + 1];
        }

        selected           = (selected + 1) % tarots.Length; //increment the selected index
        selectedTarots[^1] = Instantiate(tarots[selected], positions[^1].position, quaternion.identity); // Spawn last
    }
    public void LoadGame() => SceneManager.LoadScene("Game");

}
