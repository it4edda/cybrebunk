using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class ChampionSelect : MonoBehaviour
{
   [SerializeField] Transform[]  positions;
   [SerializeField] GameObject[] tarots;
   [SerializeField] GameObject[] selectedTarots;
   [SerializeField] int          selected  = 0;
   [SerializeField] float        swapHaste = 2;
   void Update()
   {
      if (Input.GetKeyDown(KeyCode.RightArrow)) SelectRight();
      if (Input.GetKeyDown(KeyCode.LeftArrow)) return;
      for (int i = 0; i < selectedTarots.Length; i++)
      {
         selectedTarots[i].transform.position = Vector3.Lerp(selectedTarots[i].transform.position, positions[i].position, swapHaste);
      }
   }
   void SelectRight()
   {
      //Destroy last
      Destroy(selectedTarots[^1]);
      
      //move all upward
      for (int i = 0; i < selectedTarots.Length; i++)
      {
         selectedTarots[i++] = selectedTarots[i];
      }
      
      //spawn first
      selectedTarots[0] = Instantiate(tarots[0]);
   }

}
