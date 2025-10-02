using UnityEngine;

public class MateScript : Item, IUsable
{
    [SerializeField] private int EnerggyRestoreAmount = 50;
    public void Use(GameObject user)
    {         Debug.Log("Mate usado");
        // Add logic for using the mate item here
        // For example, increase player's energy or health
        // player.GetComponent<PlayerStats>().IncreaseEnergy(10);
    }
}
