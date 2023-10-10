using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class InventoryDisplay : MonoBehaviour
{
    [SerializeField] MouseItemData mouseInventoryItem;
    protected InventorySystem inventorySystem;
    protected Dictionary<InventorySlot_UI, InventorySlot> slotDictionary;
    public InventorySystem InventorySystem => inventorySystem;
    public Dictionary<InventorySlot_UI, InventorySlot> SlotDictionary => slotDictionary;
    public abstract void AssignSlot(InventorySystem invToDisplay);

    protected virtual void Start(){
        
    }

    protected virtual void UpdateSlot(InventorySlot updatedSlot){
        foreach(var slot in SlotDictionary){
            if(slot.Value == updatedSlot){
                slot.Key.UpdateUISlot(updatedSlot);
            }
        }
    }

    public void SlotClicked(InventorySlot_UI clickedUISlot){
        Debug.Log("slot clicked");
        // clicked slot has an item and the mouse doesn't have an item - pick up that item.
        bool isLeftCtrlPressed = Input.GetKey(KeyCode.LeftControl);
        if(clickedUISlot.AssignedInventorySlot.InventoryItemData != null 
            && mouseInventoryItem.AssignedInventorySlot.InventoryItemData == null){

            if(isLeftCtrlPressed && clickedUISlot.AssignedInventorySlot.SplitStack(out InventorySlot halfStackSlot)){    
               mouseInventoryItem.UpdateMouseSlot(halfStackSlot);
               clickedUISlot.UpdateUISlot();
               return;
            }else{
                mouseInventoryItem.UpdateMouseSlot(clickedUISlot.AssignedInventorySlot);
                clickedUISlot.ClearSlot();
                return;
            }
            
           
        }

        if(clickedUISlot.AssignedInventorySlot.InventoryItemData == null 
            && mouseInventoryItem.AssignedInventorySlot.InventoryItemData != null){

            clickedUISlot.AssignedInventorySlot.AssignItem(mouseInventoryItem.AssignedInventorySlot);
            clickedUISlot.UpdateUISlot();
            mouseInventoryItem.ClearSlot();
            return;
        }

        if(clickedUISlot.AssignedInventorySlot.InventoryItemData != null 
            && mouseInventoryItem.AssignedInventorySlot.InventoryItemData != null){
            
            bool isSameItem = clickedUISlot.AssignedInventorySlot.InventoryItemData == mouseInventoryItem.AssignedInventorySlot.InventoryItemData; 
            if(isSameItem && clickedUISlot.AssignedInventorySlot.RoomLeftInStack(mouseInventoryItem.AssignedInventorySlot.StackSize))
            {
                clickedUISlot.AssignedInventorySlot.AssignItem(mouseInventoryItem.AssignedInventorySlot);
                clickedUISlot.UpdateUISlot();
                mouseInventoryItem.ClearSlot();
                return;

            }else if(isSameItem && 
                !clickedUISlot.AssignedInventorySlot.RoomLeftInStack(mouseInventoryItem.AssignedInventorySlot.StackSize, 
                out int leftInStack)
            ){
                if(leftInStack < 1){ //stack is f
                    SwapSlots(clickedUISlot);
                }else{
                    int remainingOnMouse = mouseInventoryItem.AssignedInventorySlot.StackSize - leftInStack;
                    clickedUISlot.AssignedInventorySlot.AddToStack(leftInStack);
                    clickedUISlot.UpdateUISlot();

                    InventorySlot newItem = new InventorySlot(mouseInventoryItem.AssignedInventorySlot.InventoryItemData, remainingOnMouse);
                    mouseInventoryItem.ClearSlot();
                    mouseInventoryItem.UpdateMouseSlot(newItem);
                    return;
                }
            }else if(!isSameItem){
                SwapSlots(clickedUISlot);
                return;
            }
        }
    }

    public void SwapSlots(InventorySlot_UI clickedUISlot){
        InventorySlot cloneSlot = new InventorySlot(mouseInventoryItem.AssignedInventorySlot.InventoryItemData, 
                                                    mouseInventoryItem.AssignedInventorySlot.StackSize);
        mouseInventoryItem.ClearSlot();

        mouseInventoryItem.UpdateMouseSlot(clickedUISlot.AssignedInventorySlot);

        clickedUISlot.ClearSlot();
        clickedUISlot.AssignedInventorySlot.AssignItem(cloneSlot);
        clickedUISlot.UpdateUISlot();
    }
}
