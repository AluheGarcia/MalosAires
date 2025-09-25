using UnityEngine;
[System.Serializable]
public class InventorySlot 
{
    private GameObject ItemPrefab;
    public bool HasItem;

    public GameObject itemPrefab => ItemPrefab;
    

    public InventorySlot(GameObject prefab)
    {
        ItemPrefab = prefab;
        HasItem = true;
    }
}
