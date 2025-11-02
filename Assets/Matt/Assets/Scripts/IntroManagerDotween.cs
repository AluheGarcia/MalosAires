//using UnityEngine;
//using UnityEngine.UI;
//using UnityEngine.SceneManagement;
//using System.Collections;
//using DG.Tweening;
//using TMPro;
//using System;

//public class IntroManagerDotween : MonoBehaviour
//{
//    private enum IntroState
//    {
//        LogoReveal,
//        GameTitleReveal, // Etapa final: Título/Menú
//        MenuReady,
//        LoadingGame
//    }

//    [Header("Referencias UI")]
//    [SerializeField] private FaderDotween fader;
//    [SerializeField] private Image teamLogoImage;
//    [SerializeField] private Image gameTitleImage;
//    [SerializeField] private GameObject menuPanel;
//    [SerializeField] private GameObject controlsPanel;
//    [SerializeField] private GameObject creditsPanel;

//    [Header("Audio")]
//    [SerializeField] private AudioSource audioSource;
//    [SerializeField] private AudioClip logoSound;
//    [SerializeField] private AudioClip menuMusic;

//    [Header("Tiempos")]
//    [SerializeField] private float logoDisplayTime = 3f;

//    private IntroState currentState = IntroState.LogoReveal;

//    private void Start()
//    {
//        DOTween.Init();
//        // Desactiva todos los elementos de menú e intro al inicio
//        menuPanel.SetActive(false);
//        controlsPanel.SetActive(false);
//        creditsPanel.SetActive(false);
//        teamLogoImage.gameObject.SetActive(false); // CORREGIDO
//        gameTitleImage.gameObject.SetActive(false); // CORREGIDO

//        StartLogoReveal();
//    }

//    // --- Secuencia de Animación ---

//    // 1. Muestra el logo del equipo
//    private void StartLogoReveal()
//    {
//        currentState = IntroState.LogoReveal;
//        teamLogoImage.gameObject.SetActive(true); // CORREGIDO
//        audioSource.PlayOneShot(logoSound);

//        // Al terminar el FadeIn (revela logo), espera y luego hace FadeOut (fundido a negro)
//        fader.SetOnFadeComplete(() => {
//            StartCoroutine(WaitAndFadeOut(logoDisplayTime, ShowMenu));
//        });

//        fader.FadeIn();
//    }

//    // 2. Función de ayuda: Espera un tiempo y luego realiza el FadeOut
//    private IEnumerator WaitAndFadeOut(float delay, Action nextAction)
//    {
//        yield return new WaitForSeconds(delay);

//        // Al terminar el fundido a negro (FadeOut), ejecuta la siguiente acción (ShowMenu)
//        fader.SetOnFadeComplete(() => {
//            nextAction?.Invoke();
//        });
//        fader.FadeOut();
//    }

//    // 3. Muestra el título y el menú (pantalla final)
//    private void ShowMenu()
//    {
//        currentState = IntroState.GameTitleReveal;
//        teamLogoImage.gameObject.SetActive(false); // CORREGIDO

//        // 1. Activar los elementos visuales finales
//        gameTitleImage.gameObject.SetActive(true); // CORREGIDO
//        menuPanel.SetActive(true);

//        // 2. Tocar música
//        audioSource.loop = true;
//        audioSource.clip = menuMusic;
//        audioSource.Play();

//        // 3. Animación de aparición (DOTween)
//        gameTitleImage.transform.localScale = Vector3.zero;
//        gameTitleImage.transform.DOScale(1f, 1f).SetEase(Ease.OutExpo);

//        menuPanel.transform.localScale = Vector3.zero;
//        menuPanel.transform.DOScale(1f, 0.5f)
//            .SetEase(Ease.OutBack)
//            .SetDelay(0.5f);

//        // 4. Fundido a transparente final para revelar todo
//        fader.SetOnFadeComplete(() => {
//            currentState = IntroState.MenuReady;
//            CanvasGroup menuGroup = menuPanel.GetComponent<CanvasGroup>();
//            if (menuGroup != null) menuGroup.interactable = true;
//        });

//        fader.FadeIn();
//    }

//    // --- Lógica de Transición de Paneles (Controles/Créditos) ---

//    public void OnControlsClicked()
//    {
//        if (currentState != IntroState.MenuReady) return;
//        AnimatePanelSwitch(menuPanel, controlsPanel);
//    }

