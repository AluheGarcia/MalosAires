//using UnityEngine;
//using UnityEngine.UI;
//using System;
//using DG.Tweening;

//public class FaderDotween : MonoBehaviour
//{
//    [SerializeField] private Image fadeImage;
//    [SerializeField] private float fadeDuration = 1.5f;

//    private Action onFadeComplete;

//    public void SetOnFadeComplete(Action callback)
//    {
//        onFadeComplete = callback;
//    }

//    public void FadeOut()
//    {
//        fadeImage.gameObject.SetActive(true);
//        fadeImage.color = new Color(0f, 0f, 0f, 0f);

//        fadeImage.DOFade(1f, fadeDuration)
//            .SetEase(Ease.Linear)
//            .OnComplete(() =>
//            {
//                onFadeComplete?.Invoke();
//            });
//    }

//    public void FadeIn()
//    {
//        fadeImage.gameObject.SetActive(true);
//        fadeImage.color = Color.black;

//        fadeImage.DOFade(0f, fadeDuration)
//            .SetEase(Ease.Linear)
//            .OnComplete(() =>
//            {
//                fadeImage.gameObject.SetActive(false);
//                onFadeComplete?.Invoke();
//            });
//    }
//}


using UnityEngine;
using UnityEngine.UI;
using System;
using DG.Tweening;

public class FaderDotween : MonoBehaviour
{
    // Variables privadas y serializadas (SerializeField y private)
    [SerializeField] private Image fadeImage;
    [SerializeField] private float defaultFadeDuration = 1.5f;

    private Action onFadeComplete;

    public void SetOnFadeComplete(Action callback)
    {
        onFadeComplete = callback;
    }

    /// <summary>
    /// Inicia el fundido a negro (oculta la escena).
    /// </summary>
    /// <param name="duration">Tiempo de fundido (opcional). Usa defaultFadeDuration si es 0 o negativo.</param>
    public void FadeOut(float duration = 0f)
    {
        float finalDuration = duration > 0 ? duration : defaultFadeDuration;

        fadeImage.gameObject.SetActive(true);
        // Establece la imagen a transparente para empezar el fundido a negro (alfa 0)
        fadeImage.color = new Color(0f, 0f, 0f, 0f);

        fadeImage.DOFade(1f, finalDuration)
            .SetEase(Ease.Linear)
            .OnComplete(() =>
            {
                onFadeComplete?.Invoke();
            });
    }

    /// <summary>
    /// Inicia el fundido a transparente (revela la escena).
    /// </summary>
    /// <param name="duration">Tiempo de fundido (opcional). Usa defaultFadeDuration si es 0 o negativo.</param>
    public void FadeIn(float duration = 0f)
    {
        float finalDuration = duration > 0 ? duration : defaultFadeDuration;

        fadeImage.gameObject.SetActive(true);
        // Establece la imagen a negro sólido para empezar el fundido a transparente (alfa 1)
        fadeImage.color = Color.black;

        fadeImage.DOFade(0f, finalDuration)
            .SetEase(Ease.Linear)
            .OnComplete(() =>
            {
                fadeImage.gameObject.SetActive(false);
                onFadeComplete?.Invoke();
            });
    }
}