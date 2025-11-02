//using UnityEngine;
//using UnityEngine.Events;
//using System.Collections; // Necesario para Coroutines

//public class BathroomEncounterTrigger : MonoBehaviour
//{
//    [Tooltip("Etiqueta (Tag) que debe tener el objeto para activar el trigger.")]
//    [SerializeField] private string targetTag = "Player";

//    [Header("Eventos de la Secuencia")]
//    [Tooltip("Evento disparado al entrar (ej. apagar luces).")]
//    [SerializeField] private UnityEvent onPlayerEnter;

//    [Tooltip("Retraso y evento para el mensaje en el espejo.")]
//    [SerializeField] private float mirrorMessageDelay = 5.0f;
//    [SerializeField] private UnityEvent onMirrorMessage;

//    [Tooltip("Retraso y evento para restaurar luces/normalidad.")]
//    [SerializeField] private float restoreNormalDelay = 3.0f; // Después del mensaje
//    [SerializeField] private UnityEvent onRestoreNormal;

//    [Tooltip("Retraso y evento para abrir la puerta del cubículo y spawnera al enemigo.")]
//    [SerializeField] private float enemySpawnDelay = 2.0f; // Después de restaurar normalidad
//    [SerializeField] private UnityEvent onEnemySpawn;

//    private Collider _collider;
//    private bool _hasTriggered = false;

//    private void Awake()
//    {
//        _collider = GetComponent<Collider>();
//        if (_collider == null)
//        {
//            Debug.LogError("BathroomEncounterTrigger requiere un componente Collider.");
//        }
//        if (_collider != null && !_collider.isTrigger)
//        {
//            Debug.LogWarning("El Collider de BathroomEncounterTrigger no está marcado como 'Is Trigger'.");
//        }
//    }

//    private void OnTriggerEnter(Collider other)
//    {
//        if (!_hasTriggered && other.CompareTag(targetTag))
//        {
//            _hasTriggered = true;
//            _collider.enabled = false; // Desactivar para que sea one-shot.
//            StartCoroutine(EncounterSequenceRoutine());
//        }
//    }

//    private IEnumerator EncounterSequenceRoutine()
//    {
//        // 1. Apagar luces y parpadeo inicial (conectado a onPlayerEnter)
//        onPlayerEnter.Invoke();
//        Debug.Log("Evento 1: Luces apagándose.");

//        // 2. Esperar y mostrar mensaje en el espejo
//        yield return new WaitForSeconds(mirrorMessageDelay);
//        onMirrorMessage.Invoke();
//        Debug.Log("Evento 2: Mensaje en espejo.");

//        // 3. Esperar y restaurar luces/normalidad
//        yield return new WaitForSeconds(restoreNormalDelay);
//        onRestoreNormal.Invoke();
//        Debug.Log("Evento 3: Normalidad restaurada.");

//        // 4. Esperar y abrir puerta/spawnear enemigo
//        yield return new WaitForSeconds(enemySpawnDelay);
//        onEnemySpawn.Invoke();
//        Debug.Log("Evento 4: Puerta se abre, enemigo aparece.");
//    }
//}



//using UnityEngine;
//using UnityEngine.Events;
//using System.Collections; // Necesario para la corrutina de retraso

//public class BathroomEncounterTrigger : MonoBehaviour
//{
//    // [Header("Configuración")] // Para organizar en el Inspector, si quieres
//    [SerializeField] private string targetTag = "Player";
//    [SerializeField] private float mirrorMessageDelay = 5f;
//    [SerializeField] private float restoreNormalDelay = 3f;
//    [SerializeField] private float enemySpawnDelay = 2f;

//    // [Header("Eventos")]
//    // UnityEvents para configurar desde el Inspector
//    [SerializeField] private UnityEvent onPlayerEnter = new UnityEvent();
//    [SerializeField] private UnityEvent onMirrorMessage = new UnityEvent();
//    [SerializeField] private UnityEvent onRestoreNormal = new UnityEvent();
//    [SerializeField] private UnityEvent onEnemySpawn = new UnityEvent();

//    private bool isTriggered = false;

//    private void OnTriggerEnter(Collider other)
//    {
//        if (isTriggered) return;

//        if (other.CompareTag(targetTag))
//        {
//            isTriggered = true;
//            onPlayerEnter.Invoke();

//            // Usamos corrutinas internamente para el retraso, 
//            // liberando al componente Trigger de Unity para que no tenga que gestionar los delays en el Inspector.
//            StartCoroutine(DelayedAction(mirrorMessageDelay, onMirrorMessage));
//            StartCoroutine(DelayedAction(restoreNormalDelay, onRestoreNormal));
//            StartCoroutine(DelayedAction(enemySpawnDelay, onEnemySpawn));
//        }
//    }

//    private IEnumerator DelayedAction(float delay, UnityEvent action)
//    {
//        if (delay <= 0f)
//        {
//            action.Invoke();
//            yield break;
//        }

