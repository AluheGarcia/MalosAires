using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class KnifeScript : Item
{
       
    [SerializeField] private int damage = 15;
    public int Damage => damage;

    private void Start()
    {
        inventory = GetComponentInParent<InventoryBehaviour>();       
    }

    private void Update()
    {
        if (inventory != null && inventory.Equippeditem == gameObject && Input.GetButtonDown("Fire1"))
        {
            PerformAttack(inventory.gameObject);
        }
    }
        

    private void PerformAttack(GameObject user)
    { 
        float attackRange = 1.5f;
        float attackRadius = 0.5f;

        Vector3 attackorigin = user.transform.position + user.transform.forward*attackRange;
        Collider[] hitColliders = Physics.OverlapSphere(attackorigin, attackRadius);

        PlayerStamina stamina = user.GetComponent<PlayerStamina>();
        if ( stamina != null)
        {
            stamina.SpendStaminaOnAttack();
        }

        foreach (var hit in hitColliders)
        {
            HealthManagerZombieBase zombieHealth = hit.GetComponent<HealthManagerZombieBase>();

            if (zombieHealth != null)
            {
                
                zombieHealth.TakeMeleeDamage();
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (Application.isPlaying)
        {
            Gizmos.color = Color.red;
            Vector3 attackorigin = transform.position + transform.forward;
            Gizmos.DrawWireSphere(attackorigin, 1f);
        }
    }



}
