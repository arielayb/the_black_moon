using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class DynamicInventoryDisplay : InventoryDisplay{
    [SerializeField] protected InventorySlot_UI slotPrefab;


    protected override void Start(){
        base.Start();
    }

    public void RefreshDynamicIvnentory(InventorySystem invToDisplay){
        ClearSlots();
        inventorySystem = invToDisplay;
        if(inventorySystem != null){
            inventorySystem.OnInventorySlotChanged += UpdateSlot;
        }

        AssignSlot(invToDisplay);
    }

    public override void AssignSlot(InventorySystem invToDisplay){
        ClearSlots();

        slotDictionary = new Dictionary<InventorySlot_UI, InventorySlot>();

        if(invToDisplay == null){
            return;
        }

        for(int i = 0; i < invToDisplay.InventorySize; i++){
            var uiSlot = Instantiate(slotPrefab, transform);
            slotDictionary.Add(uiSlot, invToDisplay.InventorySlots[i]);
            uiSlot.Init(invToDisplay.InventorySlots[i]);
            uiSlot.UpdateUISlot();
        }
    }

    void ClearSlots(){
        foreach(var item in transform.Cast<Transform>()){
            Destroy(item.gameObject);
        }

        if(slotDictionary != null){
            slotDictionary.Clear();
        }
    }

    private void OnDisable(){
        if(inventorySystem != null){
            inventorySystem.OnInventorySlotChanged -= UpdateSlot;
        }
    }

}