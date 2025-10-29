//using UnityEngine;

//public class DoorTrigger : MonoBehaviour
//{
//    [SerializeField] private DoorController targetDoor;
//    [SerializeField] private string targetTag = "Player";

//    private void OnTriggerEnter(Collider other)
//    {
//        if (other.CompareTag(targetTag))
//        {
//            if (targetDoor != null)
//            {
//                // Abre la puerta al entrar
//                targetDoor.ToggleDoor();
//            }
//        }
//    }

//    private void OnTriggerExit(Collider other)
//    {
//        if (other.CompareTag(targetTag))
//        {
//            if (targetDoor != null)
//            {
//                // Cierra la puerta al salir (Toggle la vuelve a llamar)
//                targetDoor.ToggleDoor();
//            }
//        }
//    }
//}


//using UnityEngine;

//public class DoorTrigger : MonoBehaviour
//{
//    // Referencia al script DoorController de la puerta.
//    [SerializeField] private DoorController targetDoor;
//    // Etiqueta del objeto que activa el trigger (debería ser "Player").
//    [SerializeField] private string targetTag = "Player";

//    // Bandera para asegurar que solo se abra una vez.
//    private bool hasOpened = false;

//    private void OnTriggerEnter(Collider other)
//    {
//        // Verifica la etiqueta y que no se haya abierto ya.
//        if (other.CompareTag(targetTag) && !hasOpened)
//        {
//            if (targetDoor != null)
//            {
//                // **Abre la puerta (usando la función que SOLO abre)**
//                targetDoor.OpenDoor();

//                // Marca como abierto para que no se vuelva a activar si se queda dentro del trigger.
//                hasOpened = true;
//            }
//            else
//            {
//                Debug.LogError("Target Door no asignado en el Inspector del DoorTrigger: " + gameObject.name);
//            }
//        }
//    }

//    // Se ELIMINA el método OnTriggerExit() para que la puerta no se cierre nunca.
//}


using UnityEngine;
using System.Collections; // Necesario para la corrutina de retraso

public class DoorTrigger : MonoBehaviour
{
    // Referencia al script DoorController de la puerta.
    [SerializeField] private DoorController targetDoor;
    // Etiqueta del objeto que activa el trigger (debería ser "Player").
    [SerializeField] private string targetTag = "Player";

    // === CONFIGURACIÓN DE INVOCACIÓN DE ENEMIGO ===
    [SerializeField] private GameObject zombiePrefab; // ¡El Prefab del Zombi!
    [SerializeField] private Transform spawnPoint; // Punto donde aparecerá el zombi
    [SerializeField] private float spawnDelay = 0.5f; // Pequeño retraso después de abrir

    // Bandera para asegurar que solo se abra una vez y que solo aparezca un zombi.
    private bool hasOpened = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(targetTag) && !hasOpened)
        {
            if (targetDoor != null)
            {
                // 1. Abre la puerta
                targetDoor.OpenDoor();

                // 2. Inicia la corrutina para que el zombi aparezca después de un breve retraso
                StartCoroutine(DelayedZombieSpawn(spawnDelay));

                // 3. Marca como abierto y evita más llamadas
                hasOpened = true;
            }
            else
            {
                Debug.LogError("Target Door no asignado en el Inspector del DoorTrigger: " + gameObject.name);
            }
        }
    }

    private IEnumerator DelayedZombieSpawn(float delay)
    {
        if (delay > 0f)
        {
            yield return new WaitForSeconds(delay);
        }

        // Lógica de generación del enemigo
        if (zombiePrefab != null && spawnPoint != null)
        {
            Instantiate(zombiePrefab, spawnPoint.position, spawnPoint.rotation);
        }
        else
        {
            Debug.LogError("Error: Zombie Prefab o Spawn Point no asignados en el Inspector del DoorTrigger.");
        }
    }

    // Se elimina OnTriggerExit() para que la puerta permanezca abierta.
}