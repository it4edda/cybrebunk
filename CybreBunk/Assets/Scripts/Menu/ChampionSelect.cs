using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChampionSelect : MonoBehaviour
{
   [SerializeField] Transform[]  positions;
   [SerializeField] GameObject[] tarots;
   [SerializeField] GameObject[] selectedTarots;
   [SerializeField] int          selected  = 0;
   [SerializeField] float        swapHaste = 2;
   void Update()
   {
      if (Input.GetKeyDown(KeyCode.LeftArrow)) SelectRight();
      if (Input.GetKeyDown(KeyCode.RightArrow)) SelectLeft();
      if (Input.GetKeyDown(KeyCode.Space)) LoadGame();
      for (int i = 0; i < selectedTarots.Length; i++)
      {
         selectedTarots[i].transform.position = Vector3.Lerp(selectedTarots[i].transform.position, positions[i].position, swapHaste * Time.deltaTime);
      }
   }
   public void SelectRight()
   {
      Destroy(selectedTarots[^1]);                        //Destroy last
      for (int i = selectedTarots.Length - 1; i > 0; i--) //move all upward
      {
         selectedTarots[i] = selectedTarots[i - 1];
      }
      selected          = (selected - 1 + tarots.Length) % tarots.Length; //decrement the selected index
      selectedTarots[0] = Instantiate(tarots[selected], positions[0].position, quaternion.identity);                  //spawn first
   }

   public void SelectLeft()
   {
      Destroy(selectedTarots[0]);                         // Destroy first
      for (int i = 0; i < selectedTarots.Length - 1; i++) // Move all downward
      {
         selectedTarots[i] = selectedTarots[i + 1];
      }
      selected           = (selected + 1) % tarots.Length; //increment the selected index
      selectedTarots[^1] = Instantiate(tarots[selected], positions[^1].position, quaternion.identity);  // Spawn last
   }
   public void LoadGame() => SceneManager.LoadScene("Game");
   
}
