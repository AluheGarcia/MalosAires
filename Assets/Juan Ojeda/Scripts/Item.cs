using JetBrains.Annotations;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public abstract class Item : MonoBehaviour
{
    [SerializeField] protected ItemData _itemData;
    public ItemData itemData => _itemData;
    [SerializeField] protected Rigidbody ItemRb;
    
    protected bool PlayerInRange = false;
    protected InventoryBehaviour inventory;   
    public InventoryBehaviour inv => inventory;    

    public GameObject itemPrefab => itemData.ItemPrefab;
    public Sprite itemSprite => itemData.ItemSprite;
    public string itemName => itemData.ItemName;
    public KeyCode assignKey => itemData.AssignKey;




    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerInRange = true;
            inventory = other.GetComponent<InventoryBehaviour>();
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerInRange = false;
            inventory = null;
        }
    }

    public void SetInventory(InventoryBehaviour Inv)
    {
        inventory = Inv;
    }

    public void SetPrefab (GameObject prefab)
    {
       typeof(Item)
            .GetField("ItemPrefab", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
            .SetValue(this, prefab);
    }

}
