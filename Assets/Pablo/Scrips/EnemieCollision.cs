using UnityEngine;
using UnityEngine.AI;
using System.Collections;
public class EnemieCollision : MonoBehaviour
{
    private NavMeshAgent agent;
    private float velocidad;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        velocidad = agent.speed;
    }
    private void OnTriggerEnter(Collider colider)
    {
        if (colider.gameObject.CompareTag("melee"))
        {
            GetComponent<HealthManagerZombieBase>().TakeMeleeDamage();
            StartCoroutine(RalentizarTemporal(0.1f, 1));
        }


        if (colider.gameObject.CompareTag("Bullet"))
        {
            GetComponent<HealthManagerZombieBase>().TakeRangeDamage();
            Destroy(colider.gameObject);
            StartCoroutine(RalentizarTemporal(0.1f, 1));
        }

    
    }
    private IEnumerator RalentizarTemporal(float porcentaje, float duracion)
    {
        agent.speed = velocidad * porcentaje;
        yield return new WaitForSeconds(duracion);
        agent.speed = velocidad;
    }


}