//    public void OnCreditsClicked()
//    {
//        if (currentState != IntroState.MenuReady) return;
//        AnimatePanelSwitch(menuPanel, creditsPanel);
//    }

//    public void OnBackFromControlsClicked()
//    {
//        AnimatePanelSwitch(controlsPanel, menuPanel);
//    }

//    public void OnBackFromCreditsClicked()
//    {
//        AnimatePanelSwitch(creditsPanel, menuPanel);
//    }

//    // Método encapsulado para animar la transición entre dos paneles
//    private void AnimatePanelSwitch(GameObject panelToHide, GameObject panelToShow)
//    {
//        CanvasGroup groupToHide = panelToHide.GetComponent<CanvasGroup>();
//        if (groupToHide != null) groupToHide.interactable = false;

//        panelToHide.transform.DOScale(0f, 0.3f)
//            .SetEase(Ease.InBack)
//            .OnComplete(() =>
//            {
//                panelToHide.SetActive(false);

//                panelToShow.SetActive(true);
//                panelToShow.transform.localScale = Vector3.zero;

//                CanvasGroup groupToShow = panelToShow.GetComponent<CanvasGroup>();
//                if (groupToShow != null) groupToShow.interactable = false;

//                panelToShow.transform.DOScale(1f, 0.5f)
//                    .SetEase(Ease.OutBack)
//                    .OnComplete(() =>
//                    {
//                        if (groupToShow != null) groupToShow.interactable = true;
//                    });
//            });
//    }

//    // --- Carga y Salida ---

//    public void OnStartClicked()
//    {
//        if (currentState != IntroState.MenuReady) return;
//        currentState = IntroState.LoadingGame;

//        menuPanel.SetActive(false);
//        gameTitleImage.gameObject.SetActive(false); // CORREGIDO

//        fader.SetOnFadeComplete(() => {
//            StartCoroutine(LoadGameSceneAsync("Level1"));
//        });

//        fader.FadeOut(); // Fundido a negro para ocultar la carga
//    }

//    // Método de carga asíncrona (Corrutina)
//    private IEnumerator LoadGameSceneAsync(string sceneName)
//    {
//        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
//        asyncLoad.allowSceneActivation = false;

//        while (asyncLoad.progress < 0.9f)
//        {
//            yield return null;
//        }

//        asyncLoad.allowSceneActivation = true;
//    }

//    public void OnExitClicked()
//    {
//        if (currentState != IntroState.MenuReady) return;

//        DOTween.KillAll();

//        if (audioSource != null && audioSource.isPlaying)
//        {
//            audioSource.Stop();
//        }

//        Application.Quit();

//#if UNITY_EDITOR
//        UnityEditor.EditorApplication.isPlaying = false;
//#endif
//    }
//}



using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using DG.Tweening;
using TMPro;
using System;

public class IntroManagerDotween : MonoBehaviour
{
    private enum IntroState
    {
        LogoReveal,
        GameTitleReveal,
        MenuReady,
        LoadingGame
    }

    [Header("Referencias UI")]
    [SerializeField] private FaderDotween fader;
    [SerializeField] private Image teamLogoImage;
    [SerializeField] private Image gameTitleImage;
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject controlsPanel;
    [SerializeField] private GameObject creditsPanel;

    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip logoSound;
    [SerializeField] private AudioClip menuMusic;
    // Guardamos el volumen original del menú
    private float initialMenuVolume;

    [Header("Tiempos")]
    [SerializeField] private float logoDisplayTime = 3f;

    [Header("Carga de Escena")]
    // 1. Añadimos una variable para el nombre de la escena (configurable)
    [SerializeField] private string gameSceneName = "Level_01";
    // 2. Variable para controlar la velocidad del fundido final
    [SerializeField] private float quickFadeDuration = 0.5f;

    private IntroState currentState = IntroState.LogoReveal;

    private void Start()
    {
        DOTween.Init();
        // Guardar el volumen inicial del menú
        if (audioSource != null)
        {
            initialMenuVolume = audioSource.volume;
        }

        // Desactiva todos los elementos de menú e intro al inicio
        menuPanel.SetActive(false);
        controlsPanel.SetActive(false);
        creditsPanel.SetActive(false);
        teamLogoImage.gameObject.SetActive(false);
        gameTitleImage.gameObject.SetActive(false);

        StartLogoReveal();
    }

    // --- Secuencia de Animación ---

