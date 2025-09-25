using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InventoryBehaviour : MonoBehaviour
{
    
    // Script Del manejo de inventario 
    [SerializeField] private Transform RightHand;
    [SerializeField] private List<GameObject> ItemPrefabs;
    [SerializeField] private List<KeyCode> ItemKeyLog;    
    [SerializeField] private bool ItemInRange = false;
    [SerializeField] private GameObject ItemPrefab;
    [SerializeField] private GameObject NearItem;
    private GameObject equippedItem = null;
    private bool HasItem = false;

    public Dictionary<KeyCode, InventorySlot> Inventory = new Dictionary<KeyCode, InventorySlot>();
    public Transform rightHand => RightHand;
    public List<GameObject> itemPrefabs => ItemPrefabs;
    public List<KeyCode> itemKeyLog => ItemKeyLog;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Inventory[KeyCode.Alpha1] = new InventorySlot(ItemPrefabs[0]) { HasItem = false };
        Inventory[KeyCode.Alpha2] = new InventorySlot(ItemPrefabs[1]) { HasItem = false };
        Inventory[KeyCode.Alpha3] = new InventorySlot(ItemPrefabs[2]) { HasItem = false };
        Inventory[KeyCode.Alpha4] = new InventorySlot(ItemPrefabs[3]) { HasItem = false };
    }

    // Update is called once per frame
    void Update()
    {
    
        // Script valido
       
        ItemAddition ();

        EquippedItem ();

        DroppedItem();

    }
 
    public void ItemAddition()
    {
        if (Input.GetKeyDown(KeyCode.E) && NearItem != null)
        {
            Item item = NearItem.GetComponent<Item>();
            KeyCode assignKey = item.GetAssignKey();

            if (Inventory.ContainsKey(assignKey) && !Inventory[assignKey].HasItem)
                {
                    Inventory[assignKey].HasItem = true;

                    GetComponent<MenuManagment>()?.AddItemToInventory(
                        item.itemName,
                        item.itemSprite
                    );

                    Destroy(NearItem);
                    NearItem = null;
                    
                }
            
        }
    }


    public void EquippedItem()
    {
        foreach (var entry in Inventory)
        {
            if (Input.GetKeyDown(entry.Key) && entry.Value.HasItem)
            {               
                foreach (Transform child in RightHand)
                    Destroy(child.gameObject);

                //equippedItem = Instantiate(entry.Value.itemPrefab, RightHand.position, RightHand.rotation, RightHand);
                equippedItem = Instantiate(entry.Value.itemPrefab, RightHand);
                equippedItem.transform.localPosition = Vector3.zero;
                equippedItem.transform.localRotation = Quaternion.identity;
                Debug.Log($"Equipado: {entry.Value.itemPrefab.name}");
            }
        }
        
    }

    public void DroppedItem()
    {
        if (equippedItem != null && Input.GetKeyDown(KeyCode.F))
        {
            Vector3 dropPos = RightHand.position + RightHand.forward;

            Item itemComp = equippedItem.GetComponent<Item>();
            KeyCode assignKey = itemComp.GetAssignKey();
            if (Inventory.ContainsKey(assignKey))
            {
                Inventory[assignKey].HasItem = false;
            }
            
            GameObject droppedItem = Instantiate(itemComp.itemPrefab, dropPos, Quaternion.identity);            

            GetComponent<MenuManagment>()?.RemoveItemFromInventory(itemComp.itemName);
            Destroy(equippedItem);
            equippedItem = null;

            Debug.Log("Item dropeado");
        }
    }

    public void OnTriggerEnter(Collider other)
    {
       if (other.gameObject.layer == LayerMask.NameToLayer("Item"))
        {
            ItemInRange = true;
            NearItem = other.gameObject;
        }
    }

    public void OnTriggerExit(Collider other)
    {

        if (other.gameObject.layer == LayerMask.NameToLayer("Item"))
        {
            ItemInRange = false;
            NearItem = null;
        }
    }

  
}
