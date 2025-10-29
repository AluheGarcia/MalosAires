using UnityEngine;

// Principio de Inversión de Dependencia (DIP): Depende de la abstracción de UnityEvent.
public class FallingVendingMachine : MonoBehaviour
{
    // Usamos el estado tirado como una simulación simple, no una animación.
    // Esto podría ser un prefab de la máquina tirada.
    [Tooltip("El objeto que representa la máquina tirada en el suelo.")]
    [SerializeField] private GameObject fallenStateObject;

    // El objeto que representa la máquina de pie.
    [Tooltip("El objeto que representa la máquina de pie (debe ocultarse).")]
    [SerializeField] private GameObject standingStateObject;

    private void Awake()
    {
        // Inicialmente, la máquina tirada debe estar inactiva y la de pie activa.
        if (fallenStateObject != null)
        {
            fallenStateObject.SetActive(false);
        }

        if (standingStateObject == null)
        {
            Debug.LogError("Objeto de estado de pie no asignado.");
        }
    }

    // Este método es el que se conecta al UnityEvent del OneShotTrigger.
    // Principio de Abierto/Cerrado (OCP): El trigger es abierto a la extensión (nuevos eventos) 
    // y este objeto proporciona el evento sin modificar el trigger.
    public void CauseMachineToFall()
    {
        // Para simular la caída vista al revés, simplemente intercambiamos los modelos.
        if (standingStateObject != null)
        {
            standingStateObject.SetActive(false);
        }

        if (fallenStateObject != null)
        {
            // La máquina tirada aparece en escena justo al activarse.
            fallenStateObject.SetActive(true);
        }

        // Opcional: Activar partículas, temblor de cámara, etc.
    }
}