using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class ItemPickUp : MonoBehaviour {
    public float pickUpRadius = 1f;

    public InventoryItemData ItemData;

    private SphereCollider myCollider;

    void Awake(){
        myCollider = GetComponent<SphereCollider>();
        myCollider.isTrigger = true;
        myCollider.radius = pickUpRadius;
    }

    void OnTriggerEnter(Collider other){
        var inventory = other.transform.GetComponent<PlayerInventoryHolder>();
        if(!inventory){
            return;
        }

        if(inventory.AddToInventory(ItemData, 1)){
            Destroy(this.gameObject);
        }
    }

}