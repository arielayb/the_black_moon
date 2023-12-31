using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;


[System.Serializable]
public class InventorySystem {
    [SerializeField] private List<InventorySlot>  inventorySlots;

    public int InventorySize => inventorySlots.Count;

    public List<InventorySlot> InventorySlots => inventorySlots;

    public UnityAction<InventorySlot> OnInventorySlotChanged;

    public InventorySystem(int size){
        inventorySlots = new List<InventorySlot>(size);

        for(int i = 0; i < size; i++){
            inventorySlots.Add(new InventorySlot());
        }
    }

    public bool AddToInventory(InventoryItemData itemToAdd, int amountToAdd){
        // check if item exists in inventory
        if(ContainsItem(itemToAdd, out List<InventorySlot> invSlot)){
            foreach(var slot in invSlot){
                if(slot.RoomLeftInStack(amountToAdd)){
                    slot.AddToStack(amountToAdd);
                    OnInventorySlotChanged?.Invoke(slot);
                    return true;
                }
            }
        }
        
        if(HasFreeSLot(out InventorySlot freeSlot)){
            freeSlot.UpdateInventorySlot(itemToAdd, amountToAdd);
            OnInventorySlotChanged?.Invoke(freeSlot);
            return true;
        }

        return false;
    }

    public bool ContainsItem(InventoryItemData itemToAdd, out List<InventorySlot> invSlot){
        invSlot = InventorySlots.Where(i => i.InventoryItemData == itemToAdd).ToList();

        return invSlot.Count > 1 ? true : false;
    }

    public bool HasFreeSLot(out InventorySlot freeSlot){
        freeSlot = InventorySlots.FirstOrDefault(i => i.InventoryItemData == null);
        return freeSlot == null ? false : true;
    }
}


