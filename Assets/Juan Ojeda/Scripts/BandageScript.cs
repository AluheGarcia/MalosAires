using Unity.VisualScripting;
using UnityEngine;

public class BandageScript : Item , IUsable

{
    private int TotalAmount = 3;
    public int totalAmount => TotalAmount;

    private void Start()
    {
       inventory = GetComponentInParent<InventoryBehaviour>();
    }
    public void Use(GameObject user)
    {       
       
        LifeCountScript playerLife = user.GetComponentInChildren<LifeCountScript>();
        InventoryBehaviour inventory = user.GetComponent<InventoryBehaviour>();
        EquippedItemHUD hud = user.GetComponentInChildren<EquippedItemHUD>();

        if (playerLife != null && TotalAmount > 0)
        {            
           
            playerLife.RestoreLife();
            TotalAmount--;

            KeyCode assignedKey = GetAssignKey();

            if (inventory.Inventory.ContainsKey(assignedKey))
            {
                inventory.Inventory[assignedKey].itemAmount = TotalAmount;               
            }

            if (hud != null)
            {
                if (TotalAmount > 0)
                    hud.UpdateDisplay(itemSprite, TotalAmount);
                else
                {
                    hud.ClearDisplay();
                }
            }

            if (TotalAmount <= 0 && inventory != null)
            {
                if (inventory.Inventory.ContainsKey(assignedKey))
                {
                    inventory.Inventory[assignedKey].HasItem = false;
                    gameObject.SetActive(false);
                }
                   
            }
        }                    
                
    }

   public int GetRemainingAmount() => TotalAmount;
    public void SetTotalAmount(int amount) => TotalAmount = amount;

}
