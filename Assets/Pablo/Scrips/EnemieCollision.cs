using UnityEngine;

public class EnemieCollision : MonoBehaviour
{


  

    private void OnTriggerEnter(Collider colider)
    {

        if (colider.gameObject.CompareTag("melee"))
        {
            GetComponent<HealthManagerZombieBase>().TakeMeleeDamage();
            Debug.Log("Da�o al enemigo");
        }


        if (colider.gameObject.CompareTag("Bullet"))
        {
            GetComponent<HealthManagerZombieBase>().TakeRangeDamage();
            Debug.Log("Da�o al enemigo");

            Destroy(colider.gameObject);
        }

    }



}
