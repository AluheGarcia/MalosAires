using JetBrains.Annotations;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public abstract class Item : MonoBehaviour
{
    // Script base para cada item

    [SerializeField] protected Rigidbody ItemRb;
    [SerializeField] private GameObject ItemPrefab;
    [SerializeField] private string ItemName;
    [SerializeField] private Sprite ItemSprite;
   
    [SerializeField] protected KeyCode AssignKey;

    protected bool PlayerInRange = false;
    protected InventoryBehaviour inventory;   
    public InventoryBehaviour inv => inventory;

    // Variables publicas pero no editables por otro 

    public GameObject itemPrefab => ItemPrefab;
    public Sprite itemSprite => ItemSprite;
    public string itemName => ItemName;



    public KeyCode GetAssignKey()
    {
        return AssignKey;
    }

    public GameObject GetPrefab()
    {
        return ItemPrefab;
    }

    public string GetName()
    {
        return ItemName;
    }

    public Sprite GetSprite()
    {
        return ItemSprite;
    }

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
