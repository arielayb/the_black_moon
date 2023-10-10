using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InventoryUIController : MonoBehaviour
{
    public DynamicInventoryDisplay chestPanel;
    public DynamicInventoryDisplay playerInventoryPanel;

    void OnEnable(){
        InventoryHolder.OnDynamicInventoryDisplayRequested += DisplayInventory;
        PlayerInventoryHolder.OnPlayerInventoryDisplayRequested += DisplayPlayerInventory;
    }

    private void OnDisable() {
        InventoryHolder.OnDynamicInventoryDisplayRequested -= DisplayInventory;
        PlayerInventoryHolder.OnPlayerInventoryDisplayRequested -= DisplayPlayerInventory;
    }

    void Awake(){
        chestPanel.gameObject.SetActive(false);
        playerInventoryPanel.gameObject.SetActive(false);
    }

    void DisplayInventory(InventorySystem invToDisplay){
        chestPanel.gameObject.SetActive(true);
        chestPanel.RefreshDynamicIvnentory(invToDisplay);

    }

    void DisplayPlayerInventory(InventorySystem invToDisplay){
        playerInventoryPanel.gameObject.SetActive(true);
        playerInventoryPanel.RefreshDynamicIvnentory(invToDisplay);

    }

    // Update is called once per frame
    void Update()
    {
        if(chestPanel.gameObject.activeInHierarchy && Input.GetKeyDown(KeyCode.Escape)){
            chestPanel.gameObject.SetActive(false);
        }

        if(playerInventoryPanel.gameObject.activeInHierarchy && Input.GetKeyDown(KeyCode.Escape)){
            playerInventoryPanel.gameObject.SetActive(false);
        }
    }
}
