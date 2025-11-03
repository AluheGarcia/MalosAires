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
            { "Tuto2", "Mouse 1 para atacar" },
            { "Tuto3", "Recoge las vendas - Presiona 2 para seleccionarlas" },
            { "Tuto4", "Tomar mate recupera estamina - Presiona 1 para seleccionarlo" },
            { "Tuto5", "Presiona la C para cambiar de arma. Recargala con la R" },
            { "Tuto6", "Debo buscar la bateria del auto" },
            { "Tuto7", "SampleText" },
            { "Tuto8", "Debo llegar a ese helicoptero" },
            { "Tuto9", "SampleText" }
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
