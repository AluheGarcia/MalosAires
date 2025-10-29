using UnityEngine;
using System.Collections;

// SRP: Este script solo maneja el evento de sonido de la puerta.
public class DoorKnockEvent : MonoBehaviour
{
    [Tooltip("El AudioSource que emitirá el sonido de los golpes.")]
    [SerializeField] private AudioSource knockAudioSource;

    [Tooltip("Retraso en segundos antes de que el sonido comience a reproducirse.")]
    [SerializeField] private float delayBeforeKnock = 2.0f;

    [Tooltip("Duración total en segundos que durará el sonido de los golpes.")]
    [SerializeField] private float knockDuration = 3.0f;

    private void Awake()
    {
        // Encapsulamiento: Asegurar que el AudioSource esté configurado.
        if (knockAudioSource == null)
        {
            // Intentar obtenerlo del mismo GameObject si no está asignado.
            knockAudioSource = GetComponent<AudioSource>();
        }

        if (knockAudioSource == null)
        {
            Debug.LogError("Se requiere un AudioSource para DoorKnockEvent.");
        }
    }

    // OCP: Este método es el punto de entrada que se conecta al UnityEvent del trigger.
    public void StartKnockSequence()
    {
        // Si ya hay un sonido en curso, no iniciar otro (KISS).
        if (knockAudioSource != null && knockAudioSource.isPlaying) return;

        // Iniciar la Coroutine que maneja el retraso y la duración.
        StartCoroutine(KnockRoutine());
    }

    private IEnumerator KnockRoutine()
    {
        // 1. Retraso (el '2f después' que solicitaste).
        yield return new WaitForSeconds(delayBeforeKnock);

        // 2. Iniciar el Sonido (debe ser un clip en bucle para que dure)
        if (knockAudioSource != null && knockAudioSource.clip != null)
        {
            // Configuramos el AudioSource para que esté en bucle y se reproduzca.
            knockAudioSource.loop = true;
            knockAudioSource.Play();
        }
        else
        {
            // Si el clip no está, salimos para evitar errores.
            yield break;
        }

        // 3. Esperar la duración del sonido de los golpes.
        yield return new WaitForSeconds(knockDuration);

        // 4. Detener el sonido (terminar el evento).
        knockAudioSource.Stop();
        knockAudioSource.loop = false;
    }
}