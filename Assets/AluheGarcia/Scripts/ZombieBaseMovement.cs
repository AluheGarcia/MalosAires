using UnityEngine;
using UnityEngine.AI;

public class ZombieBaseMovement : MonoBehaviour
{
    private Transform playerTarget;
    private bool isAttacking;
    private NavMeshAgent navMeshAgent;
    public Animator animator;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        if (navMeshAgent == null)
        {
            Debug.LogError("El script 'ZombieBaseMovement' requiere un componente NavMeshAgent.");
        }
    }
    public void StartAttacking(GameObject player)
    {
        playerTarget = player.transform;
        isAttacking = true;
        animator.SetBool("DetectoEnemigo", true);
    }

    private void Update()
    {
        if (isAttacking && playerTarget != null)
        {
            navMeshAgent.SetDestination(playerTarget.position);
        }
        
    }
}