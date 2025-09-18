using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class HealthManagerZombieBase : MonoBehaviour, IHealthZombieBase
{

    [SerializeField] int health;
    
    public int Health { get { return health; } set { health = value; } }
    public void TakeDamage() { }
    public void Death() { }
    void Update()
    {
        // --- SOLO PARA PRUEBA ---
        if (Input.GetKeyDown(KeyCode.H))
        {
            TakeDamage(10);
        }
    }


    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log(transform.name + " Health " + health);

        if (health <= 0)
        {
            Debug.Log(transform.name + " murió");
            Destroy(gameObject);
        }
    }
}

