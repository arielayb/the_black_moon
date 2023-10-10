using UnityEngine;

[System.Serializable]
public class InventorySlot{
    [SerializeField] private InventoryItemData itemData;
    [SerializeField] private int stackSize;
    
    public InventoryItemData InventoryItemData => itemData;
    public int StackSize => stackSize;
    
    public InventorySlot(InventoryItemData source, int amount){
        itemData = source;
        stackSize = amount;
    }

    public InventorySlot(){
        ClearSlot();
    }

    public void ClearSlot(){
        itemData = null;
        stackSize = -1;
    }

    public void UpdateInventorySlot(InventoryItemData data, int amount){
        itemData = data;
        stackSize = amount;
    }

    public bool RoomLeftInStack(int amountToAdd, out int amountRemaining){
        amountRemaining = itemData.maxStackSize - stackSize;
        return RoomLeftInStack(amountToAdd);
    }

    public bool RoomLeftInStack(int amountToAdd){
        if(stackSize + amountToAdd <= itemData.maxStackSize){
            return true;
        }else{
            return false;
        }
    }

    public void AddToStack(int amout){
        stackSize += amout;

    }

    public void RemoveFromStack(int amount){
        stackSize -= amount;
    }

    public void AssignItem(InventorySlot invSlot){
        if(itemData == invSlot.InventoryItemData){
            AddToStack(invSlot.stackSize);
        }else{
            itemData = invSlot.InventoryItemData;
            stackSize = 0;
            AddToStack(invSlot.stackSize);
        }
    }

    public bool SplitStack(out InventorySlot splitStack){
        if(stackSize <= 1){
            splitStack = null;
            return false;
        }
        int halfStack = Mathf.RoundToInt(stackSize/2);
        RemoveFromStack(halfStack);
        splitStack = new InventorySlot(itemData, halfStack);

        return true;
    }
}