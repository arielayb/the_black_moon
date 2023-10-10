using UnityEngine;
using UnityEngine.Events;

public class PlayerInventoryHolder : InventoryHolder {
    
    [SerializeField] protected int secondryInventorySize;
    [SerializeField] protected InventorySystem secondryInventorySystem;

    public InventorySystem SecondryInventorySystem => secondryInventorySystem;
    public static UnityAction<InventorySystem> OnPlayerInventoryDisplayRequested;

    protected override void Awake(){
        base.Awake();

        secondryInventorySystem = new InventorySystem(secondryInventorySize);
    }

    void Update(){
        if(Input.GetKeyDown(KeyCode.I)){
            OnPlayerInventoryDisplayRequested?.Invoke(secondryInventorySystem);
        }
    }

    public bool AddToInventory(InventoryItemData data, int amount){
        if(primaryInventorySystem.AddToInventory(data, amount)){
            return true;
        }else if(secondryInventorySystem.AddToInventory(data, amount)){
            return true;
            
        }

        return false;
    }
}