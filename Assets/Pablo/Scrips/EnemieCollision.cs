using UnityEngine;

public class EnemieCollision : MonoBehaviour
{
    private void OnTriggerEnter(Collider colider)
    {

        if (colider.gameObject.CompareTag("melee"))
        {
            GetComponent<HealthManagerZombieBase>().TakeMeleeDamage();
        }


        if (colider.gameObject.CompareTag("Bullet"))
        {
            GetComponent<HealthManagerZombieBase>().TakeRangeDamage();

            Destroy(colider.gameObject);
        }

    }


}
