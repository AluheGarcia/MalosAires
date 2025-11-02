using UnityEngine;

public class AmmoScript :Item, IUsable
{
    [SerializeField] private int AmmoAmount = 12;
    public int ammoAmount => AmmoAmount;
    private int BoxQuantity = 1;
    [SerializeField] private GameObject BulletPrefab;

    public void Start()
    {
        inventory = GetComponentInParent<InventoryBehaviour>();
    }

    public void Use(GameObject user)
    {
        if (BoxQuantity <= 0)
        {
            return;
        }

        PlayerAmmo playerAmmo = user.GetComponent<PlayerAmmo>();
        InventoryBehaviour inventory = user.GetComponent<InventoryBehaviour>();
        

        if (playerAmmo != null && AmmoAmount > 0 && BoxQuantity == 1)
        {
            playerAmmo.AddAmmo(AmmoAmount);

            AmmoAmount = 0;
            BoxQuantity--;

            if (inventory != null)
            {
                inventory.UpdateInventoryDis("Ammo", itemSprite, 0);

                KeyCode assignedKey = GetAssignKey();
                if (inventory.Inventory.ContainsKey(assignedKey))
                {
                    inventory.Inventory[assignedKey].HasItem = false;
                }
            }

            Destroy(gameObject);
        }

        
    }
          

}
