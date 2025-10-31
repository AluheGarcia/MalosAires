//using UnityEngine;
//using System.Collections;

//public class FlickeringLightsController : MonoBehaviour
//{
//    // SOLID: Principio de Responsabilidad Única (SRP) - Solo maneja la lógica de parpadeo.
//    [Tooltip("Lista de componentes Light que serán afectados.")]
//    [SerializeField] private Light[] targetLights;

//    [Tooltip("Tiempo mínimo y máximo entre apagados (más corto = más frenético).")]
//    [SerializeField] private Vector2 flickerDurationRange = new Vector2(0.1f, 0.4f);

//    [Tooltip("Tiempo mínimo y máximo que la luz permanece apagada.")]
//    [SerializeField] private Vector2 offTimeRange = new Vector2(0.05f, 0.15f);

//    [Tooltip("Intensidad de luz base para restaurar después de parpadear.")]
//    [SerializeField] private float defaultIntensity = 1.0f;

//    [Tooltip("Factor de probabilidad (0 a 1) de que una luz parpadee en un ciclo.")]
//    [SerializeField] private float flickerProbability = 0.6f;

//    private bool _isFlickering = false;

//    private void Awake()
//    {
//        // Almacenar la intensidad original si no se especifica
//        if (targetLights.Length > 0 && defaultIntensity == 1.0f)
//        {
//            defaultIntensity = targetLights[0].intensity;
//        }
//    }

//    // Método público para iniciar el parpadeo, conectado posiblemente a otro Trigger o evento.
//    // OCP: Puede ser llamado desde el Trigger que ya creaste, o desde un nuevo evento.
//    public void StartFlickering()
//    {
//        if (!_isFlickering)
//        {
//            _isFlickering = true;
//            // KISS: Usamos una Coroutine simple para manejar el tiempo sin depender del Update().
//            StartCoroutine(FlickerRoutine());
//        }
//    }

//    // Método público para detener el parpadeo y restaurar las luces.
//    public void StopFlickering()
//    {
//        if (_isFlickering)
//        {
//            _isFlickering = false;
//            StopAllCoroutines();

//            // Restaurar todas las luces a la intensidad predeterminada.
//            foreach (Light light in targetLights)
//            {
//                if (light != null)
//                {
//                    light.enabled = true;
//                    light.intensity = defaultIntensity;
//                }
//            }
//        }
//    }

//    private IEnumerator FlickerRoutine()
//    {
//        while (_isFlickering)
//        {
//            float waitTime = Random.Range(flickerDurationRange.x, flickerDurationRange.y);
//            yield return new WaitForSeconds(waitTime);

//            // Seleccionar aleatoriamente las luces que parpadearán en este ciclo.
//            foreach (Light light in targetLights)
//            {
//                if (light != null && Random.value <= flickerProbability)
//                {
//                    StartCoroutine(DoSingleLightFlicker(light));
//                }
//            }
//        }
//    }

//    private IEnumerator DoSingleLightFlicker(Light light)
//    {
//        // Apagar
//        light.enabled = false;

//        float offTime = Random.Range(offTimeRange.x, offTimeRange.y);
//        yield return new WaitForSeconds(offTime);

//        // Encender
//        light.enabled = true;
//    }
//}



using UnityEngine;
using System.Collections;
using System.Collections.Generic; // Asegúrate de tener este 'using' si usas List<T>

public class FlickeringLightsController : MonoBehaviour
{
    // SOLID: Principio de Responsabilidad Única (SRP) - Solo maneja la lógica de parpadeo.
    [Tooltip("Lista de componentes Light que serán afectados.")]
    [SerializeField] private Light[] targetLights;

    // Nuevo campo para la duración
    [Tooltip("Tiempo total en segundos que durará el efecto de parpadeo.")]
    [SerializeField] private float totalFlickerDuration = 10f; // Por ejemplo, 10 segundos

    [Tooltip("Tiempo mínimo y máximo entre apagados (más corto = más frenético).")]
    [SerializeField] private Vector2 flickerDurationRange = new Vector2(0.1f, 0.4f);

    [Tooltip("Tiempo mínimo y máximo que la luz permanece apagada.")]
    [SerializeField] private Vector2 offTimeRange = new Vector2(0.05f, 0.15f);

    [Tooltip("Intensidad de luz base para restaurar después de parpadear.")]
    [SerializeField] private float defaultIntensity = 1.0f;

    [Tooltip("Factor de probabilidad (0 a 1) de que una luz parpadee en un ciclo.")]
    [SerializeField] private float flickerProbability = 0.6f;

    private bool _isFlickering = false;

    // ... (El Awake() se mantiene igual)

    // OCP: El método sigue siendo el punto de inicio.
    public void StartFlickering()
    {
        if (!_isFlickering)
        {
            _isFlickering = true;
            // Detenemos la corrutina si estaba corriendo por si acaso.
            StopAllCoroutines();
            // Iniciamos la corrutina de control de duración.
            StartCoroutine(FlickerDurationRoutine());
        }
    }

    // Este nuevo método gestiona la duración total.
    private IEnumerator FlickerDurationRoutine()
    {
        // 1. Iniciar el parpadeo de las luces (la lógica de la corrutina interna).
        StartCoroutine(FlickerRoutine());

        // 2. Esperar el tiempo total de duración.
        yield return new WaitForSeconds(totalFlickerDuration);

        // 3. Detener el efecto después de que el tiempo ha transcurrido.
        StopFlickering();
    }


    public void StopFlickering()
    {
        if (_isFlickering)
        {
            _isFlickering = false;
            // Es crucial detener las corrutinas activas, incluida la FlickerRoutine.
            StopAllCoroutines();

            // Restaurar todas las luces a la intensidad predeterminada.
            foreach (Light light in targetLights)
            {
                if (light != null)
                {
                    light.enabled = true;
                    light.intensity = defaultIntensity;
                }
            }
        }
    }

    // La lógica de parpadeo ahora solo necesita ejecutarse mientras _isFlickering sea true.
    private IEnumerator FlickerRoutine()
    {
        // Mientras el control de duración no haya llamado a StopFlickering...
        while (_isFlickering)
        {
            float waitTime = Random.Range(flickerDurationRange.x, flickerDurationRange.y);
            yield return new WaitForSeconds(waitTime);

            // Seleccionar aleatoriamente las luces que parpadearán en este ciclo.
            foreach (Light light in targetLights)
            {
                if (light != null && Random.value <= flickerProbability)
                {
                    // No necesitamos esperar por esta corrutina, se ejecuta en paralelo.
                    StartCoroutine(DoSingleLightFlicker(light));
                }
            }
        }
    }

    private IEnumerator DoSingleLightFlicker(Light light)
    {
        // Verificamos si la luz no fue desactivada por StopFlickering mientras esperábamos
        if (!_isFlickering) yield break;

        // Apagar
        light.enabled = false;

        float offTime = Random.Range(offTimeRange.x, offTimeRange.y);
        yield return new WaitForSeconds(offTime);

        // Encender, solo si aún estamos parpadeando (previene encender después del apagón final)
        if (_isFlickering)
        {
            light.enabled = true;
        }
    }
}