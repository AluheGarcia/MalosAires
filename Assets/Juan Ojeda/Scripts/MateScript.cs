using UnityEngine;

public class MateScript : Item, IUsable
{
    [SerializeField] private int SipsAmount = 7;
    [SerializeField] private float EnergyRestoreAmount = 25f;
    public void Use(GameObject user)
    {         
        PlayerStamina stamina = user.GetComponent<PlayerStamina>();
        if (stamina != null && SipsAmount >0)
        {
            stamina.RestoreStaminaByDrink(EnergyRestoreAmount);
           
            
        }
        SipsAmount--;

        EquippedItemHUD hud = user.GetComponentInChildren<EquippedItemHUD>();
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

    public int GetRemainingAmount()
    {
        return SipsAmount;
    }
}
