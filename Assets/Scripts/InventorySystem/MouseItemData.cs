using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.Pool;

public class MouseItemData : MonoBehaviour
{
   public Image ItemSprite;
   public TMP_Text itemCount;
   public InventorySlot AssignedInventorySlot;
   void Awake(){
    ItemSprite.color = Color.clear;
    itemCount.text = "";
   }

   public void UpdateMouseSlot(InventorySlot invSlot){
      AssignedInventorySlot.AssignItem(invSlot);
      ItemSprite.sprite = invSlot.InventoryItemData.icon;
      itemCount.text = invSlot.StackSize.ToString();
      ItemSprite.color = Color.white;
   }

   void Update(){
      if(AssignedInventorySlot.InventoryItemData != null){
         transform.position = Input.mousePosition;
         if(Input.GetMouseButtonDown(0) && !IsPointerOverUIObject()){
            Debug.Log("pressed left-click");
            ClearSlot();
         }
      }
   }

   public void ClearSlot(){
      AssignedInventorySlot.ClearSlot();
      itemCount.text = "";
      ItemSprite.color = Color.clear;
      ItemSprite.sprite = null;
   }

   public static bool IsPointerOverUIObject(){
      PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
      eventDataCurrentPosition.position = Input.mousePosition;
      List<RaycastResult> results = new List<RaycastResult>();
      EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
      return results.Count > 0;
   }
}
