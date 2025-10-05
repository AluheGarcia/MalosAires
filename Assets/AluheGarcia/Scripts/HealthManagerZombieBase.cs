using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class HealthManagerZombieBase : MonoBehaviour, IHealthZombieBase
{

    [SerializeField] int health;
    [SerializeField] int MeleeDamage;
    [SerializeField] int RangeDamage;
    
    public int Health { get { return health; } set { health = value; } }
    //public void TakeMeleeDamage() { }
    //public void TakeRangeDamage() { }
    public void Death() { }
    //void Update()
    //{
    //    // --- SOLO PARA PRUEBA ---
    //    if (Input.GetKeyDown(KeyCode.H))
    //    {
    //        TakeDamage(10);
    //    }
    //}
    

    public void TakeRangeDamage()
    {
        health -= RangeDamage;
        Debug.Log(transform.name + " Health " + health);

        if (health <= 0)
        {
            Debug.Log(transform.name + " murió");
            Destroy(gameObject);
        }
    }

    public void TakeMeleeDamage()
    {
        health -= MeleeDamage;
        Debug.Log(transform.name + " Health " + health);

        if (health <= 0)
        {
            Debug.Log(transform.name + " murió");
            Destroy(gameObject);
        }
    }

}

