using UnityEngine;
using UnityEngine.AI;

public class ZombieBaseMovement : MonoBehaviour
{
    private Transform playerTarget;
    private bool isAttacking;
    // Componente NavMeshAgent para el movimiento y la IA.
    private NavMeshAgent navMeshAgent;

    private void Awake()
    {
        // Obtiene la referencia al componente NavMeshAgent en el mismo GameObject.
        navMeshAgent = GetComponent<NavMeshAgent>();

        // Asegúrate de que el NavMeshAgent exista.
        if (navMeshAgent == null)
        {
            Debug.LogError("El script 'ZombieBaseMovement' requiere un componente NavMeshAgent.");
        }
    }

    // Este método es llamado por el área de detección para iniciar el ataque.
    public void StartAttacking(GameObject Player)
    {
        playerTarget = Player.transform;
        isAttacking = true;
        Debug.Log(gameObject.name + " ha sido notificado y ahora está atacando.");
    }

    private void Update()
    {
        // Si estamos en modo de ataque y tenemos un objetivo,
        // le decimos al NavMeshAgent que se mueva hacia él.
        if (isAttacking && playerTarget != null)
        {
            navMeshAgent.SetDestination(playerTarget.position);
        }
    }
}




/* using UnityEngine;

public class ZombieBaseMovement : MonoBehaviour
{
    private float velocidad = 3f;
    private Rigidbody rb;
    private Transform PlayerTransform;

    [SerializeField] private float distanciaMinima = 1.5f;
    [SerializeField] private float jugadorDetectado = 8f;
    [SerializeField] private float distanciaRaycast = 1f;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
            PlayerTransform = player.transform;
        else
            Debug.LogError("No se encontró ningún objeto con la etiqueta 'Player'");
    }


    void FixedUpdate()
    {
        if (PlayerTransform == null) return;

        Vector3 direccion = PlayerTransform.position - rb.position;
        float distancia = direccion.magnitude;

        RaycastHit hit;
        Vector3 direccionRay = direccion.normalized;
        Debug.DrawRay(transform.position, direccionRay * distanciaRaycast, Color.red);
        
            if (Physics.Raycast(transform.position, direccionRay, out hit, distanciaRaycast))
            {
                if (hit.collider.CompareTag("Wall"))
                {
                    // Generamos las dos posibles direcciones
                    Vector3 derecha = Quaternion.Euler(0, 90, 0) * direccionRay;
                    Vector3 izquierda = Quaternion.Euler(0, -90, 0) * direccionRay;

                    // Variables para guardar distancias
                    float distanciaDerecha = float.MaxValue;
                    float distanciaIzquierda = float.MaxValue;

                    RaycastHit hitInfo;

                    // Medimos hacia la derecha
                    if (Physics.Raycast(transform.position, derecha, out hitInfo, jugadorDetectado))
                        distanciaDerecha = hitInfo.distance;
                    else
                        distanciaDerecha = jugadorDetectado; // libre hasta rango máximo

                    // Medimos hacia la izquierda
                    if (Physics.Raycast(transform.position, izquierda, out hitInfo, jugadorDetectado))
                        distanciaIzquierda = hitInfo.distance;
                    else
                        distanciaIzquierda = jugadorDetectado; // libre hasta rango máximo

                    // Elegimos el camino con más espacio libre (más corto para salir del bloqueo)
                    if (distanciaDerecha > distanciaIzquierda)
                        direccionRay = derecha;
                    else
                        direccionRay = izquierda;
                }

        }

        if (distancia > distanciaMinima && distancia <= jugadorDetectado)
        {
            direccion.Normalize();
            Vector3 nuevoPos = rb.position + direccion * velocidad * Time.fixedDeltaTime;
            rb.MovePosition(nuevoPos);

            Quaternion rotacion = Quaternion.LookRotation(direccion);
            rb.MoveRotation(rotacion);
        }
    }


}
*/