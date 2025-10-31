using UnityEngine;
using UnityEngine.UI;

public class UIDistanceFader : MonoBehaviour
{
    public float fadeStartDistance = 10f; // Distance at which fading begins
    public float fadeEndDistance = 20f;   // Distance at which element is fully invisible
    public Camera mainCamera; // Assign your main camera in the Inspector

    private CanvasGroup canvasGroup;

    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            Debug.LogError("UIDistanceFader requires a CanvasGroup component on the same GameObject.");
            enabled = false; // Disable the script if no CanvasGroup is found
            return;
        }

        if (mainCamera == null)
        {
            mainCamera = Camera.main; // Try to find the main camera if not assigned
            if (mainCamera == null)
            {
                Debug.LogError("No main camera found or assigned to UIDistanceFader.");
                enabled = false;
                return;
            }
        }
    }

    void Update()
    {
        if (canvasGroup == null || mainCamera == null) return;

        float distance = Vector3.Distance(transform.position, mainCamera.transform.position);

        // Calculate alpha based on distance
        float targetAlpha = 1f;
        if (distance > fadeStartDistance)
        {
            targetAlpha = 1f - Mathf.InverseLerp(fadeStartDistance, fadeEndDistance, distance);
        }

        // Apply the calculated alpha to the CanvasGroup
        canvasGroup.alpha = targetAlpha;
    }
}