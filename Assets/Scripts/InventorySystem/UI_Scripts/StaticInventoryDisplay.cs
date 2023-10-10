using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class StaticInventoryDisplay : InventoryDisplay {

    [SerializeField] private InventoryHolder inventoryHolder;
    [SerializeField] private InventorySlot_UI[] slots;


    protected override void Start(){
        base.Start();

        if(inventoryHolder != null){
            inventorySystem = inventoryHolder.PrimaryInventorySystem;
            inventorySystem.OnInventorySlotChanged += UpdateSlot;
        }else{
            Debug.LogWarning($"No inventory assigned to {this.gameObject}");
        }

        AssignSlot(inventorySystem);
    }

    public override void AssignSlot(InventorySystem invToDisplay){
        slotDictionary = new Dictionary<InventorySlot_UI, InventorySlot>();

        for(int i = 0; i < inventorySystem.InventorySize; i++){
            slotDictionary.Add(slots[i], inventorySystem.InventorySlots[i]);
            slots[i].Init(inventorySystem.InventorySlots[i]);
        }
    }
}