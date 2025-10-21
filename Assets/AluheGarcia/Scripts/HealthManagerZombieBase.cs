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
    public void Death() { }

    void Awake()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        maxHealth = health;
    }
    public void TakeRangeDamage()
    {
        health -= RangeDamage;
        if (health <= maxHealth / 2)
        {
            animator.SetBool("EnemigoCrawl", true);
            agent.speed = 1;
        }
        if (health <= 0)
        {
            Destroy(gameObject);       
        }
    }

    public void TakeMeleeDamage()
    {
        health -= MeleeDamage;
        if (health <= maxHealth / 2)
        {
            animator.SetBool("EnemigoCrawl", true);
            agent.speed = 1;
        }
        if (health <= 0)
        {
            Destroy(gameObject);   
        }
    }

}

