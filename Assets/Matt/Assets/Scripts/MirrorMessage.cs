//using UnityEngine;
//using System.Collections;

//public class MirrorMessage : MonoBehaviour
//{
//    [Tooltip("El GameObject que contiene el mensaje visible en el espejo.")]
//    [SerializeField] private GameObject messageObject;

//    [Tooltip("Duración en segundos que el mensaje permanece visible.")]
//    [SerializeField] private float displayDuration = 2.0f;

//    private void Awake()
//    {
//        if (messageObject != null)
//        {
//            messageObject.SetActive(false); // Asegurarse de que esté oculto al inicio.
//        }
//        else
//        {
//            Debug.LogError("MessageObject no asignado en MirrorMessage.");
//        }
//    }

//    // OCP: Método público llamado por el Trigger.
//    public void ShowAndHideMessage()
//    {
//        StartCoroutine(DisplayMessageRoutine());
//    }

//    private IEnumerator DisplayMessageRoutine()
//    {
//        if (messageObject != null)
//        {
//            messageObject.SetActive(true);
//            yield return new WaitForSeconds(displayDuration);
//            messageObject.SetActive(false);
//        }
//    }
//}



using UnityEngine;
using System.Collections;

public class MirrorMessage : MonoBehaviour
{
    // Objeto que se activa/desactiva
    [SerializeField] private GameObject messageObject;
    // Cuánto tiempo estará visible
    [SerializeField] private float displayDuration = 2.0f;

    // Bandera para evitar que se ejecute dos veces a la vez
    private bool isDisplaying = false;

    private void Awake()
    {
        if (messageObject == null)
        {
            Debug.LogError("Error: MessageObject no asignado en el Inspector de MirrorMessage.");
            return;
        }

        // 1. Ocultar el objeto al inicio (funciona)
        messageObject.SetActive(false);
    }

    /// <summary>
    /// Método público llamado por el Trigger. Muestra el mensaje por el tiempo configurado.
    /// </summary>
    public void ShowAndHideMessage()
    {
        if (messageObject == null || isDisplaying) return;

        // 2. Iniciar la corrutina que maneja la activación y el tiempo
        StartCoroutine(DisplayMessageRoutine());
    }

    private IEnumerator DisplayMessageRoutine()
    {
        // 3. Activar el mensaje
        isDisplaying = true;
        messageObject.SetActive(true);

        // 4. Esperar el tiempo
        yield return new WaitForSeconds(displayDuration);

        // 5. Desactivar el mensaje
        messageObject.SetActive(false);
        isDisplaying = false;
    }
}