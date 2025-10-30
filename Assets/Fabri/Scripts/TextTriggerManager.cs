using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextTriggerManager : MonoBehaviour
{
    public TextMeshProUGUI uiText;
    private Dictionary<string, string> triggerTexts;
    public float fadeDuration = 1f;
    public float displayDuration = 2f;



    void Start()
    {
        triggerTexts = new Dictionary<string, string>
        {
            { "Tuto1", "WASD - Movimiento / Mouse - Mirar" },
            { "Tuto2", "Recoge el cuchillo con la tecla E " },
            { "Tuto3", "Mouse 1 para atacar" },
            { "Tuto4", "Recoge las vendas y curate" },
            { "Tuto5", "Recoge la pistola" }
        };
    }

    public void ShowMessage(string triggerKey)
    {
        if (triggerTexts.TryGetValue(triggerKey, out string message))
        {
            StopAllCoroutines();
            StartCoroutine(FadeTextRoutine(message));

        }
        else
        {
            Debug.LogWarning($"No text found for trigger: {triggerKey}");
        }
    }

    private IEnumerator FadeTextRoutine(string message)
    {
        uiText.text = message;
        Color originalColor = uiText.color;

        // Fade in
        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            float alpha = t / fadeDuration;
            uiText.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            yield return null;
        }
        uiText.color = new Color(originalColor.r, originalColor.g, originalColor.b, 1f);

        // Wait
        yield return new WaitForSeconds(displayDuration);

        // Fade out
        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            float alpha = 1f - (t / fadeDuration);
            uiText.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            yield return null;
        }
        uiText.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0f);
    }



}
