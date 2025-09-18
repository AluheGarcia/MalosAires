using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InventoryBehaviour : MonoBehaviour
{
    // Vida del jugador, solo para chequeo del slider de vida
    [SerializeField] private float PlayerLife = 100f;
        private float damage = 5f;
    public float playerLife => PlayerLife;

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
        // Testeo de perdidad de salud
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlayerLife = PlayerLife - damage;
        }

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

                equippedItem = Instantiate(entry.Value.itemPrefab, RightHand.position, RightHand.rotation, RightHand);
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

    // Esto es para el conteneo de vidas, es testeo

    public void ResetHealth()
    {
        PlayerLife = 100f;
    }

}
