using UnityEngine;

public class AutoRotate : MonoBehaviour
{
    [Header("Ejes de rotación")]
    public Vector3 rotationAxis = new Vector3(0, 1, 0); 

    [Header("Velocidad de rotación")]
    public float rotationSpeed = 45f;

    void Update()
    {
        transform.Rotate(rotationAxis * rotationSpeed * Time.deltaTime);
    }
}
