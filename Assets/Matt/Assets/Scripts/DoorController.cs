//using UnityEngine;
//using System.Collections;

//public class DoorController : MonoBehaviour
//{
//    // Ángulo que abrirá la puerta (e.g., Vector3(0, 90, 0) para rotar en el eje Y 90 grados)
//    [SerializeField] private Vector3 openRotation = new Vector3(0f, 90f, 0f);
//    // Ángulo de la puerta cerrada (debería ser 0,0,0 si la has configurado bien en Blender)
//    [SerializeField] private Vector3 closedRotation = new Vector3(0f, 0f, 0f);
//    // Tiempo que tarda en abrirse/cerrarse (suavizado)
//    [SerializeField] private float openSpeed = 0.5f;

//    private bool isOpen = false;
//    private Coroutine currentCoroutine;

//    public void ToggleDoor()
//    {
//        // Detiene cualquier animación de apertura/cierre en curso
//        if (currentCoroutine != null)
//        {
//            StopCoroutine(currentCoroutine);
//        }

//        isOpen = !isOpen;
//        currentCoroutine = StartCoroutine(MoveDoorRoutine());
//    }

//    private IEnumerator MoveDoorRoutine()
//    {
//        Quaternion startRot = transform.localRotation;
//        Quaternion targetRot = Quaternion.Euler(isOpen ? openRotation : closedRotation);
//        float timeElapsed = 0f;

//        while (timeElapsed < openSpeed)
//        {
//            // Rotación suave (Slerp)
//            transform.localRotation = Quaternion.Slerp(startRot, targetRot, timeElapsed / openSpeed);
//            timeElapsed += Time.deltaTime;
//            yield return null;
//        }

//        // Asegura que la rotación sea exacta al final
//        transform.localRotation = targetRot;
//        currentCoroutine = null;
//    }
//}

using UnityEngine;
using System.Collections;

public class DoorController : MonoBehaviour
{
    [SerializeField] private Vector3 openRotation = new Vector3(0f, 90f, 0f);
    [SerializeField] private Vector3 closedRotation = new Vector3(0f, 0f, 0f);
    [SerializeField] private float openSpeed = 0.5f;

    private bool isOpen = false;
    private Coroutine currentCoroutine;

    /// <summary>
    /// Intenta abrir la puerta. No hace nada si ya está abierta.
    /// </summary>
    public void OpenDoor()
    {
        if (isOpen) return;

        if (currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
        }

        isOpen = true;
        currentCoroutine = StartCoroutine(MoveDoorRoutine());
    }

    /// <summary>
    /// Alterna el estado de la puerta (abrir si está cerrada, cerrar si está abierta).
    /// </summary>
    public void ToggleDoor()
    {
        if (currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
        }

        isOpen = !isOpen;
        currentCoroutine = StartCoroutine(MoveDoorRoutine());
    }

    private IEnumerator MoveDoorRoutine()
    {
        Quaternion startRot = transform.localRotation;
        Quaternion targetRot = Quaternion.Euler(isOpen ? openRotation : closedRotation);
        float timeElapsed = 0f;

        while (timeElapsed < openSpeed)
        {
            transform.localRotation = Quaternion.Slerp(startRot, targetRot, timeElapsed / openSpeed);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        transform.localRotation = targetRot;
        currentCoroutine = null;
    }
}