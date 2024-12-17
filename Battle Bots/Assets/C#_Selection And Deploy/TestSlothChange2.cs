using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotChange : MonoBehaviour
{
    //slot1 for the panel2
   public GameObject Panel2_Slot1;// slot1 will have the index of 0 
    //slot2 for the panel2
   public GameObject Panel2_Slot2;// slot2 will have the index of 1

   private int SelectedBotIndex = 0;// setting the visibility through index 

   void Start() // by default setting the slot 1 active
   {
     Panel2_Slot1.SetActive(true);
     Panel2_Slot2.SetActive(false);
   }

   void Update()
   {
      HandleInput();
   }

   void HandleInput()
   {
     //Selection input is my left and right movement for slot change
      float SelectionInput = Input.GetAxisRaw("Horizontal2");

      if(SelectionInput>0.1f && SelectedBotIndex == 0)
      {
        SelectedBotIndex =1;
         Setvisibilty();
      }
      if(SelectionInput< -0.1f && SelectedBotIndex ==1)
      {
        SelectedBotIndex =0;
        Setvisibilty();
      }


   }

   void Setvisibilty()
   {
       if(SelectedBotIndex==0)
       {
        Panel2_Slot1.SetActive(true);
        Panel2_Slot2.SetActive(false);
       }
       if(SelectedBotIndex==1)
       {
        Panel2_Slot1.SetActive(false);
        Panel2_Slot2.SetActive(true);
       }
   }
}
