using Microsoft.Unity.VisualStudio.Editor;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

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
    public GameObject Equippeditem => equippedItem;
    private bool HasItem = false;

    [SerializeField] private EquippedItemHUD equippedItemHUD;
    [SerializeField] private UnityEngine.UI.Image equippedItemIcon;
    

    public Dictionary<KeyCode, InventorySlot> Inventory = new Dictionary<KeyCode, InventorySlot>();
    private KeyCode equippedKey = KeyCode.None;
    public KeyCode EquippedKey => equippedKey;
    public Transform rightHand => RightHand;
    public List<GameObject> itemPrefabs => ItemPrefabs;
    public List<KeyCode> itemKeyLog => ItemKeyLog;

    
    void Start()
    {
        Inventory[KeyCode.Alpha1] = new InventorySlot(ItemPrefabs[0]) { HasItem = false };
        Inventory[KeyCode.Alpha2] = new InventorySlot(ItemPrefabs[1]) { HasItem = false }; 
        equippedItemHUD = FindAnyObjectByType<EquippedItemHUD>();


    }

    
    void Update()
    {
         
       
        ItemAddition ();

        EquippedItem ();

        DroppedItem();

        if (equippedItem != null && Input.GetKeyDown(KeyCode.Q))
        {
            
            IUsable usableItem = equippedItem.GetComponent<IUsable>();
            if (usableItem != null)
            {
                usableItem.Use(gameObject);
            }

        }


    }
 
    public void ItemAddition()
    {
        if (Input.GetKeyDown(KeyCode.E) && NearItem != null)
        {
            Item item = NearItem.GetComponent<Item>();
            if (item == null) return;

            KeyCode assignKey = item.GetAssignKey();

            AmmoScript ammoBox = NearItem.GetComponent<AmmoScript>();
            if ( ammoBox != null)
            {
                PlayerAmmo playerAmmo = GetComponent<PlayerAmmo>();
                if (playerAmmo != null)
                {
                    playerAmmo.AddAmmo(ammoBox.ammoAmount);
                    GetComponent<MenuManagment>()?.AddItemToInventory(
                       item.itemName,
                       item.itemSprite);
                    NearItem.SetActive(false);
                    NearItem = null;
                    return;
                }                
            }

            if (Inventory.ContainsKey(assignKey))
            {
                if (Inventory[assignKey].HasItem && item.itemName == "Bandage")
                {
                    return;
                }

                if (!Inventory[assignKey].HasItem)
                {
                    Inventory[assignKey].HasItem = true;

                    GetComponent<MenuManagment>()?.AddItemToInventory(
                        item.itemName,
                        item.itemSprite);

                    NearItem.SetActive(false);
                    NearItem = null;

                    return;
                }

                    
                
            }

            if (!Inventory.ContainsKey(assignKey))
            {
                GetComponent<MenuManagment>()?.AddItemToInventory(
                    item.itemName,
                    item.itemSprite);

                NearItem.SetActive(false);
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

             
                equippedItem = Instantiate(entry.Value.itemPrefab, RightHand);
                equippedItem.transform.localPosition = Vector3.zero;
                equippedItem.transform.localRotation = Quaternion.identity;
               

                equippedKey = entry.Key;
                               

                Item itemScript = equippedItem.GetComponent<Item>();
                if (itemScript != null)
                {
                    itemScript.SetInventory (this);
                    itemScript.SetPrefab(entry.Value.itemPrefab);

                    if (equippedItemHUD != null)
                    {
                       int amount = 1;
                        if (itemScript is BandageScript bandage)
                            amount = bandage.GetRemainingAmount();
                        else  if (itemScript is MateScript mate)
                            amount = mate.GetRemainingAmount();

                        equippedItemHUD.UpdateDisplay(itemScript.itemSprite, amount);
                    }

                }

                

              

            }
        }
        
    }

    public void DroppedItem()
    {
        if (equippedItem != null && Input.GetKeyDown(KeyCode.F))
        {
            Vector3 dropPos = RightHand.position + RightHand.forward;

            Item itemComp = equippedItem.GetComponent<Item>();

            if (itemComp != null)
            {
                KeyCode assignKey = itemComp.GetAssignKey();

            if (Inventory.ContainsKey(assignKey))
            {
                Inventory[assignKey].HasItem = false;
            }
               Instantiate(itemComp.itemPrefab, dropPos, Quaternion.identity);
            }
            
            GameObject droppedItem = Instantiate(itemComp.itemPrefab, dropPos, Quaternion.identity);            

            GetComponent<MenuManagment>()?.RemoveItemFromInventory(itemComp.itemName);
            equippedItem.SetActive(false);
            equippedItem = null;

            Debug.Log("Item dropeado");
        }
    }

    public bool ContainsItem(GameObject prefab)
    {
        foreach (var slot in Inventory.Values)
        {
            Debug.Log($"[DEBUG] Slot prefab: {slot.itemPrefab?.name} ({slot.itemPrefab?.GetInstanceID()}) | HasItem: {slot.HasItem}");
            if (slot.itemPrefab == prefab && slot.HasItem)
                return true;
        }
        return false;
    }

    public bool IsItemEquipped(GameObject item)
    {
        return equippedItem == item;
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

    [System.Serializable]
    public class InventorySlot
    {
        public GameObject itemPrefab;
        public bool HasItem;
        public InventorySlot(GameObject prefab)
        {
            itemPrefab = prefab;
            HasItem = false;
        }
    }
}


