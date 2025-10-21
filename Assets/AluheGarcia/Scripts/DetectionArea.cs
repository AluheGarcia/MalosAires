using UnityEngine;

public class DetectionArea : MonoBehaviour
{
    private SphereCollider detectionCollider;
    void Start()
    {
        detectionCollider = GetComponent<SphereCollider>();
        if (detectionCollider == null)
        {
            Debug.Log("no se encuentra un SphereCollider");
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            float radius = detectionCollider.radius * Mathf.Max(transform.lossyScale.x, transform.lossyScale.y, transform.lossyScale.z);
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius);

            foreach (var hitCollider in hitColliders)
            {
                ZombieBaseMovement enemy = hitCollider.GetComponentInParent<ZombieBaseMovement>();
                if (enemy != null)
                {
                    enemy.StartAttacking(other.gameObject);
                }
            }
        }
    }
}