    private void StartLogoReveal()
    {
        currentState = IntroState.LogoReveal;
        teamLogoImage.gameObject.SetActive(true);
        audioSource.PlayOneShot(logoSound);

        fader.SetOnFadeComplete(() => {
            StartCoroutine(WaitAndFadeOut(logoDisplayTime, ShowMenu));
        });

        fader.FadeIn(1f); // Asumo que el FadeIn por defecto es 1s
    }

    private IEnumerator WaitAndFadeOut(float delay, Action nextAction)
    {
        yield return new WaitForSeconds(delay);

        fader.SetOnFadeComplete(() => {
            nextAction?.Invoke();
        });
        fader.FadeOut(1f); // Asumo que el FadeOut por defecto es 1s
    }

    private void ShowMenu()
    {
        currentState = IntroState.GameTitleReveal;
        teamLogoImage.gameObject.SetActive(false);

        gameTitleImage.gameObject.SetActive(true);
        menuPanel.SetActive(true);

        audioSource.loop = true;
        audioSource.clip = menuMusic;
        audioSource.volume = initialMenuVolume; // Asegurar el volumen correcto
        audioSource.Play();

        gameTitleImage.transform.localScale = Vector3.zero;
        gameTitleImage.transform.DOScale(1f, 1f).SetEase(Ease.OutExpo);

        menuPanel.transform.localScale = Vector3.zero;
        menuPanel.transform.DOScale(1f, 0.5f)
            .SetEase(Ease.OutBack)
            .SetDelay(0.5f);

        fader.SetOnFadeComplete(() => {
            currentState = IntroState.MenuReady;
            CanvasGroup menuGroup = menuPanel.GetComponent<CanvasGroup>();
            if (menuGroup != null) menuGroup.interactable = true;
        });

        fader.FadeIn(1f);
    }

    // --- Lógica de Transición de Paneles (Controles/Créditos) ---

    // ... (Mantener las funciones de click de controles/créditos sin cambios) ...

    private void AnimatePanelSwitch(GameObject panelToHide, GameObject panelToShow)
    {
        CanvasGroup groupToHide = panelToHide.GetComponent<CanvasGroup>();
        if (groupToHide != null) groupToHide.interactable = false;

        panelToHide.transform.DOScale(0f, 0.3f)
            .SetEase(Ease.InBack)
            .OnComplete(() =>
            {
                panelToHide.SetActive(false);

                panelToShow.SetActive(true);
                panelToShow.transform.localScale = Vector3.zero;

                CanvasGroup groupToShow = panelToShow.GetComponent<CanvasGroup>();
                if (groupToShow != null) groupToShow.interactable = false;

                panelToShow.transform.DOScale(1f, 0.5f)
                    .SetEase(Ease.OutBack)
                    .OnComplete(() =>
                    {
                        if (groupToShow != null) groupToShow.interactable = true;
                    });
            });
    }

    // --- Carga y Salida ---

    public void OnStartClicked()
    {
        if (currentState != IntroState.MenuReady) return;
        currentState = IntroState.LoadingGame;

        menuPanel.SetActive(false);
        gameTitleImage.gameObject.SetActive(false);

        // 3. SECUENCIA DE TRANSICIÓN: Fundido a Negro y Fundido de Audio
        fader.SetOnFadeComplete(() => {
            StartCoroutine(LoadGameSceneAsync(gameSceneName));
        });

        // Fundido a negro con la duración rápida configurable
        fader.FadeOut(quickFadeDuration);

        // Fundido de audio a 0 en el mismo tiempo
        audioSource.DOFade(0f, quickFadeDuration).SetEase(Ease.Linear);
    }

    private IEnumerator LoadGameSceneAsync(string sceneName)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        asyncLoad.allowSceneActivation = false;

        while (asyncLoad.progress < 0.9f)
        {
            yield return null;
        }

        // Devolvemos el volumen a su estado inicial para la próxima escena (opcional, 
        // pero buena práctica si el AudioSource persiste)
        audioSource.volume = initialMenuVolume;

        asyncLoad.allowSceneActivation = true;
    }

    public void OnExitClicked()
    {
        if (currentState != IntroState.MenuReady) return;

        DOTween.KillAll();

        if (audioSource != null && audioSource.isPlaying)
        {
            audioSource.DOFade(0f, 0.5f).OnComplete(() =>
            {
                Application.Quit();
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#endif
            });
            return;
        }

        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}