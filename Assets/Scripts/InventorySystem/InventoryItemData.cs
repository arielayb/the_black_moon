using UnityEngine;


[CreateAssetMenu]
public class InventoryItemData : ScriptableObject {
    public string displayName;
    int id;
    public GameObject model;
    [TextArea]
    public string description;
    public Sprite icon;
    public int startingAmmo;
    public int maxStackSize;
}
