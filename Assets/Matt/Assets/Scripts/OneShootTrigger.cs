using UnityEngine;
using UnityEngine.Events;

public class OneShotTrigger : MonoBehaviour
{
    [Tooltip("Etiqueta (Tag) que debe tener el objeto para activar el trigger.")]
    [SerializeField] private string targetTag = "Player";

    [Tooltip("Componente AudioSource para reproducir el sonido.")]
    [SerializeField] private AudioSource audioSource;

    [Tooltip("Evento que se invoca cuando el trigger se activa por primera vez.")]
    [SerializeField] private UnityEvent onFirstTrigger;

    private Collider _collider;

    private void Awake()
    {
        // Obtener el Collider en Awake para encapsulamiento y seguridad.
        _collider = GetComponent<Collider>();
        if (_collider == null)
        {
            Debug.LogError("El componente Collider (o Trigger) es requerido en el mismo GameObject.");
        }

        // Asumiendo que se requiere un AudioSource
        if (audioSource == null)
        {
            Debug.LogWarning("AudioSource no asignado. Solo se ejecutará el UnityEvent.");
        }

        // Asegurar que el collider esté configurado como trigger
        if (_collider != null && !_collider.isTrigger)
        {
            Debug.LogWarning("El Collider no está marcado como 'Is Trigger'. El trigger no funcionará.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Principio de Responsabilidad Única (SRP): Este método solo maneja la detección y la llamada a los eventos.
        if (other.CompareTag(targetTag))
        {
            ExecuteOneShotEvent();
        }
    }

    private void ExecuteOneShotEvent()
    {
        // 1. Reproducir Sonido
        if (audioSource != null && audioSource.clip != null)
        {
            audioSource.PlayOneShot(audioSource.clip);
        }

        // 2. Invocar Evento (para la máquina expendedora, etc.)
        onFirstTrigger.Invoke();

        // 3. Desactivar el Trigger (para el uso único - KISS)
        // Desactivamos el componente Collider para que no detecte más entradas.
        if (_collider != null)
        {
            _collider.enabled = false;
        }

        // Opcional: Desactivar todo el GameObject si no se necesita nada más.
        // gameObject.SetActive(false); 
    }
}