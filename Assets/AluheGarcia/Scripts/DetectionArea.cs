using UnityEngine;

public class DetectionArea : MonoBehaviour
{
    private BoxCollider detectionCollider;
    void Start()
    {
        detectionCollider = GetComponent<BoxCollider>();
        if (detectionCollider == null)
        {
            Debug.Log("no se encuentra un boxcollider");
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Jugador detectado! Buscando enemigos en el área de la caja.");

            // Calcula el tamaño de la caja de búsqueda.
            // Usamos 'lossyScale' para obtener la escala global del objeto.
            Vector3 searchHalfExtents = Vector3.Scale(detectionCollider.size, transform.lossyScale) / 2;

            // Realiza la detección con OverlapBox.
            Collider[] hitColliders = Physics.OverlapBox(transform.position, searchHalfExtents, transform.rotation);

            foreach (var hitCollider in hitColliders)
            {
                ZombieBaseMovement enemy = hitCollider.GetComponent<ZombieBaseMovement>();
                if (enemy != null)
                {
                    enemy.StartAttacking(other.gameObject);
                }
            }
        }
    }


    // Update is called once per frame
    void Update()
    {

    }
}
