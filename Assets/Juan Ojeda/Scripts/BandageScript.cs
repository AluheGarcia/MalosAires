using UnityEngine;

public class BandageScript : Item , IUsable

{
    private int TotalAmount = 3;

    private void Start()
    {
       inventory = GetComponentInParent<InventoryBehaviour>();
    }
    public void Use(GameObject user)
    {       
       


        LifeCountScript playerLife = user.GetComponentInChildren<LifeCountScript>();
       
        if (playerLife != null && TotalAmount > 0)
        {            
           
            playerLife.RestoreLife();
            
        }
        TotalAmount--;

        InventoryBehaviour inventory = user.GetComponent<InventoryBehaviour>();

        if (TotalAmount <= 0 && inventory != null)
        {
            KeyCode assignedKey = GetAssignKey();
            if (inventory.Inventory.ContainsKey(assignedKey))
            {
                inventory.Inventory[assignedKey].HasItem = false;
            }
            Destroy(gameObject);
        }
    }

    
}
