using UnityEngine;


public interface IHealthZombieBase
{
    int Health{ get; set; }
    void TakeDamage();
    void Death();
}
