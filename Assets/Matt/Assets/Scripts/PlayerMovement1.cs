using UnityEngine;

public class PlayerMovement1 : MonoBehaviour
{
    // Velocidad de movimiento configurable.
    public float movementSpeed = 5f;

    // Componente Rigidbody.
    private Rigidbody rb;

    void Start()
    {
        // Inicialización del Rigidbody.
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody is missing on the player object.");
        }

        // Bloqueo y ocultamiento del cursor para juegos de primera/tercera persona.
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void FixedUpdate()
    {
        // 1. Obtener la entrada de movimiento vertical (Adelante/Atrás)
        float moveZ = Input.GetAxisRaw("Vertical");

        // 🟢 LÍNEA CORREGIDA: Obtiene la entrada horizontal (Izquierda/Derecha)
        float moveX = Input.GetAxisRaw("Horizontal");

        // 2. Calcular la dirección de movimiento relativa al objeto.
        Vector3 moveDirection = (transform.forward * moveZ) + (transform.right * moveX);

        // 3. Calcular la nueva velocidad.
        Vector3 newVelocity = moveDirection.normalized * movementSpeed;

        // 4. Aplicar la nueva velocidad al Rigidbody, manteniendo la velocidad Y (para la gravedad).
        rb.linearVelocity = new Vector3(newVelocity.x, rb.linearVelocity.y, newVelocity.z);
    }
}