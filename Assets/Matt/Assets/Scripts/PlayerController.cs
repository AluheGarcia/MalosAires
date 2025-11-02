using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float attackCooldown = 1f;
    [SerializeField] private float attackRange = 1f;
    [SerializeField] private int attackDamage = 10;
    [SerializeField] private LayerMask attackLayer;

    private CharacterController characterController;
    private float nextAttackTime;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        HandleMovement();
        HandleAttack();
    }

    private void HandleMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;
        moveDirection = Camera.main.transform.TransformDirection(moveDirection);
        moveDirection.y = 0f;

        characterController.Move(moveDirection * moveSpeed * Time.deltaTime);

        if (moveDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
        }
    }

    private void HandleAttack()
    {
        if (Input.GetButtonDown("Fire1") && Time.time >= nextAttackTime)
        {
            Attack();
            nextAttackTime = Time.time + attackCooldown;
        }
    }

    private void Attack()
    {
        Debug.Log("Player Attacked!");

        Vector3 attackOrigin = transform.position + transform.forward * 0.5f;

        Collider[] hitTargets = Physics.OverlapSphere(attackOrigin, attackRange, attackLayer);

        foreach (Collider target in hitTargets)
        {
            IDamageable damageable = target.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.TakeDamage(attackDamage);
            }
        }
    }

    // Para visualizar el rango de ataque en el Editor
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Vector3 attackOrigin = transform.position + transform.forward * 0.5f;
        Gizmos.DrawWireSphere(attackOrigin, attackRange);
    }
}

// Interfaz para seguir el principio SOLID (Principio de Sustitución de Liskov)
public interface IDamageable
{
    void TakeDamage(int damage);
}