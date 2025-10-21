using UnityEngine;

public class AmmoScript :Item, IUsable
{
    [SerializeField] private int AmmoAmount = 12;
    private int BoxQuantity = 1;
    [SerializeField] private GameObject BulletPrefab;

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
