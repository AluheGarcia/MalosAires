using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class HealthManagerZombieBase : MonoBehaviour, IHealthZombieBase
{

    [SerializeField] int health;
    
    [SerializeField] int MeleeDamage;
    [SerializeField] int RangeDamage;
    public Animator animator;
    private int maxHealth;
    
    private UnityEngine.AI.NavMeshAgent agent;
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
    
    void Awake()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        maxHealth = health;
    }
    public void TakeRangeDamage()
    {
        health -= RangeDamage;
        Debug.Log(transform.name + " Health " + health);

        if (health <= maxHealth/2)
        {
            animator.SetBool("EnemigoCrawl", true);
            agent.speed = 1;
        }
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

