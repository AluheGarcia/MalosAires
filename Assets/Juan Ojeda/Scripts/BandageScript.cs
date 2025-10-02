using UnityEngine;

public class BandageScript : Item , IUsable

{
    public void Use(GameObject user)
    {
        Debug.Log("Benda usada");

        LifeCountScript playerLife = user.GetComponent<LifeCountScript>();
        if (playerLife != null)
        {
            playerLife.RestoreLife();
        }
        
    }
}
