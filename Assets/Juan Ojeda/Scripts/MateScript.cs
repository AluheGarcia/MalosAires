using UnityEngine;

public class MateScript : Item, IUsable
{
    [SerializeField] private int SipsAmount = 7;    
    public int sipsAmount => SipsAmount;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip drinkSound;
    [SerializeField] private float EnergyRestoreAmount = 25f;
    public void Use(GameObject user)
    {         
        PlayerStamina stamina = user.GetComponent<PlayerStamina>();
        EquippedItemHUD hud = user.GetComponentInChildren<EquippedItemHUD>();
        InventoryBehaviour inventory = user.GetComponent<InventoryBehaviour>();

        if (stamina != null && SipsAmount >0)
        {
            stamina.RestoreStaminaByDrink(EnergyRestoreAmount);
            SipsAmount--;

            if (drinkSound != null)
            {
                AudioSource.PlayClipAtPoint(drinkSound, user.transform.position, 1f);
            }


            KeyCode assignedKey = itemData.AssignKey;

            if (inventory.Inventory.ContainsKey(assignedKey))
            {
                inventory.Inventory[assignedKey].itemAmount = SipsAmount;                
            }

            if (hud != null)
            {
                if (SipsAmount > 0)
                    hud.UpdateDisplay(itemSprite, SipsAmount);
                else
                {
                    hud.ClearDisplay();
                }
            }
        }
        

        if (SipsAmount <= 0)
        {
           
            if (inventory.Inventory.ContainsKey(itemData.AssignKey))
            {
               inventory.Inventory[itemData.AssignKey].HasItem = false;
                gameObject.SetActive(false);
            }
           
        }       
        

    }

    public int GetRemainingAmount() => SipsAmount;

    public void SetTotalAmount (int amount) => SipsAmount = amount;

}
