using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot_UI : MonoBehaviour
{
    [SerializeField] private Image itemSprite;
    [SerializeField] private TMP_Text itemCount;
    [SerializeField] private InventorySlot assignedInventorySlot;

    private Button button;

    public InventorySlot AssignedInventorySlot => assignedInventorySlot;
    public InventoryDisplay ParentDisplay {get; private set;}

    private void Awake(){
       ClearSlot();

        button = GetComponent<Button>();

        button?.onClick.AddListener(OnUISlotClick);

        ParentDisplay = transform.parent.GetComponent<InventoryDisplay>();
    }

    public void Init(InventorySlot slot){
        assignedInventorySlot = slot;
        UpdateUISlot(slot);
    }

    public void UpdateUISlot(InventorySlot slot){
        if(slot.InventoryItemData != null){
            itemSprite.sprite = slot.InventoryItemData.icon;
            itemSprite.color = Color.white;
        }else{
            ClearSlot();
        }

        if(slot.StackSize > 1){
            itemCount.text = slot.StackSize.ToString();
        }else{
            itemCount.text = "";
        }
    }

    public void UpdateUISlot(){
        if(assignedInventorySlot != null){
            UpdateUISlot(assignedInventorySlot);
        }
        
    }

    public void ClearSlot(){
        assignedInventorySlot?.ClearSlot();
        itemSprite.sprite = null;
        itemSprite.color = Color.clear;
        itemCount.text = "";
    }

    public void OnUISlotClick(){
        ParentDisplay?.SlotClicked(this);

    }
}
