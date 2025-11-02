<<<<<<< Updated upstream
using UnityEngine;
using UnityEngine.UI;
using System;
using DG.Tweening;

public class FaderDotween : MonoBehaviour
{
    [SerializeField] private Image fadeImage;
    [SerializeField] private float fadeDuration = 1.5f;

    private Action onFadeComplete;

    public void SetOnFadeComplete(Action callback)
    {
        onFadeComplete = callback;
    }

    public void FadeOut()
    {
        fadeImage.gameObject.SetActive(true);
        fadeImage.color = new Color(0f, 0f, 0f, 0f);

        fadeImage.DOFade(1f, fadeDuration)
            .SetEase(Ease.Linear)
            .OnComplete(() =>
            {
                onFadeComplete?.Invoke();
            });
    }

    public void FadeIn()
    {
        fadeImage.gameObject.SetActive(true);
        fadeImage.color = Color.black;

        fadeImage.DOFade(0f, fadeDuration)
            .SetEase(Ease.Linear)
            .OnComplete(() =>
            {
                fadeImage.gameObject.SetActive(false);
                onFadeComplete?.Invoke();
            });
    }
=======
//dotween using UnityEngine;

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

using UnityEngine;

using UnityEngine.UI;

using System;

using DG.Tweening;



public class FaderDotween : MonoBehaviour

{

    [SerializeField] private Image fadeImage;

    [SerializeField] private float fadeDuration = 1.5f;



    private Action onFadeComplete;



    public void SetOnFadeComplete(Action callback)

    {

        onFadeComplete = callback;

    }



    public void FadeOut()

    {

        fadeImage.gameObject.SetActive(true);

        fadeImage.color = new Color(0f, 0f, 0f, 0f);



        fadeImage.DOFade(1f, fadeDuration)

            .SetEase(Ease.Linear)

            .OnComplete(() =>

            {

                onFadeComplete?.Invoke();

            });

    }



    public void FadeIn()

    {

        fadeImage.gameObject.SetActive(true);

        fadeImage.color = Color.black;



        fadeImage.DOFade(0f, fadeDuration)

            .SetEase(Ease.Linear)

            .OnComplete(() =>

            {

                fadeImage.gameObject.SetActive(false);

                onFadeComplete?.Invoke();

            });

    }

>>>>>>> Stashed changes
}