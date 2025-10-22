using UnityEngine;

public class PlayerAmmo : MonoBehaviour
{
    public int TotalAmmo { get; private set; } = 0;

    public void AddAmmo(int amount)
    {
        TotalAmmo += amount;

    }

    public bool TryConsumeAmmo(int amount)
    {
        if (TotalAmmo >= amount)
        {
            TotalAmmo -= amount;
            return true;
        }
        return false;
    }
}
