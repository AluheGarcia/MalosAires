using UnityEngine;


public interface IHealthZombieBase
{
    int Health{ get; set; }
    void TakeMeleeDamage();
    void TakeRangeDamage();
    void Death();
}