//        yield return new WaitForSeconds(delay);
//        action.Invoke();
//    }
//}

///* * NOTA: 
// * Si usas este script, debes configurar en el Inspector:
// * - onPlayerEnter: la llamada para iniciar el parpadeo de luces.
// * - onMirrorMessage: la llamada a MirrorMessage.ShowAnd...()
// * - onRestoreNormal: la llamada para restaurar las luces.
// * * De esta manera, el script sigue el principio KISS/Encapsulado 
// * al manejar los tiempos de retraso internamente con corrutinas.
// */


//using UnityEngine;
//using System.Collections;
//using UnityEngine.Events;

//public class BathroomEncounterTrigger : MonoBehaviour
//{
//    // === CONFIGURACIÓN GENERAL ===
//    [SerializeField] private string targetTag = "Player";

//    private bool isTriggered = false;

//    // === CONFIGURACIÓN DE EVENTOS DEL BAÑO ===
//    // Eventos que se lanzan inmediatamente
//    [SerializeField] private UnityEvent onPlayerEnter = new UnityEvent();

//    // Evento del Espejo
//    [SerializeField] private float mirrorMessageDelay = 5f;
//    [SerializeField] private UnityEvent onMirrorMessage = new UnityEvent();

//    // Evento de Restauración de Luces
//    [SerializeField] private float restoreNormalDelay = 3f;
//    [SerializeField] private UnityEvent onRestoreNormal = new UnityEvent();

//    // === CONFIGURACIÓN DE INVOCACIÓN DE ENEMIGO (NUEVAS VARIABLES) ===
//    [SerializeField] private GameObject zombiePrefab; // ¡El Prefab del Zombi!
//    [SerializeField] private Transform spawnPoint; // Punto donde aparecerá el zombi
//    [SerializeField] private float enemySpawnDelay = 2f;

//    // (Hemos quitado onEnemySpawn como UnityEvent porque la lógica de Instanciar es fija)

//    private void OnTriggerEnter(Collider other)
//    {
//        if (isTriggered) return;

//        if (other.CompareTag(targetTag))
//        {
//            isTriggered = true;
//            onPlayerEnter.Invoke();

//            // Llama al mensaje y restauración con retraso
//            StartCoroutine(DelayedAction(mirrorMessageDelay, onMirrorMessage));
//            StartCoroutine(DelayedAction(restoreNormalDelay, onRestoreNormal));

//            // ¡Llama a la invocación del Zombi con retraso!
//            StartCoroutine(DelayedZombieSpawn(enemySpawnDelay));
//        }
//    }

//    private IEnumerator DelayedAction(float delay, UnityEvent action)
//    {
//        if (delay <= 0f)
//        {
//            action.Invoke();
//            yield break;
//        }

//        yield return new WaitForSeconds(delay);
//        action.Invoke();
//    }

//    private IEnumerator DelayedZombieSpawn(float delay)
//    {
//        if (delay > 0f)
//        {
//            yield return new WaitForSeconds(delay);
//        }

//        // Lógica de generación del enemigo
//        if (zombiePrefab != null && spawnPoint != null)
//        {
//            Instantiate(zombiePrefab, spawnPoint.position, spawnPoint.rotation);
//        }
//        else
//        {
//            // Solo logramos un error si no se configuran las referencias críticas
//            Debug.LogError("Error: Zombie Prefab o Spawn Point no asignados en el Inspector.");
//        }
//    }
//}



using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class BathroomEncounterTrigger : MonoBehaviour
{
    // === CONFIGURACIÓN GENERAL ===
    [SerializeField] private string targetTag = "Player";

    private bool isTriggered = false;

    // === CONFIGURACIÓN DE EVENTOS DEL BAÑO ===
    [SerializeField] private UnityEvent onPlayerEnter = new UnityEvent();

    [SerializeField] private float mirrorMessageDelay = 5f;
    [SerializeField] private UnityEvent onMirrorMessage = new UnityEvent();

    [SerializeField] private float restoreNormalDelay = 3f;
    [SerializeField] private UnityEvent onRestoreNormal = new UnityEvent();

    private void OnTriggerEnter(Collider other)
    {
        if (isTriggered) return;

        if (other.CompareTag(targetTag))
        {
            isTriggered = true;
            onPlayerEnter.Invoke();

            // Llama al mensaje y restauración con retraso
            StartCoroutine(DelayedAction(mirrorMessageDelay, onMirrorMessage));
            StartCoroutine(DelayedAction(restoreNormalDelay, onRestoreNormal));
        }
    }

    private IEnumerator DelayedAction(float delay, UnityEvent action)
    {
        if (delay <= 0f)
        {
            action.Invoke();
            yield break;
        }

        yield return new WaitForSeconds(delay);
        action.Invoke();
    }
}